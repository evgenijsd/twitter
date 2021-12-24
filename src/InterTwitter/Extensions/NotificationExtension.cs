using InterTwitter.Models.NotificationViewModel;
using System.Linq;

namespace InterTwitter.Extensions
{
    public static class NotificationExtension
    {
        #region -- Public methods --

        public static BaseNotificationViewModel ToBaseNotificationViewModel(this BaseNotificationViewModel tweet) => new BaseNotificationViewModel
        {
            TweetId = tweet.TweetId,
            UserId = tweet.UserId,
            CreationTime = tweet.CreationTime,
            UserAvatar = tweet.UserAvatar,
            UserName = tweet.UserName,
            Text = tweet.Text,
            MediaPaths = tweet.MediaPaths,
            Media = tweet.Media,
            NotificationIcon = tweet.NotificationIcon,
            NotificationText = tweet.NotificationText,
        };

        public static BaseNotificationViewModel ToImagesNotificationViewModel(this BaseNotificationViewModel tweet) => new ImagesNotificationViewModel(tweet.MediaPaths.Count())
        {
            TweetId = tweet.TweetId,
            UserId = tweet.UserId,
            CreationTime = tweet.CreationTime,
            UserAvatar = tweet.UserAvatar,
            UserName = tweet.UserName,
            Text = tweet.Text,
            MediaPaths = tweet.MediaPaths,
            Media = tweet.Media,
            NotificationIcon = tweet.NotificationIcon,
            NotificationText = tweet.NotificationText,
        };

        #endregion
    }
}
