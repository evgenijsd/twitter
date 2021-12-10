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

        private bool _IsTweetLiked;
        public bool IsTweetLiked
        {
            get => _IsTweetLiked;
            set => SetProperty(ref _IsTweetLiked, value);
        }

        private ObservableCollection<BaseTweetViewModel> _tweets;

        public ObservableCollection<BaseTweetViewModel> Tweets
        {
            get => _tweets;
            set => SetProperty(ref _tweets, value);
        }

        #endregion

        #region -- Overrides --

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
            var getTweetResult = await _tweetService.GetAllTweetsAsync();

            if (getTweetResult.IsSuccess)
            {
                var tweetViewModels = new List<BaseTweetViewModel>(getTweetResult.Result.Select(x => x.Media == ETypeAttachedMedia.Photos || x.Media == ETypeAttachedMedia.Gif ? x.ToImagesTweetViewModel() : x.ToBaseTweetViewModel()).OrderBy(x => x.CreationTime));

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
            _dialogs.DisplayAlertAsync("Alert", $"Id post - {tweetId}", "Ok");
            Tweets.Remove(_tweets.FirstOrDefault(x => x.TweetId == tweetId));
            //implement logic
        }
        #endregion
    }
}
