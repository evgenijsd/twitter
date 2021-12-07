using Prism.Navigation;
using SkiaSharp;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace InterTwitter.ViewModels
{
    public class CreateTweetPageViewModel : BaseViewModel
    {
        private int value;

        public CreateTweetPageViewModel(INavigationService navigationService)
            : base(navigationService)
        {
            Text = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. A, neque, metus ipsum fermentum morbi at.";
            value = _text.Length;

            _listUploadPhotos = new List<MiniCardViewModel>()
            {
                new MiniCardViewModel()
                {
                    PathImage = "test_upload.png",
                    PathActionImage = "ic_clear_filled_blue.png",
                    ActionCommand = null,
                },
                new MiniCardViewModel()
                {
                    PathImage = "test_upload.png",
                    PathActionImage = "ic_clear_filled_blue.png",
                    ActionCommand = null,
                },
                new MiniCardViewModel()
                {
                    PathImage = "test_upload.png",
                    PathActionImage = "ic_clear_filled_blue.png",
                    ActionCommand = null,
                },
                new MiniCardViewModel()
                {
                    PathImage = "test_upload.png",
                    PathActionImage = "ic_clear_filled_blue.png",
                    ActionCommand = null,
                },
                new MiniCardViewModel()
                {
                    PathImage = "test_upload.png",
                    PathActionImage = "ic_clear_filled_blue.png",
                    ActionCommand = null,
                },
                new MiniCardViewModel()
                {
                    PathImage = "test_upload.png",
                    PathActionImage = "ic_clear_filled_blue.png",
                    ActionCommand = null,
                },
            };
        }

        #region -- Public properties --

        private string _text;
        public string Text
        {
            get => _text;
            set => SetProperty(ref _text, value);
        }

        private string _circleProgressBarText;
        public string CircleProgressBarText
        {
            get => _circleProgressBarText;
            set => SetProperty(ref _circleProgressBarText, value);
        }

        private Color _circleProgressBarTextColor;
        public Color CircleProgressBarTextColor
        {
            get => _circleProgressBarTextColor;
            set => SetProperty(ref _circleProgressBarTextColor, value);
        }

        private float _circleProgressBarFontScale;
        public float CircleProgressBarFontScale
        {
            get => _circleProgressBarFontScale;
            set => SetProperty(ref _circleProgressBarFontScale, value);
        }

        private SKTypefaceStyle _circleProgressBarFontAttributes;
        public SKTypefaceStyle CircleProgressBarFontAttributes
        {
            get => _circleProgressBarFontAttributes;
            set => SetProperty(ref _circleProgressBarFontAttributes, value);
        }

        private int _circleProgressBarvalue;
        public int CircleProgressBarValue
        {
            get => _circleProgressBarvalue;
            set => SetProperty(ref _circleProgressBarvalue, value);
        }

        private Color _circleProgressBarProgressLineColor = Color.Blue;
        public Color CircleProgressBarProgressLineColor
        {
            get => _circleProgressBarProgressLineColor;
            set => SetProperty(ref _circleProgressBarProgressLineColor, value);
        }

        private List<MiniCardViewModel> _listUploadPhotos;
        public List<MiniCardViewModel> ListUploadPhotos
        {
            get => _listUploadPhotos;
            set => SetProperty(ref _listUploadPhotos, value);
        }

        #endregion

        #region -- Overrides --

        protected override void OnPropertyChanged(PropertyChangedEventArgs args)
        {
            base.OnPropertyChanged(args);

            switch (args.PropertyName)
            {
                case nameof(Text):
                    Counter();
                    break;
            }
        }

        #endregion

        #region -- Private methods --

        private void Counter()
        {
            value = Text.Length;

            if (value == 251)
            {
                CircleProgressBarTextColor = Color.Red;
                CircleProgressBarFontScale = 0.8f;
                CircleProgressBarProgressLineColor = Color.Red;
            }

            if (value > 250)
            {
                CircleProgressBarText = (250 - value).ToString();
            }

            if (value == 300)
            {
                CircleProgressBarText = ":D";
                CircleProgressBarFontScale = 1;
                CircleProgressBarFontAttributes = SKTypefaceStyle.Bold;
            }

            CircleProgressBarValue = value;
        }

        #endregion
    }
}