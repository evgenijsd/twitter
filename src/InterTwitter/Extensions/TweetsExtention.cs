using InterTwitter.Models;
using InterTwitter.Models.TweetViewModel;
using System;
using System.Collections.Generic;
using System.Text;

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

        #endregion
    }
}
