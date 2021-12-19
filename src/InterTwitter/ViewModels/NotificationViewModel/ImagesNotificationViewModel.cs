using System;

namespace InterTwitter.Models.NotificationViewModel
{
    public class ImagesNotificationViewModel : BaseNotificationViewModel
    {
        private readonly int _imagesNumber;

        public ImagesNotificationViewModel(int imagesNumber)
        {
            if (imagesNumber > 0)
            {
                _imagesNumber = imagesNumber;

                InitImagesPositioning();
            }
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
        private void InitImagesPositioning()
        {
            _rowHeight = _imagesNumber < 3 ? 186 : 80;

            _rowHeight = _imagesNumber == 3 | _imagesNumber == 4 ? 89 : _rowHeight;

            DefiningColumnNumber();
        }

        private void DefiningColumnNumber()
        {
            _columnNumber = _imagesNumber <= 4 ? 2 : 3;

            _columnNumber = _imagesNumber == 1 ? 1 : _columnNumber;
        }

        #endregion
    }
}
