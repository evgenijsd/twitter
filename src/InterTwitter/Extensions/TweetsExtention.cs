using InterTwitter.Models;
using InterTwitter.Models.TweetViewModel;
using System.Linq;

namespace InterTwitter.Extensions
{
    public static class TweetsExtention
    {
        #region -- Public methods --

        public static BaseTweetViewModel ToBaseTweetViewModel(this TweetModel tweetModel) => new BaseTweetViewModel
        {
            TweetId = tweetModel.Id,
            UserId = tweetModel.UserId,
            Text = tweetModel.Text,
            Media = tweetModel.Media,
            MediaPaths = tweetModel.MediaPaths,
            CreationTime = tweetModel.CreationTime,
        };

        public static BaseTweetViewModel ToImagesTweetViewModel(this TweetModel tweetModel) => new ImagesTweetViewModel(tweetModel.MediaPaths.Count())
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
