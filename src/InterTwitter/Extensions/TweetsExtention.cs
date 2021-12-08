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

        public static BaseTweetViewModel ToViewModel(this TweetModel tweetModel, UserModel userModel)
        {
            BaseTweetViewModel tweetViewModel = null;

            if (tweetModel != null && userModel != null)
            {
                if (tweetModel.Media == Enums.TweetType.ImagesTweet)
                {
                    tweetViewModel = new ImagesTweetViewModel(userModel, tweetModel);
                }
                else
                {
                    tweetViewModel = new BaseTweetViewModel(userModel, tweetModel);
                }
            }

            return tweetViewModel;
        }

        #endregion
    }
}
