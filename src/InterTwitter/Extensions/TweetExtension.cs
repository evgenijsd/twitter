using InterTwitter.Models;
using InterTwitter.ViewModels.TweetViewModel;
using Prism.Navigation;
using System.Linq;

namespace InterTwitter.Extensions
{
    public static class TweetExtension
    {
        #region -- Public methods --

        public static BaseTweetViewModel ToBaseTweetViewModel(this TweetModel tweet) => new BaseTweetViewModel()
        {
            TweetId = tweet.Id,
            UserId = tweet.UserId,
            Text = tweet.Text,
            Media = tweet.Media,
            MediaPaths = tweet.MediaPaths,
            CreationTime = tweet.CreationTime,
        };

        public static BaseTweetViewModel ToImagesTweetViewModel(this TweetModel tweet) => new ImagesTweetViewModel(tweet.MediaPaths.Count())
        {
            TweetId = tweet.Id,
            UserId = tweet.UserId,
            Text = tweet.Text,
            Media = tweet.Media,
            MediaPaths = tweet.MediaPaths,
            CreationTime = tweet.CreationTime,
        };

        public static TweetModel ToTweetModel(this BaseTweetViewModel tweet) => new TweetModel
        {
            Id = tweet.TweetId,
            UserId = tweet.UserId,
            Text = tweet.Text,
            Media = tweet.Media,
            MediaPaths = tweet.MediaPaths,
            CreationTime = tweet.CreationTime,
        };

        #endregion
    }
}
