using InterTwitter.Services.PermissionsService;
using Prism.Navigation;
using System.Collections.ObjectModel;
using System.Linq;

namespace InterTwitter.ViewModels
{
    public class VideoGalleryPageViewModel : BaseViewModel
    {
        private IPermissionsService _permissionsService;

        public VideoGalleryPageViewModel(
            INavigationService navigationService,
            IPermissionsService permissionsService)
            : base(navigationService)
        {
            _permissionsService = permissionsService;
        }

        #region -- Public properties --

        private string _videoSource;
        public string VideoSource
        {
            get => _videoSource;
            set => SetProperty(ref _videoSource, value);
        }

        #endregion

        #region -- INavigationAware implementation --

        public override void OnNavigatedTo(INavigationParameters parameters)
        {
            if (parameters.Count > 0)
            {
                if (parameters.ContainsKey(Constants.Messages.MEDIA))
                {
                    var list = parameters.GetValue<ObservableCollection<MiniCardViewModel>>(Constants.Messages.MEDIA);

                    VideoSource = list.FirstOrDefault().PathImage;
                }
            }
        }

        #endregion
    }
}
