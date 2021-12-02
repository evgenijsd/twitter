using InterTwitter.Helpers;
using InterTwitter.Models.TweetViewModel;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace InterTwitter.ViewModels
{
    public class HomePageViewModel : BaseViewModel
    {
        #region -- Public properties --

        private List<BaseTweetViewModel> tweetList = new List<BaseTweetViewModel>();

        private ObservableCollection<BaseTweetViewModel> _tweets;

        public ObservableCollection<BaseTweetViewModel> Tweets
        {
            get => _tweets;
            set => SetProperty(ref _tweets, value);
        }

        private ICommand _likeButtonCommand;
        public ICommand LikeButtonCommand => _likeButtonCommand ?? (_likeButtonCommand = SingleExecutionCommand.FromFunc<BaseTweetViewModel>(OnLikeCommandAsync));

        private ICommand _markTweetButtonCommand;
        public ICommand MarkTweetButtonCommand => _markTweetButtonCommand ?? (_markTweetButtonCommand = SingleExecutionCommand.FromFunc<BaseTweetViewModel>(OnMarkCommandAsync));

        #endregion

        #region -- Overrides --

        public override void OnNavigatedTo(INavigationParameters parameters)
        {
            InitAsync();
        }

        #endregion

        #region -- Private helpers --
        private Task InitAsync()
        {
            var observTweetCollection = new ObservableCollection<BaseTweetViewModel>();

            observTweetCollection.Add(new BaseTweetViewModel()
            {
                UserName = "Mike",
                UserAvatar = "man",
                Text = "In continuation of my video about DataTemplates, we will now look at the DataTemplateSelector. With the DataTemplateSelector you can apply different DataTemplateSelector. With the DataTemplateSelector templates based on logic that you implement yourself! ",
                Images = new List<string> { "image1.png", "image2", "image3", "image4", "image5" },
                Media = "VideoTweet",
            });

            observTweetCollection.Add(new BaseTweetViewModel()
            {
                UserName = "Kate",
                UserAvatar = "woman2",
                Text = "In continuation of my video about DataTemplates, we will now look at the DataTemplateSelector. With the DataTemplateSelector you can apply different templates based on logic that you implement yourself! ",
                Images = new List<string> { "image1.png", "image2", "image3" },
                Media = "ImageTweet",
            });

            //observTweetCollection.Add(new BaseTweetViewModel()
            //{
            //    UserName = "Bob",
            //    Text = "In continuation of my video about DataTemplates, we will now look at the DataTemplateSelector. With the DataTemplateSelector you can apply different templates based on logic that you implement yourself! ",
            //    Media = "GifTweet",
            //});

            //observTweetCollection.Add(new BaseTweetViewModel()
            //{
            //    UserName = "Tom",
            //    Text = "In continuation of my video about DataTemplates, we will now look at the DataTemplateSelector. With the DataTemplateSelector you can apply different templates based on logic that you implement yourself! ",
            //    Media = "TextTweet",
            //});
            Tweets = observTweetCollection;

            return Task.CompletedTask;
        }

        private Task OnMarkCommandAsync(BaseTweetViewModel tweet)
        {
            throw new NotImplementedException();
        }

        private Task OnLikeCommandAsync(BaseTweetViewModel tweet)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
