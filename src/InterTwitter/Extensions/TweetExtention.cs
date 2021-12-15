using InterTwitter.Models;
using InterTwitter.ViewModels.TweetViewModel;
using Prism.Navigation;
using System.Linq;

namespace InterTwitter.Extensions
{
    public static class TweetExtention
    {
        #region -- Public methods --

        public static BaseTweetViewModel ToBaseTweetViewModel(this TweetModel tweetModel, INavigationService navigationService) => new BaseTweetViewModel(navigationService)
        {
            TweetId = tweetModel.Id,
            UserId = tweetModel.UserId,
            Text = tweetModel.Text,
            Media = tweetModel.Media,
            MediaPaths = tweetModel.MediaPaths,
            CreationTime = tweetModel.CreationTime,
        };

        public static BaseTweetViewModel ToImagesTweetViewModel(this TweetModel tweetModel, INavigationService navigationService) => new ImagesTweetViewModel(navigationService, tweetModel.MediaPaths.Count())
        {
            TweetId = tweetModel.Id,
            UserId = tweetModel.UserId,
            Text = tweetModel.Text,
            Media = tweetModel.Media,
            MediaPaths = tweetModel.MediaPaths,
            CreationTime = tweetModel.CreationTime,
        };

        #endregion
    }
}
