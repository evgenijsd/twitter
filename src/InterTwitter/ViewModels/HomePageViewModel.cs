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
                Tweets = new ObservableCollection<BaseTweetViewModel>(getTweetResult.Result.Select(x => x.ToBaseTweetViewModel()));
            }

            int rt = 5;
            //var listPhotos = new List<string> { "gif1", };
            //var rowHeight = listPhotos.Count < 3 ? 186 : 80; //error there pixel
            //rowHeight = listPhotos.Count == 3 | listPhotos.Count == 4 ? 89 : rowHeight; //error there pixel

            //var countColumn = listPhotos.Count <= 4 ? 2 : 3;
            //countColumn = listPhotos.Count == 1 ? 1 : countColumn;

            //observTweetCollection.Add(new ImagesTweetViewModel()
            //{
            //    UserName = "GifOneImage",
            //    UserAvatar = "man",
            //    Text = "In continuation of my video about DataTemplates, we will now look at the DataTemplateSelector. With the DataTemplateSelector you can apply different DataTemplateSelector. With the DataTemplateSelector templates based on logic that you implement yourself! ",
            //    TweetType = TweetType.GifTweet,
            //    ImagesPaths = listPhotos,
            //    RowHeight = rowHeight,
            //    CountColumn = countColumn,
            //    IsTweekLiked = false,
            //    CreationTime = DateTime.Now,
            //});

            //listPhotos = new List<string> { "image1", };
            //rowHeight = listPhotos.Count < 3 ? 186 : 80; //error there pixel
            //rowHeight = listPhotos.Count == 3 | listPhotos.Count == 4 ? 89 : rowHeight; //error there pixel

            //countColumn = listPhotos.Count <= 4 ? 2 : 3;
            //countColumn = listPhotos.Count == 1 ? 1 : countColumn;

            //observTweetCollection.Add(new VideoTweetViewModel()
            //{
            //    UserName = "Mike",
            //    UserAvatar = "man",
            //    Text = "In continuation of my video about DataTemplates, we will now look at the DataTemplateSelector. With the DataTemplateSelector you can apply difft DataTemplateSelector. With the DataTemplateSelector templates based on logic that you implement yourself! ",
            //    TweetType = TweetType.VideoTweet,
            //    VideoPath = "https://www.youtube.com/embed/mcHt8L6CAB8",
            //    IsTweekLiked = false,
            //    CreationTime = DateTime.Now,
            //});

            //observTweetCollection.Add(new GifTweetViewModel()
            //{
            //    UserName = "Test Gif",
            //    UserAvatar = "man",
            //    Text = "In continuation of my video about DataTemplates, we will now look at the DataTemplateSelector. With the DataTemplateSelector you can apply different DataTemplateSelector. With the DataTemplateSelector templates based on logic that you implement yourself! ",
            //    TweetType = TweetType.GifTweet,
            //    GifPath = "gif1",
            //    IsTweekLiked = false,
            //    CreationTime = DateTime.Now,
            //});

            //listPhotos = new List<string> { "image2", "image3", "image4", "image5", "image1", };
            //rowHeight = listPhotos.Count < 3 ? 186 : 80; //error there pixel
            //rowHeight = listPhotos.Count == 3 | listPhotos.Count == 4 ? 89 : rowHeight; //error there pixel

            //countColumn = listPhotos.Count <= 4 ? 2 : 3;
            //countColumn = listPhotos.Count == 1 ? 1 : countColumn;

            //observTweetCollection.Add(new ImagesTweetViewModel()
            //{
            //    UserName = "Mike",
            //    UserAvatar = "man",
            //    Text = "In continuation of my video about DataTemplates, we will now look at the DataTemplateSelector. With the DataTemplateSelector you can apply different DataTemplateSelector. With the DataTemplateSelector templates based on logic that you implement yourself! ",
            //    TweetType = TweetType.ImagesTweet,
            //    ImagesPaths = listPhotos,
            //    RowHeight = rowHeight,
            //    CountColumn = countColumn,
            //    IsTweekLiked = false,
            //    CreationTime = DateTime.Now,
            //});

            //listPhotos = new List<string> { "image2", "image3", "image4", "image5", "image6", "image1", };
            //rowHeight = listPhotos.Count < 3 ? 186 : 80; //error there pixel
            //rowHeight = listPhotos.Count == 3 | listPhotos.Count == 4 ? 89 : rowHeight; //error there pixel

            //countColumn = listPhotos.Count <= 4 ? 2 : 3;
            //countColumn = listPhotos.Count == 1 ? 1 : countColumn;

            //observTweetCollection.Add(new ImagesTweetViewModel()
            //{
            //    UserName = "Mike",
            //    UserAvatar = "man",
            //    Text = "In continuation of my video about DataTemplates, we will now look at the DataTemplateSelector. With the DataTemplateSelector you can apply different DataTemplateSelector. With the DataTemplateSelector templates based on logic that you implement yourself! ",
            //    TweetType = TweetType.ImagesTweet,
            //    ImagesPaths = listPhotos,
            //    RowHeight = rowHeight,
            //    CountColumn = countColumn,
            //    IsTweekLiked = false,
            //    CreationTime = DateTime.Now,
            //});

            //observTweetCollection.Add(new BaseTweetViewModel()
            //{
            //    UserName = "Kate Text Tweet",
            //    UserAvatar = "woman2",
            //    Text = "In continuation of my video about DataTemplates, we will now look at the DataTemplateSelector. With the DataTemplateSelector you can apply different templates based on logic that you implement yourself! ",
            //    IsTweekLiked = false,
            //    CreationTime = DateTime.Now,
            //});
        }

        #endregion
    }
}
