using InterTwitter.Enums;
using InterTwitter.Extensions;
using InterTwitter.Helpers;
using InterTwitter.Models.TweetViewModel;
using InterTwitter.Services;
using InterTwitter.Services.Settings;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace InterTwitter.ViewModels
{
    public class HomePageViewModel : BaseViewModel
    {
        private readonly ISettingsManager _settingsManager;
        private readonly ITweetService _tweetService;

        public HomePageViewModel(
            ISettingsManager settingManager,
            ITweetService tweetService)
        {
            _settingsManager = settingManager;
            _tweetService = tweetService;
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

        #endregion

        #region -- Private helpers --

        private async Task InitAsync()
        {
            var getTweetResult = await _tweetService.GetAllTweetsAsync();

            if (getTweetResult.IsSuccess)
            {
                var tweetViewModels = new List<BaseTweetViewModel>(getTweetResult.Result.Select(x => x.Media == ETweetType.ImagesTweet || x.Media == ETweetType.GifTweet ? x.ToImagesTweetViewModel() : x.ToBaseTweetViewModel()));

                foreach (var tweet in tweetViewModels)
                {
                    var tweetAuthor = await _tweetService.GetUserAsync(tweet.UserId);

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

        #endregion
    }
}
