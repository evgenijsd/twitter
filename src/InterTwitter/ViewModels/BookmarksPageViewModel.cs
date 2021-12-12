using InterTwitter.Enums;
using InterTwitter.Extensions;
using InterTwitter.Helpers;
using InterTwitter.Models.TweetViewModel;
using InterTwitter.Services;
using InterTwitter.Services.BookmarkService;
using InterTwitter.Services.Settings;
using InterTwitter.Views;
using Prism.Events;
using Prism.Navigation;
using Prism.Services;
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
        private readonly IEventAggregator _event;
        private IPageDialogService _dialogs { get; }

        public BookmarksPageViewModel(
            INavigationService navigationService,
            ISettingsManager settingManager,
            IEventAggregator aggregator,
            ITweetService tweetService,
            IBookmarkService bookmarkService,
            IPageDialogService dialogs)
            : base(navigationService)
        {
            IconPath = Prism.PrismApplicationBase.Current.Resources["ic_bookmarks_gray"] as ImageSource;
            _tweetService = tweetService;
            _bookmarkService = bookmarkService;
            _event = aggregator;
            _dialogs = dialogs;
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

        private int _userId = 1;
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

        private ICommand _GoBackCommand;
        public ICommand GoBackCommand => _GoBackCommand ??= SingleExecutionCommand.FromFunc(OnGoBackCommandAsync);

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

        public override async void OnNavigatedTo(INavigationParameters parameters)
        {
            int userid = 1;
            UserId = userid;

            await Task.Delay(TimeSpan.FromSeconds(0.1));

            var resultTweet = _tweetService.GetAllTweetsAsync().Result;
            var resultBookmark = _bookmarkService.GetBookmarksAsync(userid).Result;
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
                    }

                    tweet.IsBookmarked = true;
                    tweet.CurrentUserId = UserId;
                }

                Tweets = new ObservableCollection<BaseTweetViewModel>(tweetViewModels);
            }
        }

        public override void Initialize(INavigationParameters parameters)
        {
            _event.GetEvent<DeleteBookmarkEvent>().Subscribe(DeleteTweet);
        }

        #endregion

        #region -- Private helpers --

        private Task OnOpenFlyoutCommandAsync()
        {
            MessagingCenter.Send(this, Constants.Messages.OPEN_SIDEBAR, true);
            MessagingCenter.Send(this, Constants.Messages.TAB_CHANGE, typeof(BookmarksPage));
            return Task.CompletedTask;
        }

        private void DeleteTweet(int tweetId)
        {
            Tweets = new (Tweets.Where(x => x.TweetId != tweetId));
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

        private async Task OnGoBackCommandAsync()
        {
            await NavigationService.GoBackAsync();
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
