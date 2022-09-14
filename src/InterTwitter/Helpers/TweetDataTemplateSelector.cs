﻿using InterTwitter.Enums;
using InterTwitter.Models.TweetViewModel;
using Xamarin.Forms;

namespace InterTwitter.Helpers
{
    public class TweetDataTemplateSelector : DataTemplateSelector
    {
        #region -- Public properties

        public DataTemplate VideoTweetDataTemplate { get; set; }

        public DataTemplate ImageTweetDataTemplate { get; set; }

        public DataTemplate TextTweetDataTemplate { get; set; }

        #endregion

        #region -- Overrides --

        protected override DataTemplate OnSelectTemplate(object item, BindableObject container)
        {
            DataTemplate dataTemplate = null;

            var tweet = item as BaseTweetViewModel;
            switch (tweet.Media)
            {
                case EAttachedMediaType.Photos:
                case EAttachedMediaType.Gif:
                    dataTemplate = ImageTweetDataTemplate;
                    break;
                case EAttachedMediaType.Video:
                    dataTemplate = VideoTweetDataTemplate;
                    break;
                default:
                    dataTemplate = TextTweetDataTemplate;
                    break;
            }

            return dataTemplate;
        }

        #endregion
    }
}
