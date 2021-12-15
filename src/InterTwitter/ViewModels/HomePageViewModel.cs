using InterTwitter.Enums;
using InterTwitter.Extensions;
using InterTwitter.Helpers;
using InterTwitter.Models;
using InterTwitter.Models.TweetViewModel;
using InterTwitter.Services;
using InterTwitter.Services.BookmarkService;
using InterTwitter.Services.LikeService;
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
        private readonly IBookmarkService _bookmarkService;
        private readonly ILikeService _likeService;

        private bool _isFirstStart = true;

        public HomePageViewModel(
            INavigationService navigationService,
            IBookmarkService bookmarkService,
            ILikeService likeService,
            ITweetService tweetService)
            : base(navigationService)
        {
            IconPath = Prism.PrismApplicationBase.Current.Resources["ic_home_gray"] as ImageSource;
            _bookmarkService = bookmarkService;
            _likeService = likeService;
            _tweetService = tweetService;
        }

        #region -- Public properties --

        private int _userId = 0;
        public int UserId
        {
            get => _userId;
            set => SetProperty(ref _userId, value);
        }

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

            MessagingCenter.Unsubscribe<MessageEvent>(this, MessageEvent.AddBookmark);
            MessagingCenter.Unsubscribe<MessageEvent>(this, MessageEvent.DeleteBookmark);
            MessagingCenter.Unsubscribe<MessageEvent>(this, MessageEvent.AddLike);
            MessagingCenter.Unsubscribe<MessageEvent>(this, MessageEvent.DeleteLike);
        }

        #endregion

        #region -- Private helpers --

        private async Task InitAsync()
        {
            int userid = 1;
            UserId = userid;
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
                        tweet.IsBookmarked = (await _bookmarkService.AnyAsync(tweet.TweetId, UserId)).IsSuccess;
                        tweet.IsTweetLiked = (await _likeService.AnyAsync(tweet.TweetId, UserId)).IsSuccess;
                        var result = await _likeService.CountAsync(tweet.TweetId);
                        if (result.IsSuccess)
                        {
                            tweet.LikesNumber = result.Result;
                        }
                    }
                }

                Tweets = new ObservableCollection<BaseTweetViewModel>(tweetViewModels);

                MessagingCenter.Subscribe<MessageEvent>(this, MessageEvent.AddBookmark, (me) => AddBookmarkAsync(me));
                MessagingCenter.Subscribe<MessageEvent>(this, MessageEvent.DeleteBookmark, (me) => DeleteBookmarkAsync(me));
                MessagingCenter.Subscribe<MessageEvent>(this, MessageEvent.AddLike, (me) => AddLikeAsync(me));
                MessagingCenter.Subscribe<MessageEvent>(this, MessageEvent.DeleteLike, (me) => DeleteLikeAsync(me));
            }
        }

        private async void AddBookmarkAsync(MessageEvent me)
        {
            var result = await _bookmarkService.AddBookmarkAsync(me.UnTweetId, UserId);
        }

        private async void DeleteBookmarkAsync(MessageEvent me)
        {
            var result = await _bookmarkService.DeleteBoormarkAsync(me.UnTweetId, UserId);
        }

        private async void AddLikeAsync(MessageEvent me)
        {
            var resultAdd = await _likeService.AddLikeAsync(me.UnTweetId, UserId);
            var result = await _likeService.CountAsync(me.UnTweetId);
            if (result.IsSuccess)
            {
                var tweet = Tweets.FirstOrDefault(x => x.TweetId == me.UnTweetId);
                if (tweet != null)
                {
                    tweet.LikesNumber = result.Result;
                }
            }
        }

        private async void DeleteLikeAsync(MessageEvent me)
        {
            var resultAdd = await _likeService.DeleteLikeAsync(me.UnTweetId, UserId);
            var result = await _likeService.CountAsync(me.UnTweetId);
            if (result.IsSuccess)
            {
                var tweet = Tweets.FirstOrDefault(x => x.TweetId == me.UnTweetId);
                tweet.LikesNumber = result.Result;
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
