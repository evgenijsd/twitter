using InterTwitter.Enums;
using InterTwitter.Extensions;
using InterTwitter.Helpers;
using InterTwitter.Models;
using InterTwitter.Models.TweetViewModel;
using InterTwitter.Services;
using InterTwitter.Services.Authorization;
using InterTwitter.Services.Registration;
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
        private readonly IAuthorizationService _autorizationService;
        private readonly IRegistrationService _registrationService;

        private bool _isFirstStart = true;
        private UserModel _currentUser;

        public HomePageViewModel(
            ITweetService tweetService,
            INavigationService navigationService,
            IAuthorizationService autorizationService,
            IRegistrationService registrationService)
            : base(navigationService)
        {
            _tweetService = tweetService;
            _autorizationService = autorizationService;
            _registrationService = registrationService;

            IconPath = Prism.PrismApplicationBase.Current.Resources["ic_home_gray"] as ImageSource;
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
        public override void OnNavigatedTo(INavigationParameters parameters)
        {
            base.OnNavigatedTo(parameters);
        }

        public override async void OnAppearing()
        {
            if (_isFirstStart)
            {
                await InitAsync();
            }

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
                        var tweetAuthor = await _tweetService.GetAuthorAsync(tweet.UserId);

                        if (tweetAuthor.IsSuccess)
                        {
                            tweet.UserAvatar = tweetAuthor.Result.AvatarPath;
                            tweet.UserBackgroundImage = tweetAuthor.Result.BackgroundUserImagePath;
                            tweet.UserName = tweetAuthor.Result.Name;
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
