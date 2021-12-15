using Prism.Navigation;
using System.Linq;

namespace InterTwitter.ViewModels.TweetFullPage
{
    public class ImagesFullPageViewModel : BaseTweetFullPageViewModel
    {
        public ImagesFullPageViewModel(
            INavigationService navigationService)
            : base(navigationService)
        {
        }

        #region -- Public properties --

        private byte _mediaPathNumber;
        private byte MediaPathNumber
        {
            get => _mediaPathNumber;
            set => SetProperty(ref _mediaPathNumber, value);
        }

        #endregion

        #region -- Overrides --
        public override void OnNavigatedTo(INavigationParameters parameters)
        {
            base.OnNavigatedTo(parameters);
            if (TweetViewModel != null)
            {
                MediaPathNumber = (byte)TweetViewModel.MediaPaths.Count();
            }
        }

        #endregion
    }
}
