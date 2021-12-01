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

        public DataTemplate VideoTemplate { get; set; }

        public DataTemplate GifTemplate { get; set; }

        public DataTemplate ImageTemplate { get; set; }

        public DataTemplate TextTemplate { get; set; }

        #endregion

        #region -- Overrides --
        protected override DataTemplate OnSelectTemplate(object item, BindableObject container)
        {
            var tweet = item as BaseTweetViewModel;

            if (tweet?.Media == TweetType.VideoTweet.ToString())
            {
                return VideoTemplate;
            }
            else if (tweet?.Media == TweetType.ImageTweet.ToString())
            {
                return ImageTemplate;
            }
            else if (tweet?.Media == TweetType.GifTweet.ToString())
            {
                return GifTemplate;
            }

            return TextTemplate;
        }

        #endregion
    }
}
