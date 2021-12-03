using System;
using System.Collections.Generic;
using System.Text;

namespace InterTwitter.Models.TweetViewModel
{
    public class ImagesTweetViewModel : BaseTweetViewModel
    {
        #region -- Public properties --

        private List<string> _imagesPaths;
        public List<string> ImagesPaths
        {
            get => _imagesPaths;
            set => SetProperty(ref _imagesPaths, value);
        }

        #endregion
    }
}
