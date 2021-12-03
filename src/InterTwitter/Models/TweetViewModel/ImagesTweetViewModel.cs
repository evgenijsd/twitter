using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

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

        private int _rowHeight;
        public int RowHeight
        {
            get => _rowHeight;
            set => SetProperty(ref _rowHeight, value);
        }

        private int _countColumn;
        public int CountColumn
        {
            get => _countColumn;
            set => SetProperty(ref _countColumn, value);
        }

        #endregion
    }
}
