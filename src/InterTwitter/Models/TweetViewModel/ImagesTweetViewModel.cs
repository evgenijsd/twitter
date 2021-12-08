using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Input;

namespace InterTwitter.Models.TweetViewModel
{
    public class ImagesTweetViewModel : BaseTweetViewModel
    {
        public ImagesTweetViewModel(
            UserModel userModel,
            TweetModel tweetModel)
            : base(userModel, tweetModel)
        {
            InitImagesPositioning(tweetModel);
        }

        #region -- Public properties --

        private int _rowHeight;
        public int RowHeight
        {
            get => _rowHeight;
            set => SetProperty(ref _rowHeight, value);
        }

        private int _columnNumber;
        public int ColumnNumber
        {
            get => _columnNumber;
            set => SetProperty(ref _columnNumber, value);
        }

        #endregion

        #region -- Private helpers --

        private void InitImagesPositioning(TweetModel tweet)
        {
            var imagesNumber = tweet.MediaPaths.Count();

            _rowHeight = imagesNumber < 3 ? 186 : 80;

            _rowHeight = imagesNumber == 3 | imagesNumber == 4 ? 89 : _rowHeight;

            DefiningColumnNumber(imagesNumber);
        }

        private void DefiningColumnNumber(int imagesNumber)
        {
            _columnNumber = imagesNumber <= 4 ? 2 : 3;

            _columnNumber = imagesNumber == 1 ? 1 : _columnNumber;
        }

        #endregion
    }
}
