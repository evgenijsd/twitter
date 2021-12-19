using InterTwitter.Enums;
using InterTwitter.Extensions;
using InterTwitter.Helpers;
using InterTwitter.Models.TweetViewModel;
using InterTwitter.Services;
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
    public class NotificationPageViewModel : BaseTabViewModel
    {
        private readonly ITweetService _tweetService;

        public NotificationPageViewModel(
            ITweetService tweetService,
            INavigationService navigationService)
            : base(navigationService)
        {
            IconPath = Prism.PrismApplicationBase.Current.Resources["ic_notifications_gray"] as ImageSource;
            _tweetService = tweetService;
        }

        #region --- Public Properties ---

        private int _userId = 0;
        public int UserId
        {
            get => _userId;
            set => SetProperty(ref _userId, value);
        }

        private ObservableCollection<BaseTweetViewModel> _tweets;

        public ObservableCollection<BaseTweetViewModel> Tweets
        {
            get => _tweets;
            set => SetProperty(ref _tweets, value);
        }

        private ICommand _openFlyoutCommandAsync;

        public ICommand OpenFlyoutCommandAsync => _openFlyoutCommandAsync ?? (_openFlyoutCommandAsync = SingleExecutionCommand.FromFunc(OnOpenFlyoutCommandAsync));

        #endregion

        #region -- Overrides --

        public override void OnAppearing()
        {
            IconPath = Prism.PrismApplicationBase.Current.Resources["ic_notifications_blue"] as ImageSource;
        }

        public override void OnDisappearing()
        {
            IconPath = Prism.PrismApplicationBase.Current.Resources["ic_notifications_gray"] as ImageSource;
        }

        #endregion

        #region --- Private Helpers ---

        private Task OnOpenFlyoutCommandAsync()
        {
            MessagingCenter.Send(this, Constants.Messages.OPEN_SIDEBAR, true);
            MessagingCenter.Send(this, Constants.Messages.TAB_CHANGE, typeof(NotificationsPage));
            return Task.CompletedTask;
        }

        public override async void OnNavigatedTo(INavigationParameters parameters)
        {
            int userid = 1;
            UserId = userid;

            var resultTweet = await _tweetService.GetAllTweetsAsync();
            var getTweetResult = resultTweet.Result.ToList();

            if (resultTweet.IsSuccess)
            {
                var tweetViewModels = new List<BaseTweetViewModel>(getTweetResult
                    .Select(x => x.Media == EAttachedMediaType.Photos || x.Media == EAttachedMediaType.Gif ? x.ToImagesTweetViewModel() : x.ToBaseTweetViewModel()).OrderByDescending(x => x.CreationTime));

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

            #endregion

        }
    }
}
