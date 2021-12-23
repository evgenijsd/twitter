using InterTwitter.Enums;
using InterTwitter.Extensions;
using InterTwitter.Helpers;
using InterTwitter.Models;
using InterTwitter.Models.TweetViewModel;
using InterTwitter.Services;
using InterTwitter.Services.Settings;
using InterTwitter.Services.UserService;
using InterTwitter.Views;
using Prism.Mvvm;
using Prism.Services.Dialogs;
using Prism.Navigation;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace InterTwitter.ViewModels
{
    public class ProfilePageViewModel : BaseViewModel
    {
        private readonly ISettingsManager _settingsManager;
        private readonly IUserService _userService;
        private readonly ITweetService _tweetService;
        private readonly IDialogService _dialogService;

        private UserModel _user;
        private bool _isCurrentUser;
        private bool _isUserBlocked;
        private bool _isUserMuted;

        public ProfilePageViewModel(
            INavigationService navigationService,
            ISettingsManager settingsManager,
            IUserService userService,
            ITweetService tweetService,
            IDialogService dialogService1)
            : base(navigationService)
        {
            _settingsManager = settingsManager;
            _userService = userService;
            _tweetService = tweetService;
            _dialogService = dialogService1;
        }

        #region --- Public Properties ---

        private List<MenuItemViewModel> _menuItems;
        public List<MenuItemViewModel> MenuItems
        {
            get => _menuItems;
            set => SetProperty(ref _menuItems, value);
        }

        private ObservableCollection<BindableBase> _userTweets;
        public ObservableCollection<BindableBase> UserTweets
        {
            get => _userTweets;
            set => SetProperty(ref _userTweets, value);
        }

        private ObservableCollection<BindableBase> _likedtweets;
        public ObservableCollection<BindableBase> LikedTweets
        {
            get => _likedtweets;
            set => SetProperty(ref _likedtweets, value);
        }

        private string _userMail;
        public string UserMail
        {
            get => _userMail;
            set => SetProperty(ref _userMail, value);
        }

        private string _userName;
        public string UserName
        {
            get => _userName;
            set => SetProperty(ref _userName, value);
        }

        private string _userBackgroundImage;
        public string UserBackgroundImage
        {
            get => _userBackgroundImage;
            set => SetProperty(ref _userBackgroundImage, value);
        }

        private string _userImagePath;
        public string UserImagePath
        {
            get => _userImagePath;
            set => SetProperty(ref _userImagePath, value);
        }

        private bool _isUserMenuVisible;
        public bool IsUserMenuVisible
        {
            get => _isUserMenuVisible;
            set => SetProperty(ref _isUserMenuVisible, value);
        }

        private bool _isCurrentUserMenuVisible;
        public bool IsCurrentUserMenuVisible
        {
            get => _isCurrentUserMenuVisible;
            set => SetProperty(ref _isCurrentUserMenuVisible, value);
        }

        private bool _isBlacklistButtonVisible;
        public bool IsBlacklistButtonVisible
        {
            get => _isBlacklistButtonVisible;
            set => SetProperty(ref _isBlacklistButtonVisible, value);
        }

        private bool _isChangeProfileButtonVisible;
        public bool IsChangeProfileButtonVisible
        {
            get => _isChangeProfileButtonVisible;
            set => SetProperty(ref _isChangeProfileButtonVisible, value);
        }

        private bool _isMuteButtonVisible;
        public bool IsMuteButtonVisible
        {
            get => _isMuteButtonVisible;
            set => SetProperty(ref _isMuteButtonVisible, value);
        }

        private ICommand _navgationCommandAsync;
        public ICommand NavgationCommandAsync => _navgationCommandAsync ??= SingleExecutionCommand.FromFunc(NavigationService.GoBackAsync);

        private ICommand _hamburgerMenuCommandAsync;
        public ICommand HamburgerMenuCommandAsync
            => _hamburgerMenuCommandAsync ??= SingleExecutionCommand.FromFunc(OnHamburgerMenuCommand);

        private ICommand _addUserToBlacklistCommandAsync;
        public ICommand AddUserToBlacklistCommandAsync
            => _addUserToBlacklistCommandAsync ??= SingleExecutionCommand.FromFunc(OnAddUserToBlacklistCommandAsync);

        private ICommand _addUserToMuteListCommandAsync;
        public ICommand AddUserToMuteListCommandAsync
            => _addUserToMuteListCommandAsync ??= SingleExecutionCommand.FromFunc(OnAddUserToMutelistCommandAsync);

        private ICommand _removeUserFromBlacklistCommandAsync;
        public ICommand RemoveUserFromBlacklistCommandAsync
            => _removeUserFromBlacklistCommandAsync ??= SingleExecutionCommand.FromFunc(OnRemoveUserFromBlacklistCommandAsync);

        private ICommand _removeUserFromMuteListCommandAsync;
        public ICommand RemoveUserFromMuteListCommandAsync
            => _removeUserFromMuteListCommandAsync ??= SingleExecutionCommand.FromFunc(OnRemoveUserFromMuteCommandAsync);

        private ICommand _navigationToEditCommandAsync;
        public ICommand NavigationToEditCommandAsync => _navigationToEditCommandAsync ??= SingleExecutionCommand.FromFunc(
            () => NavigationService.NavigateAsync(nameof(EditProfilePage), new NavigationParameters { { Constants.Navigation.CURRENT_USER, _user } }));

        private ICommand _navigationToBlacklistCommandAsync;
        public ICommand NavigationToBlacklistCommandAsync => _navigationToBlacklistCommandAsync ??= SingleExecutionCommand.FromFunc(
            () => NavigationService.NavigateAsync(nameof(BlacklistPage), new NavigationParameters { { Constants.Navigation.BLACKLIST, _user } }));

        private ICommand _navigationToMutelistCommandAsync;
        public ICommand NavigationToMutelistCommandAsync => _navigationToMutelistCommandAsync ??= SingleExecutionCommand.FromFunc(
            () => NavigationService.NavigateAsync(nameof(BlacklistPage), new NavigationParameters { { Constants.Navigation.MUTELIST, _user } }));

        #endregion

        #region -- Overrides --

        public async override Task InitializeAsync(INavigationParameters parameters)
        {
            if (parameters.ContainsKey(Constants.Navigation.CURRENT_USER))
            {
                _user = parameters[Constants.Navigation.CURRENT_USER] as UserModel;
                _isCurrentUser = true;
                IsChangeProfileButtonVisible = true;
            }
            else if (parameters.ContainsKey(Constants.Navigation.USER))
            {
                _user = parameters[Constants.Navigation.USER] as UserModel;
                _isCurrentUser = false;
                IsChangeProfileButtonVisible = false;

                _isUserMuted = _userService.CheckIfUserIsMutedAsync(_user.Id).Result.Result;
                _isUserBlocked = _userService.CheckIfUserIsBlockedAsync(_user.Id).Result.Result;

                IsBlacklistButtonVisible = _isUserBlocked;
                IsMuteButtonVisible = _isUserMuted;
            }

            UserBackgroundImage = _user.BackgroundUserImagePath;
            UserImagePath = _user.AvatarPath;
            UserMail = _user.Email;
            UserName = _user.Name;

            Subscribe();

            await InitAsync();
        }

        #endregion

        #region -- Private Helpers --

        private void Subscribe()
        {
            MessagingCenter.Subscribe<EditProfilePageViewModel>(this, Constants.Messages.USER_PROFILE_CHANGED, UpdateAsync);
        }

        private async void UpdateAsync(object sender)
        {
            var userResponse = await _userService.GetUserAsync(_settingsManager.UserId);
            if (userResponse.IsSuccess)
            {
                _user = userResponse.Result;
                UserBackgroundImage = _user.BackgroundUserImagePath;
                UserImagePath = _user.AvatarPath;
                UserMail = _user.Email;
                UserName = _user.Name;
            }
        }

        private async Task InitAsync()
        {
            var getTweetResult = await _tweetService.GetAllTweetsAsync();

            if (getTweetResult.IsSuccess)
            {
                var tweetViewModels = new List<BaseTweetViewModel>(getTweetResult.Result.
                    Select(x => x.Media == EAttachedMediaType.Photos ||
                    x.Media == EAttachedMediaType.Gif ? x.ToImagesTweetViewModel() : x.ToBaseTweetViewModel()));

                foreach (var tweet in tweetViewModels)
                {
                    var tweetAuthor = await _tweetService.GetAuthorAsync(tweet.UserId);

                    if (tweetAuthor.IsSuccess)
                    {
                        tweet.UserAvatar = tweetAuthor.Result.AvatarPath;
                        tweet.UserBackgroundImage = tweetAuthor.Result.BackgroundUserImagePath;
                        tweet.UserName = tweetAuthor.Result.Name;
                    }
                }

                UserTweets = new ObservableCollection<BindableBase>(tweetViewModels);
                LikedTweets = new ObservableCollection<BindableBase>(tweetViewModels);
            }

            MenuItems = new List<MenuItemViewModel>(new[]
                {
                    new MenuItemViewModel
                    {
                        Id = 0,
                        Title = Resources.Resource.Posts,
                        ImageSource = Prism.PrismApplicationBase.Current.Resources["ic_home_gray"] as ImageSource,
                        TextColor = (Color)Prism.PrismApplicationBase.Current.Resources["appcolor_i4"],
                        ContentCollection = UserTweets,
                    },

                    new MenuItemViewModel
                    {
                        Id = 1,
                        Title = Resources.Resource.Likes,
                        ImageSource = Prism.PrismApplicationBase.Current.Resources["ic_search_gray"] as ImageSource,
                        TextColor = (Color)Prism.PrismApplicationBase.Current.Resources["appcolor_i4"],
                        ContentCollection = LikedTweets,
                    },
                });
        }

        private Task OnHamburgerMenuCommand()
        {
            if (_isCurrentUser)
            {
                IsCurrentUserMenuVisible = true;
            }
            else
            {
                IsUserMenuVisible = true;
            }

            return Task.CompletedTask;
        }

        private async Task OnAddUserToBlacklistCommandAsync()
        {
            var param = new DialogParameters();
            var isUserBlockedResponse = await _userService.CheckIfUserIsBlockedAsync(_user.Id);
            if (isUserBlockedResponse.IsSuccess)
            {
                if (isUserBlockedResponse.Result)
                {
                    param.Add(Constants.DialogParameterKeys.MESSAGE, Resources.Resource.This_user_is_already_blocked);

                    _dialogService.ShowDialog(nameof(AlertView), param);
                }
                else
                {
                    param.Add(Constants.DialogParameterKeys.TITLE, $"{Resources.Resource.Add_} {_user.Name} {Resources.Resource._toBlacklist_}");
                    param.Add(Constants.DialogParameterKeys.MESSAGE, Resources.Resource.This_user_will_not_see_your_posts);
                    param.Add(Constants.DialogParameterKeys.OK_BUTTON_TEXT, Resources.Resource.Add_to_Blacklist);
                    param.Add(Constants.DialogParameterKeys.CANCEL_BUTTON_TEXT, Resources.Resource.Cancel);

                    _dialogService.ShowDialog(nameof(AlertView), param, CloseDialogCallback);
                }
            }

            void CloseDialogCallback(IDialogResult dialogResult)
            {
                bool result = (bool)dialogResult?.Parameters[Constants.DialogParameterKeys.ACCEPT];
                if (result)
                {
                    _userService.RemoveFromMutelistAsync(_user.Id);
                    _userService.AddToBlacklistAsync(_user.Id);
                    IsBlacklistButtonVisible = true;
                    IsMuteButtonVisible = false;
                }
            }
        }

        private async Task OnAddUserToMutelistCommandAsync()
        {
            var param = new DialogParameters();
            var isUserMutedResponse = await _userService.CheckIfUserIsBlockedAsync(_user.Id);
            if (isUserMutedResponse.IsSuccess)
            {
                if (isUserMutedResponse.Result)
                {
                    param.Add(Constants.DialogParameterKeys.MESSAGE, Resources.Resource.This_user_is_already_muted);

                    _dialogService.ShowDialog(nameof(AlertView), param);
                }
                else
                {
                    param.Add(Constants.DialogParameterKeys.TITLE, $"{Resources.Resource.Add_} {_user.Name} {Resources.Resource._to_mute_}");
                    param.Add(Constants.DialogParameterKeys.MESSAGE, Resources.Resource.User_can_see_information_about_you);
                    param.Add(Constants.DialogParameterKeys.OK_BUTTON_TEXT, Resources.Resource.Add_to_Mute);
                    param.Add(Constants.DialogParameterKeys.CANCEL_BUTTON_TEXT, Resources.Resource.Cancel);

                    _dialogService.ShowDialog(nameof(AlertView), param, CloseDialogCallback);
                }
            }

            void CloseDialogCallback(IDialogResult dialogResult)
            {
                bool result = (bool)dialogResult?.Parameters[Constants.DialogParameterKeys.ACCEPT];
                if (result)
                {
                    _userService.RemoveFromBlacklistAsync(_user.Id);
                    _userService.AddToMuteListAsync(_user.Id);
                    IsBlacklistButtonVisible = false;
                    IsMuteButtonVisible = true;
                }
            }
        }

        private Task OnRemoveUserFromBlacklistCommandAsync()
        {
            var param = new DialogParameters();
            param.Add(Constants.DialogParameterKeys.TITLE, $"{Resources.Resource.Remove} {_user.Name} {Resources.Resource.from_the_Blacklist}?");
            param.Add(Constants.DialogParameterKeys.OK_BUTTON_TEXT, Resources.Resource.Remove);
            param.Add(Constants.DialogParameterKeys.CANCEL_BUTTON_TEXT, Resources.Resource.Cancel);

            _dialogService.ShowDialog(nameof(AlertView), param, CloseDialogCallback);
            return Task.CompletedTask;

            void CloseDialogCallback(IDialogResult dialogResult)
            {
                bool result = (bool)dialogResult?.Parameters[Constants.DialogParameterKeys.ACCEPT];
                if (result)
                {
                    _userService.RemoveFromBlacklistAsync(_user.Id);
                    IsBlacklistButtonVisible = false;
                }
            }
        }

        private Task OnRemoveUserFromMuteCommandAsync()
        {
            var param = new DialogParameters();
            param.Add(Constants.DialogParameterKeys.TITLE, $"{Resources.Resource.Remove} {_user.Name} {Resources.Resource.from_the_mute}?");
            param.Add(Constants.DialogParameterKeys.OK_BUTTON_TEXT, Resources.Resource.Remove);
            param.Add(Constants.DialogParameterKeys.CANCEL_BUTTON_TEXT, Resources.Resource.Cancel);

            _dialogService.ShowDialog(nameof(AlertView), param, CloseDialogCallback);
            return Task.CompletedTask;

            void CloseDialogCallback(IDialogResult dialogResult)
            {
                bool result = (bool)dialogResult?.Parameters[Constants.DialogParameterKeys.ACCEPT];
                if (result)
                {
                    _userService.RemoveFromMutelistAsync(_user.Id);
                    IsMuteButtonVisible = false;
                }
            }
        }

        #endregion

    }
}
