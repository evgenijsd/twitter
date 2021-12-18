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

        public ProfilePageViewModel(INavigationService navigationService, ISettingsManager settingsManager, IUserService userService, ITweetService tweetService, IDialogService dialogService1)
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

        private ObservableCollection<BindableBase> _tweets;
        public ObservableCollection<BindableBase> Tweets
        {
            get => _tweets;
            set => SetProperty(ref _tweets, value);
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

        public ICommand NavgationCommandAsync => SingleExecutionCommand.FromFunc(NavigationService.GoBackAsync);

        public ICommand HamburgerMenuCommandAsync
            => SingleExecutionCommand.FromFunc(OnHamburgerMenuCommand);

        public ICommand AddUserToBlacklistCommandAsync
            => SingleExecutionCommand.FromFunc(OnAddUserToBlacklistCommandAsync);

        public ICommand AddUserToMuteListCommandAsync
            => SingleExecutionCommand.FromFunc(OnAddUserToMutelistCommandAsync);

        public ICommand RemoveUserFromBlacklistCommandAsync
            => SingleExecutionCommand.FromFunc(OnRemoveUserFromBlacklistCommandAsync);

        public ICommand RemoveUserFromMuteListCommandAsync
            => SingleExecutionCommand.FromFunc(OnRemoveUserFromMuteCommandAsync);

        public ICommand NavigationToEditCommandAsync => SingleExecutionCommand.FromFunc(
            () => NavigationService.NavigateAsync(nameof(EditProfilePage), new NavigationParameters { { Constants.NavigationKeys.CURRENT_USER, _user } }));

        public ICommand NavigationToBlacklistCommandAsync => SingleExecutionCommand.FromFunc(
            () => NavigationService.NavigateAsync(nameof(BlacklistPage), new NavigationParameters { { Constants.NavigationKeys.BLACKLIST, _user } }));

        public ICommand NavigationToMutelistCommandAsync => SingleExecutionCommand.FromFunc(
            () => NavigationService.NavigateAsync(nameof(BlacklistPage), new NavigationParameters { { Constants.NavigationKeys.MUTELIST, _user } }));

        #endregion

        #region -- Overrides --

        public async override Task InitializeAsync(INavigationParameters parameters)
        {
            if (parameters.ContainsKey(Constants.NavigationKeys.CURRENT_USER))
            {
                _user = parameters[Constants.NavigationKeys.CURRENT_USER] as UserModel;
                _isCurrentUser = true;
                IsChangeProfileButtonVisible = true;
            }
            else if (parameters.ContainsKey(Constants.NavigationKeys.USER))
            {
                _user = parameters[Constants.NavigationKeys.USER] as UserModel;
                _isCurrentUser = false;
                IsChangeProfileButtonVisible = false;

                _isUserMuted = _userService.IsUserMuted(_settingsManager.UserId, _user.Id).Result.Result;
                _isUserBlocked = _userService.IsUserBlocked(_settingsManager.UserId, _user.Id).Result.Result;

                IsBlacklistButtonVisible = _isUserBlocked;
                IsMuteButtonVisible = _isUserMuted;
            }

            UserBackgroundImage = _user.BackgroundUserImagePath;
            UserImagePath = _user.AvatarPath;
            UserMail = _user.Email;
            UserName = _user.Name;

            Subscribe();

            await InitAsync();

            MenuItems = new List<MenuItemViewModel>(new[]
                {
                    new MenuItemViewModel
                    {
                        Id = 0, Title = Resources.Resource.Posts,
                        ImageSource = Prism.PrismApplicationBase.Current.Resources["ic_home_gray"] as ImageSource,
                        TextColor = (Color)Prism.PrismApplicationBase.Current.Resources["appcolor_i4"],
                        ContentCollection = Tweets, // TODO: filtering
                    },

                    new MenuItemViewModel
                    {
                        Id = 1,
                        Title = Resources.Resource.Likes,
                        ImageSource = Prism.PrismApplicationBase.Current.Resources["ic_search_gray"] as ImageSource,
                        TextColor = (Color)Prism.PrismApplicationBase.Current.Resources["appcolor_i4"],
                        ContentCollection = Tweets, // TODO: filtering
                    },
                });
        }

        #endregion

        #region -- Private Helpers --

        private void Subscribe()
        {
            MessagingCenter.Subscribe<EditProfilePageViewModel>(this, Constants.Messages.USER_PROFILE_CHANGED, UpdateAsync);
        }

        private async void UpdateAsync(object sender)
        {
            await Task.Delay(1);
            _user = _userService.GetUserAsync(_settingsManager.UserId).Result.Result;

            UserBackgroundImage = _user.BackgroundUserImagePath;
            UserImagePath = _user.AvatarPath;
            UserMail = _user.Email;
            UserName = _user.Name;
        }

        private async Task InitAsync()
        {
            var getTweetResult = await _tweetService.GetAllTweetsAsync();

            if (getTweetResult.IsSuccess)
            {
                var tweetViewModels = new List<BaseTweetViewModel>(getTweetResult.Result.
                    Select(x => x.Media == EAttachedMediaType.Photos || x.Media == EAttachedMediaType.Gif ? x.ToImagesTweetViewModel() : x.ToBaseTweetViewModel()));

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

                Tweets = new ObservableCollection<BindableBase>(tweetViewModels);
            }
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
            if (_userService.IsUserBlocked(_settingsManager.UserId, _user.Id).Result.Result)
            {
                var param = new DialogParameters();
                param.Add("message", Resources.Resource.This_user_is_already_blocked);

                _dialogService.ShowDialog("AlertView", param);
            }
            else
            {
                var param = new DialogParameters();
                param.Add("title", $"{Resources.Resource.Add_} {_user.Name} {Resources.Resource._toBlacklist_}");
                param.Add("message", Resources.Resource.This_user_will_not_see_your_posts);
                param.Add("okButtonText", Resources.Resource.Add_to_Blacklist);
                param.Add("cancelButtonText", Resources.Resource.Cancel);

                _dialogService.ShowDialog("Alert2View", param, CloseDialogCallback);
            }

            void CloseDialogCallback(IDialogResult dialogResult)
            {
                bool result = (bool)dialogResult?.Parameters["Accept"];
                if (result)
                {
                    int remId = _userService.RemoveFromMutelistAsync(_settingsManager.UserId, _user.Id).Result.Result;
                    int remId2 = _userService.AddToBlacklistAsync(_settingsManager.UserId, _user.Id).Result.Result;
                    IsBlacklistButtonVisible = true;
                    IsMuteButtonVisible = false;
                }
            }
        }

        private async Task OnAddUserToMutelistCommandAsync()
        {
            if (_userService.IsUserMuted(_settingsManager.UserId, _user.Id).Result.Result)
            {
                var param = new DialogParameters();
                param.Add("message", Resources.Resource.This_user_is_already_muted);

                _dialogService.ShowDialog("AlertView", param);
            }
            else
            {
                var param = new DialogParameters();
                param.Add("title", $"{Resources.Resource.Add_} {_user.Name} {Resources.Resource._to_mute_}");
                param.Add("message", Resources.Resource.User_can_see_information_about_you);
                param.Add("okButtonText", Resources.Resource.Add_to_Mute);
                param.Add("cancelButtonText", Resources.Resource.Cancel);

                _dialogService.ShowDialog("Alert2View", param, CloseDialogCallback);
            }

            void CloseDialogCallback(IDialogResult dialogResult)
            {
                bool result = (bool)dialogResult?.Parameters["Accept"];
                if (result)
                {
                    int remId = _userService.RemoveFromBlacklistAsync(_settingsManager.UserId, _user.Id).Result.Result;
                    int remId2 = _userService.AddToMuteListAsync(_settingsManager.UserId, _user.Id).Result.Result;
                    IsBlacklistButtonVisible = false;
                    IsMuteButtonVisible = true;
                }
            }
        }

        private async Task OnRemoveUserFromBlacklistCommandAsync()
        {
            var param = new DialogParameters();
            param.Add("title", $"{Resources.Resource.Remove} {_user.Name} {Resources.Resource.from_the_Blacklist}?");
            param.Add("okButtonText", Resources.Resource.Remove);
            param.Add("cancelButtonText", Resources.Resource.Cancel);

            _dialogService.ShowDialog("Alert2View", param, CloseDialogCallback);

            void CloseDialogCallback(IDialogResult dialogResult)
            {
                bool result = (bool)dialogResult?.Parameters["Accept"];
                if (result)
                {
                    int res = _userService.RemoveFromBlacklistAsync(_settingsManager.UserId, _user.Id).Result.Result;
                    IsBlacklistButtonVisible = false;
                }
            }
        }

        private async Task OnRemoveUserFromMuteCommandAsync()
        {
            var param = new DialogParameters();
            param.Add("title", $"{Resources.Resource.Remove} {_user.Name} {Resources.Resource.from_the_mute}?");
            param.Add("okButtonText", Resources.Resource.Remove);
            param.Add("cancelButtonText", Resources.Resource.Cancel);

            _dialogService.ShowDialog("Alert2View", param, CloseDialogCallback);

            void CloseDialogCallback(IDialogResult dialogResult)
            {
                bool result = (bool)dialogResult?.Parameters["Accept"];
                if (result)
                {
                    int res = _userService.RemoveFromMutelistAsync(_settingsManager.UserId, _user.Id).Result.Result;
                    IsMuteButtonVisible = false;
                }
            }
        }

        #endregion

    }
}
