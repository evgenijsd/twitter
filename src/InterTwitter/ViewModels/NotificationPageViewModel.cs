using InterTwitter.Enums;
using InterTwitter.Extensions;
using InterTwitter.Helpers;
using InterTwitter.Models;
using InterTwitter.Models.NotificationViewModel;
using InterTwitter.Services;
using InterTwitter.Views;
using Prism.Navigation;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace InterTwitter.ViewModels
{
    public class NotificationPageViewModel : BaseTabViewModel
    {
        private readonly ITweetService _tweetService;

        private readonly IAuthorizationService _autorizationService;

        private readonly IRegistrationService _registrationService;

        private readonly ILikeService _likeService;

        private readonly IBookmarkService _bookmarkService;

        private UserModel _currentUser;
        private int _userId;

        public NotificationPageViewModel(
            ITweetService tweetService,
            INavigationService navigationService,
            ILikeService likeService,
            IBookmarkService bookmarkService,
            IAuthorizationService autorizationService,
            IRegistrationService registrationService)
            : base(navigationService)
        {
            IconPath = Prism.PrismApplicationBase.Current.Resources["ic_notifications_gray"] as ImageSource;
            _tweetService = tweetService;
            _bookmarkService = bookmarkService;
            _likeService = likeService;
            _autorizationService = autorizationService;
            _registrationService = registrationService;
        }

        #region -- Public Properties --

        private bool _IsNotFound;

        public bool IsNotFound
        {
            get => _IsNotFound;
            set => SetProperty(ref _IsNotFound, value);
        }

        private ObservableCollection<BaseNotificationViewModel> _tweets;

        public ObservableCollection<BaseNotificationViewModel> Tweets
        {
            get => _tweets;
            set => SetProperty(ref _tweets, value);
        }

        private ICommand _openFlyoutCommandAsync;

        public ICommand OpenFlyoutCommandAsync => _openFlyoutCommandAsync ?? (_openFlyoutCommandAsync = SingleExecutionCommand.FromFunc(OnOpenFlyoutCommandAsync));

        #endregion

        #region -- Overrides --

        protected override void OnPropertyChanged(PropertyChangedEventArgs args)
        {
            base.OnPropertyChanged(args);

            if (args.PropertyName == nameof(Tweets))
            {
                IsNotFound = Tweets == null || Tweets.Count == 0;
            }
        }

        public override void OnAppearing()
        {
            IconPath = Prism.PrismApplicationBase.Current.Resources["ic_notifications_blue"] as ImageSource;
        }

        public override void OnDisappearing()
        {
            IconPath = Prism.PrismApplicationBase.Current.Resources["ic_notifications_gray"] as ImageSource;
        }

        #endregion

        #region -- Private Helpers --

        private Task OnOpenFlyoutCommandAsync()
        {
            MessagingCenter.Send(this, Constants.Messages.OPEN_SIDEBAR, true);
            MessagingCenter.Send(this, Constants.Messages.TAB_CHANGE, typeof(NotificationsPage));
            return Task.CompletedTask;
        }

        public override async void OnNavigatedTo(INavigationParameters parameters)
        {
            _userId = _autorizationService.UserId;
            var result = await _registrationService.GetByIdAsync(_userId);

            if (result.IsSuccess)
            {
                _currentUser = result.Result;
                var resultTweets = await _tweetService.GetByUserTweetsAsync(_userId);

                if (resultTweets.IsSuccess)
                {
                    var notificationViewModels = new ObservableCollection<BaseNotificationViewModel>();

                    var resultBookmarks = await _bookmarkService.GetNotificationsAsync(_userId);
                    if (resultBookmarks.IsSuccess)
                    {
                        var bookmarks = resultBookmarks.Result.Where(x => resultTweets.Result.Any(y => y.Id == x.TweetId)).ToList();
                        foreach (var b in bookmarks)
                        {
                            var tweet = resultTweets.Result.FirstOrDefault(x => x.Id == b.TweetId);
                            var user = await _tweetService.GetAuthorAsync(b.UserId);
                            var notification = new BaseNotificationViewModel
                            {
                                TweetId = b.TweetId,
                                UserId = b.UserId,
                                CreationTime = b.CreationTime,
                                UserAvatar = user.Result?.AvatarPath,
                                UserName = user.Result?.Name,
                                Text = tweet.Text,
                                MediaPaths = tweet.MediaPaths,
                                Media = tweet.Media,
                                NotificationIcon = "ic_bookmarks_blue",
                                NotificationText = "saved your post",
                            };

                            notificationViewModels.Add(notification);
                        }
                    }

                    var resultLikes = await _likeService.GetNotificationsAsync(_userId);
                    if (resultLikes.IsSuccess)
                    {
                        var likes = resultLikes.Result.Where(x => resultTweets.Result.Any(y => y.Id == x.TweetId)).ToList();
                        foreach (var l in likes)
                        {
                            var tweet = resultTweets.Result.FirstOrDefault(x => x.Id == l.TweetId);
                            var user = await _tweetService.GetAuthorAsync(l.UserId);
                            var notification = new BaseNotificationViewModel
                            {
                                TweetId = l.TweetId,
                                UserId = l.UserId,
                                CreationTime = l.CreationTime,
                                UserAvatar = user.Result?.AvatarPath,
                                UserName = user.Result?.Name,
                                Text = tweet.Text,
                                MediaPaths = tweet.MediaPaths,
                                Media = tweet.Media,
                                NotificationIcon = "ic_like_blue",
                                NotificationText = "liked your post",
                            };

                            notificationViewModels.Add(notification);
                        }
                    }

                    Tweets = new ObservableCollection<BaseNotificationViewModel>(notificationViewModels
                            .Select(x => x.Media == EAttachedMediaType.Photos || x.Media == EAttachedMediaType.Gif ? x.ToImagesNotificationViewModel() : x.ToBaseNotificationViewModel())
                            .OrderByDescending(x => x.CreationTime));
                }
            }

            #endregion

        }
    }
}