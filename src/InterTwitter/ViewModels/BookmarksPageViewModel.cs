using InterTwitter.Enums;
using InterTwitter.Extensions;
using InterTwitter.Helpers;
using InterTwitter.Models.TweetViewModel;
using InterTwitter.Services;
using InterTwitter.Services.Settings;
using Prism.Events;
using Prism.Navigation;
using Prism.Services;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;

namespace InterTwitter.ViewModels
{
    public class BookmarksPageViewModel : BasePageViewModel
    {
        private readonly ITweetService _tweetService;
        private readonly IEventAggregator _event;
        private IPageDialogService _dialogs { get; }

        public BookmarksPageViewModel(
            ISettingsManager settingManager,
            IEventAggregator aggregator,
            ITweetService tweetService,
            IPageDialogService dialogs)
        {
            _tweetService = tweetService;
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
        public ICommand UnvisibleButtonCommand => _UnvisibleButtonCommand ??= SingleExecutionCommand.FromFunc(UnvisibleButtonCommandAsync);

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
            await InitAsync();
        }

        public override void Initialize(INavigationParameters parameters)
        {
            _event.GetEvent<DeleteBookmarkEvent>().Subscribe(DeleteTweet);
        }

        #endregion

        #region -- Private helpers --

        private async Task InitAsync()
        {
            int userid = 1;

            var getTweetResult = await _tweetService.GetAllTweetsAsync();
            var getBookmarks = _tweetService.GetBookmarksAsync(userid).Result.Result;

            if (getTweetResult.IsSuccess)
            {
                var tweetViewModels = new List<BaseTweetViewModel>(getTweetResult.Result.Where(x => getBookmarks.Any(y => y.TweetId == x.Id)).Select(x => x.Media == ETypeAttachedMedia.Photos || x.Media == ETypeAttachedMedia.Gif ? x.ToImagesTweetViewModel() : x.ToBaseTweetViewModel()).OrderBy(x => x.CreationTime));

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
                }

                Tweets = new ObservableCollection<BaseTweetViewModel>(tweetViewModels/*.Where(x => x.UserId == 1)*/);
                //Tweets.Remove(_tweets.FirstOrDefault(x => x.TweetId == 6));
                //_tweetService.DeleteBoormarkAsync(6);
            }
        }

        private void DeleteTweet(int tweetId)
        {
            //_dialogs.DisplayAlertAsync("Alert", $"Id post - {tweetId}", "Ok");
            Tweets = new (Tweets.Where(x => x.TweetId != tweetId));
            //implement logic
        }
        #endregion

        private Task OnVisibleButtonCommandAsync()
        {
            //await _navigationService.NavigateAsync("/MainPage");
            if (!IsNotFound)
            {
                IsVisibleButton = true;
            }

            return Task.CompletedTask;
        }

        private Task UnvisibleButtonCommandAsync()
        {
            //await _navigationService.NavigateAsync("/MainPage");
            IsVisibleButton = false;
            return Task.CompletedTask;
        }
    }
}
