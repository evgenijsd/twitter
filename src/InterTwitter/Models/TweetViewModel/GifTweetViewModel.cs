using System;
using System.Collections.Generic;
using System.Text;

namespace InterTwitter.Models.TweetViewModel
{
    public class GifTweetViewModel : BaseTweetViewModel
    {
        #region -- Public properties --

        private string _gifPath;
        public string GifPath
        {
            get => _gifPath;
            set => SetProperty(ref _gifPath, value);
        }

        #endregion
    }
}
