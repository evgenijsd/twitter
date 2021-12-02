using Prism.Navigation;
using SkiaSharp;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace InterTwitter.ViewModels
{
    public class CreateTweetPageViewModel : BaseViewModel
    {
        public CreateTweetPageViewModel(INavigationService navigationService)
            : base(navigationService)
        {
        }

        #region -- Public properties --

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

        private Color _circleProgressBarProgressLineColor;
        public Color CircleProgressBarProgressLineColor
        {
            get => _circleProgressBarProgressLineColor;
            set => SetProperty(ref _circleProgressBarProgressLineColor, value);
        }

        public ICommand OnRefresh => new Command(Refresh);

        #endregion

        #region -- Private methods --

        private int value = 3;
        private void Refresh(object obj)
        {
            if (value == 3)
            {
                CircleProgressBarProgressLineColor = Color.Blue;
            }

            if (value == 33)
            {
                CircleProgressBarTextColor = Color.Red;
                CircleProgressBarFontScale = 0.8f;
                CircleProgressBarProgressLineColor = Color.Red;
            }

            if (value > 30 && value < 60)
            {
                CircleProgressBarText = (30 - value).ToString();
            }

            if (value > 60)
            {
                CircleProgressBarText = ":D";
                CircleProgressBarFontScale = 1;
                CircleProgressBarFontAttributes = SKTypefaceStyle.Bold;
            }

            CircleProgressBarValue = value;

            value += 3;
        }

        #endregion

    }
}