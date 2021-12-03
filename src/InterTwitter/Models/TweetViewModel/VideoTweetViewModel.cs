using System;
using System.Collections.Generic;
using System.Text;

namespace InterTwitter.Models.TweetViewModel
{
    public class VideoTweetViewModel : BaseTweetViewModel
    {
        #region -- Public properties --

        private string _videoPath;
        public string VideoPaths
        {
            get => _videoPath;
            set => SetProperty(ref _videoPath, value);
        }

        #endregion
    }
}
