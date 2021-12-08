using InterTwitter.Enums;
using InterTwitter.Models.TweetViewModel;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace InterTwitter.Helpers
{
    public class TweetDataTemplateSelector : DataTemplateSelector
    {
        #region -- Public properties

        public DataTemplate VideoTweetDataTemplate { get; set; }

        public DataTemplate GifTweetDataTemplate { get; set; }

        public DataTemplate ImageTweetDataTemplate { get; set; }

        public DataTemplate TextTweetDataTemplate { get; set; }

        #endregion

        #region -- Overrides --

        protected override DataTemplate OnSelectTemplate(object item, BindableObject container)
        {
            DataTemplate dataTemplate = null;

            var tweet = item as BaseTweetViewModel;
            switch (tweet.TweetModel.Media)
            {
                case TweetType.ImagesTweet:
                case TweetType.GifTweet:
                     dataTemplate = ImageTweetDataTemplate;
                    break;
                case TweetType.VideoTweet:
                    dataTemplate = VideoTweetDataTemplate;
                    break;
                default:
                    dataTemplate = TextTweetDataTemplate;
                    break;
            }

            return dataTemplate;

            //if (tweet.Media.CompareTo(TweetType.VideoTweet) == 0)
            //{
            //    return VideoTweetDataTemplate;
            //}
            //else if (tweet.Media == TweetType.ImageTweet.ToString())
            //{
            //    return ImageTweetDataTemplate;
            //}
            //else if (tweet?.Media == TweetType.GifTweet.ToString())
            //{
            //    return GifTweetDataTemplate;
            //}

            //return TextTweetDataTemplate;
        }

        #endregion
    }
}
