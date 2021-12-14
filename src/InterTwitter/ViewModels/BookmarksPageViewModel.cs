using InterTwitter.Enums;
using InterTwitter.Extensions;
using InterTwitter.Helpers;
using InterTwitter.Models.TweetViewModel;
using InterTwitter.Services;
using InterTwitter.Services.BookmarkService;
using InterTwitter.Services.LikeService;
using InterTwitter.Services.Settings;
using InterTwitter.Views;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace InterTwitter.ViewModels
{
    public class BookmarksPageViewModel : BaseTabViewModel
    {
        private readonly ITweetService _tweetService;
        private readonly IBookmarkService _bookmarkService;
        private readonly ILikeService _likeService;

        public BookmarksPageViewModel(
            INavigationService navigationService,
            ITweetService tweetService,
            ILikeService likeService,
            IBookmarkService bookmarkService)
            : base(navigationService)
        {
            IconPath = Prism.PrismApplicationBase.Current.Resources["ic_bookmarks_gray"] as ImageSource;
            _tweetService = tweetService;
            _likeService = likeService;
            _bookmarkService = bookmarkService;
        }

        #region -- Public properties --

        private bool _IsNotFound;
        public bool IsNotFound
        {
            get => _IsNotFound;
            set => SetProperty(ref _IsNotFound, value);
        }

        private bool _IsVisibleButton = false;
        public bool IsVisibleButton
        {
            get => _IsVisibleButton;
            set => SetProperty(ref _IsVisibleButton, value);
        }

        private int _userId = 0;
        public int UserId
        {
            get => _userId;
            set => SetProperty(ref _userId, value);
        }

        private string _imageSource = "ic_hidden_menu_gray";
        public string ImageSource
        {
            get => _imageSource;
            set => SetProperty(ref _imageSource, value);
        }

        private ObservableCollection<BaseTweetViewModel> _tweets;
        public ObservableCollection<BaseTweetViewModel> Tweets
        {
            get => _tweets;
            set => SetProperty(ref _tweets, value);
        }

        private ICommand _VisibleButtonCommand;
        public ICommand VisibleButtonCommand => _VisibleButtonCommand ??= SingleExecutionCommand.FromFunc(OnVisibleButtonCommandAsync);

        private ICommand _UnvisibleButtonCommand;
        public ICommand UnvisibleButtonCommand => _UnvisibleButtonCommand ??= SingleExecutionCommand.FromFunc(OnUnvisibleButtonCommandAsync);

        private ICommand _DeleteAllBookmarks;
        public ICommand DeleteAllBookmarks => _DeleteAllBookmarks ??= SingleExecutionCommand.FromFunc(OnDeleteAllBookmarksCommandAsync);

        public ICommand OpenFlyoutCommandAsync => SingleExecutionCommand.FromFunc(OnOpenFlyoutCommandAsync);

        #endregion

        #region -- Overrides --
        protected override void OnPropertyChanged(PropertyChangedEventArgs args)
        {
            base.OnPropertyChanged(args);

            if (args.PropertyName == nameof(Tweets))
            {
                IsNotFound = Tweets == null || Tweets.Count == 0;
                if (IsNotFound)
                {
                    ImageSource = string.Empty;
                }
                else
                {
                    ImageSource = "ic_hidden_menu_gray";
                }
            }
        }

        public override void OnAppearing()
        {
            IconPath = Prism.PrismApplicationBase.Current.Resources["ic_bookmarks_blue"] as ImageSource;
        }

        public override void OnDisappearing()
        {
            IconPath = Prism.PrismApplicationBase.Current.Resources["ic_bookmarks_gray"] as ImageSource;
            MessagingCenter.Unsubscribe<MessageEvent>(this, MessageEvent.DeleteBookmark);
        }

        public override async void OnNavigatedTo(INavigationParameters parameters)
        {
            int userid = 1;
            UserId = userid;

            await Task.Delay(TimeSpan.FromSeconds(0.1));

            var resultTweet = await _tweetService.GetAllTweetsAsync();
            var resultBookmark = await _bookmarkService.GetBookmarksAsync(userid);
            var getTweetResult = resultTweet.Result.ToList();
            var getBookmarks = resultBookmark.Result;

            if (resultTweet.IsSuccess && resultBookmark.IsSuccess)
            {
                var tweetViewModels = new List<BaseTweetViewModel>(getTweetResult.Where(x => getBookmarks.Any(y => y.TweetId == x.Id))
                    .Select(x => x.Media == ETypeAttachedMedia.Photos || x.Media == ETypeAttachedMedia.Gif ? x.ToImagesTweetViewModel() : x.ToBaseTweetViewModel()).OrderBy(x => x.CreationTime));

                foreach (var tweet in tweetViewModels)
                {
                    var tweetAuthor = await _tweetService.GetUserAsync(tweet.UserId);

                    if (tweetAuthor.IsSuccess)
                    {
                        tweet.UserAvatar = tweetAuthor.Result.AvatarPath;
                        tweet.UserBackgroundImage = tweetAuthor.Result.BackgroundUserImagePath;
                        tweet.UserName = tweetAuthor.Result.Name;
                        tweet.IsTweekLiked = (await _likeService.AnyAsync(tweet.TweetId, UserId)).IsSuccess;
                        var result = await _likeService.CountAsync(tweet.TweetId);
                        if (result.IsSuccess)
                        {
                            tweet.LikesNumber = result.Result;
                        }
                    }

                    tweet.IsBookmarked = true;
                }

                Tweets = new ObservableCollection<BaseTweetViewModel>(tweetViewModels);

                MessagingCenter.Subscribe<MessageEvent>(this, MessageEvent.DeleteBookmark, (me) => DeleteBookmarkAsync(me));
            }
        }

        #endregion

        #region -- Private helpers --

        private async void DeleteBookmarkAsync(MessageEvent me)
        {
            var result = await _bookmarkService.DeleteBoormarkAsync(me.UnTweetId, UserId);
            if (result.IsSuccess)
            {
                var tweet = Tweets.FirstOrDefault(x => x.TweetId == me.UnTweetId);
                Tweets.Remove(tweet);
            }
        }

        private Task OnOpenFlyoutCommandAsync()
        {
            MessagingCenter.Send(this, Constants.Messages.OPEN_SIDEBAR, true);
            MessagingCenter.Send(this, Constants.Messages.TAB_CHANGE, typeof(BookmarksPage));
            return Task.CompletedTask;
        }

        private Task OnVisibleButtonCommandAsync()
        {
            if (!IsNotFound)
            {
                IsVisibleButton = true;
            }

            return Task.CompletedTask;
        }

        private Task OnUnvisibleButtonCommandAsync()
        {
            IsVisibleButton = false;

            return Task.CompletedTask;
        }

        private async Task OnDeleteAllBookmarksCommandAsync()
        {
            var result = await _bookmarkService.DeleteAllBookmarksAsync(UserId);
            if (result.IsSuccess)
            {
                Tweets = new ();
            }

            IsVisibleButton = false;
        }

        #endregion
    }
}
