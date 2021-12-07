using System;
using System.Collections.Generic;
using System.Text;

namespace InterTwitter.Models.TweetViewModel
{
    public class VideoTweetViewModel : BaseTweetViewModel
    {
        #region -- Public properties --

        private string _videoPath;
        public string VideoPath
        {
            get => _videoPath;
            set => SetProperty(ref _videoPath, value);
        }

        #endregion
    }
}
