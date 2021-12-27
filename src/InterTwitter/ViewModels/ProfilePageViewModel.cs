using InterTwitter.Enums;
using InterTwitter.Extensions;
using InterTwitter.Helpers;
using InterTwitter.Models;
using InterTwitter.Models.TweetViewModel;
using InterTwitter.Services;
using InterTwitter.Services.Settings;
using InterTwitter.Services.Share;
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
        private readonly IRegistrationService _registrationService;
        private readonly IAuthorizationService _authorizationService;
        private readonly IShareService _shareService;

        private UserModel _user;
        private bool _isCurrentUser;
        private bool _isUserBlocked;
        private bool _isUserMuted;

        public ProfilePageViewModel(
            INavigationService navigationService,
            ISettingsManager settingsManager,
            IUserService userService,
            ITweetService tweetService,
            IRegistrationService registrationService,
            IAuthorizationService authorizationService,
            IShareService shareService)
            : base(navigationService)
        {
            _settingsManager = settingsManager;
            _userService = userService;
            _tweetService = tweetService;
            _registrationService = registrationService;
            _authorizationService = authorizationService;
            _shareService = shareService;
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
                        Title = Resources.Strings.Strings.Posts,
                        ImageSource = Prism.PrismApplicationBase.Current.Resources["ic_home_gray"] as ImageSource,
                        TextColor = (Color)Prism.PrismApplicationBase.Current.Resources["appcolor_i4"],
                        ContentCollection = UserTweets,
                    },

                    new MenuItemViewModel
                    {
                        Id = 1,
                        Title = Resources.Strings.Strings.Likes,
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
                    param.Add(Constants.DialogParameterKeys.OK_BUTTON_TEXT, Resources.Strings.Strings.Ok);
                    param.Add(Constants.DialogParameterKeys.MESSAGE, Resources.Strings.Strings.UserBlocked);

                    dialogs = EDialogs.AddToBlock;
                    await Rg.Plugins.Popup.Services.PopupNavigation.Instance.PushAsync(new AlertView(param, CloseDialogCallback));
                }
                else
                {
                    param.Add(Constants.DialogParameterKeys.TITLE, $"{Resources.Strings.Strings.Add} {_user.Name} {Resources.Strings.Strings.ToBlacklist}?");
                    param.Add(Constants.DialogParameterKeys.MESSAGE, Resources.Strings.Strings.UserNotSeeYouPost);
                    param.Add(Constants.DialogParameterKeys.OK_BUTTON_TEXT, Resources.Strings.Strings.AddToBlacklist);
                    param.Add(Constants.DialogParameterKeys.CANCEL_BUTTON_TEXT, Resources.Strings.Strings.Cancel);

                    dialogs = EDialogs.AddToBlock;
                    await Rg.Plugins.Popup.Services.PopupNavigation.Instance.PushAsync(new AlertView(param, CloseDialogCallback));
                }
            }
        }

        private async Task OnAddUserToMutelistCommandAsync()
        {
            var param = new DialogParameters();
            var isUserMutedResponse = await _userService.CheckIfUserIsMutedAsync(_user.Id);
            if (isUserMutedResponse.IsSuccess)
            {
                if (isUserMutedResponse.Result)
                {
                    param.Add(Constants.DialogParameterKeys.OK_BUTTON_TEXT, Resources.Strings.Strings.Ok);
                    param.Add(Constants.DialogParameterKeys.MESSAGE, Resources.Strings.Strings.UserMuted);

                    dialogs = EDialogs.AddToMute;
                    await Rg.Plugins.Popup.Services.PopupNavigation.Instance.PushAsync(new AlertView(param, CloseDialogCallback));
                }
                else
                {
                    param.Add(Constants.DialogParameterKeys.TITLE, $"{Resources.Strings.Strings.Add} {_user.Name} {Resources.Strings.Strings.ToMute}?");
                    param.Add(Constants.DialogParameterKeys.MESSAGE, Resources.Strings.Strings.UserCanSeeInfo);
                    param.Add(Constants.DialogParameterKeys.OK_BUTTON_TEXT, Resources.Strings.Strings.AddToMute);
                    param.Add(Constants.DialogParameterKeys.CANCEL_BUTTON_TEXT, Resources.Strings.Strings.Cancel);

                    dialogs = EDialogs.AddToMute;
                    await Rg.Plugins.Popup.Services.PopupNavigation.Instance.PushAsync(new AlertView(param, CloseDialogCallback));
                }
            }
        }

        private async Task OnRemoveUserFromBlacklistCommandAsync()
        {
            var param = new DialogParameters();
            param.Add(Constants.DialogParameterKeys.TITLE, $"{Resources.Strings.Strings.Remove} {_user.Name} {Resources.Strings.Strings.FromTheBlacklist}?");
            param.Add(Constants.DialogParameterKeys.OK_BUTTON_TEXT, Resources.Strings.Strings.Remove);
            param.Add(Constants.DialogParameterKeys.CANCEL_BUTTON_TEXT, Resources.Strings.Strings.Cancel);

            dialogs = EDialogs.RemoveFromBlock;
            await Rg.Plugins.Popup.Services.PopupNavigation.Instance.PushAsync(new AlertView(param, CloseDialogCallback));
        }

        private async Task OnRemoveUserFromMuteCommandAsync()
        {
            var param = new DialogParameters();
            param.Add(Constants.DialogParameterKeys.TITLE, $"{Resources.Strings.Strings.Remove} {_user.Name} {Resources.Strings.Strings.FromTheMute}?");
            param.Add(Constants.DialogParameterKeys.OK_BUTTON_TEXT, Resources.Strings.Strings.Remove);
            param.Add(Constants.DialogParameterKeys.CANCEL_BUTTON_TEXT, Resources.Strings.Strings.Cancel);

            dialogs = EDialogs.RemoveFromMute;
            await Rg.Plugins.Popup.Services.PopupNavigation.Instance.PushAsync(new AlertView(param, CloseDialogCallback));
        }

        private void CloseDialogCallback(IDialogParameters dialogResult)
        {
            switch (dialogs)
            {
                case EDialogs.RemoveFromMute:
                    {
                        bool result = (bool)dialogResult?[Constants.DialogParameterKeys.ACCEPT];
                        if (result)
                        {
                            _userService.RemoveFromMutelistAsync(_user.Id);
                            IsMuteButtonVisible = false;
                        }

                        Rg.Plugins.Popup.Services.PopupNavigation.Instance.PopAsync();
                    }

                    break;
                case EDialogs.RemoveFromBlock:
                    {
                        bool result = (bool)dialogResult?[Constants.DialogParameterKeys.ACCEPT];
                        if (result)
                        {
                            _userService.RemoveFromBlacklistAsync(_user.Id);
                            IsBlacklistButtonVisible = false;
                        }

                        Rg.Plugins.Popup.Services.PopupNavigation.Instance.PopAsync();
                    }

                    break;
                case EDialogs.AddToBlock:
                    {
                        bool result = (bool)dialogResult?[Constants.DialogParameterKeys.ACCEPT];
                        if (result)
                        {
                            _userService.RemoveFromMutelistAsync(_user.Id);
                            _userService.AddToBlacklistAsync(_user.Id);
                            IsBlacklistButtonVisible = true;
                            IsMuteButtonVisible = false;
                        }

                        Rg.Plugins.Popup.Services.PopupNavigation.Instance.PopAsync();
                    }

                    break;
                case EDialogs.AddToMute:
                    {
                        bool result = (bool)dialogResult?[Constants.DialogParameterKeys.ACCEPT];
                        if (result)
                        {
                            _userService.RemoveFromBlacklistAsync(_user.Id);
                            _userService.AddToMuteListAsync(_user.Id);
                            IsBlacklistButtonVisible = false;
                            IsMuteButtonVisible = true;
                        }

                        Rg.Plugins.Popup.Services.PopupNavigation.Instance.PopAsync();
                    }

                    break;
                default:
                    break;
            }
        }

        public enum EDialogs
        {
            RemoveFromMute,
            RemoveFromBlock,
            AddToMute,
            AddToBlock,
        }

        private EDialogs dialogs;

        public ICommand _shareUserProfileTapCommand;
        public ICommand ShareUserProfileTapCommand => _shareUserProfileTapCommand ??= SingleExecutionCommand.FromFunc(OnShareUserProfileTapCommandAsync);

        #endregion

        #region -- Overrides --

        public override void OnNavigatedTo(INavigationParameters parameters)
        {
            base.OnNavigatedTo(parameters);

            if (parameters.TryGetValue(Constants.Navigation.USER, out UserModel user))
            {
                UserName = user.Name;
                UserMail = user.Email;
                UserImagePath = user.AvatarPath;
            }
        }

        #endregion

        #region -- Private helpers --

        private async Task OnShareUserProfileTapCommandAsync()
        {
            var aOResult = await _registrationService.GetByIdAsync(_authorizationService.UserId);

            if (aOResult.IsSuccess)
            {
                var user = aOResult.Result;
                string uri = $"{Constants.Values.APP_USER_LINK}{Constants.Values.APP_USER_LINK_ID}/{user.Id}";

                await _shareService.ShareTextRequest(user.Name, uri);
            }
        }

        #endregion
    }
}
