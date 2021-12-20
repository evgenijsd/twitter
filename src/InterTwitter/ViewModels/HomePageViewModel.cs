using InterTwitter.Enums;
using InterTwitter.Extensions;
using InterTwitter.Helpers;
using InterTwitter.Models;
using InterTwitter.Models.TweetViewModel;
using InterTwitter.Services;
using InterTwitter.Services.Settings;
using InterTwitter.Services.UserService;
using InterTwitter.Views;
using Prism.Navigation;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace InterTwitter.ViewModels
{
    public class HomePageViewModel : BaseTabViewModel
    {
        private readonly ITweetService _tweetService;
        private readonly IUserService _userService;
        private readonly ISettingsManager _settingsManager;
        private readonly IAuthorizationService _autorizationService;
        private readonly IRegistrationService _registrationService;

        private bool _isFirstStart = true;
        private UserModel _currentUser;

        public HomePageViewModel(
            ISettingsManager settingsManager,
            ITweetService tweetService,
            IAuthorizationService autorizationService,
            IRegistrationService registrationService,
            IUserService userService,
            INavigationService navigationService)
            : base(navigationService)
        {
            _tweetService = tweetService;
            _autorizationService = autorizationService;
            _registrationService = registrationService;

            IconPath = Prism.PrismApplicationBase.Current.Resources["ic_home_gray"] as ImageSource;
            _tweetService = tweetService;
            _userService = userService;
            _settingsManager = settingsManager;
        }

        #region -- Public properties --

        private ICommand _openFlyoutCommandAsync;
        public ICommand OpenFlyoutCommandAsync => _openFlyoutCommandAsync ?? (_openFlyoutCommandAsync = SingleExecutionCommand.FromFunc(OnOpenFlyoutCommandAsync));

        private ICommand _addTweetCommandAsync;
        public ICommand AddTweetCommandAsync => _addTweetCommandAsync ?? (_addTweetCommandAsync = SingleExecutionCommand.FromFunc(OnOpenAddTweetPageAsync));

        private ObservableCollection<BaseTweetViewModel> _tweets;

        public ObservableCollection<BaseTweetViewModel> Tweets
        {
            get => _tweets;
            set => SetProperty(ref _tweets, value);
        }

        #endregion

        #region -- Overrides --
        public override async Task InitializeAsync(INavigationParameters parameters)
        {
            await InitAsync();
        }

        public override void OnAppearing()
        {
            IconPath = Prism.PrismApplicationBase.Current.Resources["ic_home_blue"] as ImageSource;
        }

        public override void OnDisappearing()
        {
            IconPath = Prism.PrismApplicationBase.Current.Resources["ic_home_gray"] as ImageSource;
        }

        #endregion

        #region -- Private helpers --

        private async Task InitAsync()
        {
            var result = await _registrationService.GetByIdAsync(_autorizationService.UserId);
            if (result.IsSuccess)
            {
                _currentUser = result.Result;
                var getTweetResult = await _tweetService.GetAllTweetsAsync();

                if (getTweetResult.IsSuccess)
                {
                    var tweetViewModels = new List<BaseTweetViewModel>(getTweetResult.Result.Select(x => x.Media == EAttachedMediaType.Photos || x.Media == EAttachedMediaType.Gif ? x.ToImagesTweetViewModel() : x.ToBaseTweetViewModel()));

                foreach (var tweet in tweetViewModels)
                {
                    UserModel user = _userService.GetUserAsync(tweet.UserId).Result.Result;
                    var tweetAuthor = await _tweetService.GetAuthorAsync(tweet.UserId);

                    if (tweetAuthor.IsSuccess)
                    {
                        tweet.UserAvatar = tweetAuthor.Result.AvatarPath;
                        tweet.UserBackgroundImage = tweetAuthor.Result.BackgroundUserImagePath;
                        tweet.UserName = tweetAuthor.Result.Name;
                        if (user.Id == _settingsManager.UserId)
                        {
                            tweet.MoveToProfileCommand = new Command(() => NavigationService.NavigateAsync(nameof(ProfilePage), new NavigationParameters { { Constants.NavigationKeys.CURRENT_USER, user } }));
                        }
                        else
                        {
                            tweet.MoveToProfileCommand = new Command(() => NavigationService.NavigateAsync(nameof(ProfilePage), new NavigationParameters { { Constants.NavigationKeys.USER, user } }));
                        }
                    }
                }

                    Tweets = new ObservableCollection<BaseTweetViewModel>(tweetViewModels);
                }
            }
        }

        private Task OnOpenAddTweetPageAsync()
        {
            return Task.CompletedTask;
        }

        private Task OnOpenFlyoutCommandAsync()
        {
            MessagingCenter.Send(this, Constants.Messages.OPEN_SIDEBAR, true);
            MessagingCenter.Send(this, Constants.Messages.TAB_CHANGE, typeof(HomePage));
            return Task.CompletedTask;
        }

        #endregion
    }
}
