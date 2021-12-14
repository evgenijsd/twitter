using InterTwitter.Services.EnvironmentService;
using InterTwitter.Services.PermissionsService;
using MapNotepad.Helpers;
using Prism.Navigation;
using System.Collections.ObjectModel;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;

namespace InterTwitter.ViewModels
{
    public class VideoGalleryPageViewModel : BaseViewModel
    {
        private IPermissionsService _permissionsService;

        private IEnvironmentService _environmentService;

        private bool status;

        private Color color;

        public VideoGalleryPageViewModel(
            INavigationService navigationService,
            IPermissionsService permissionsService,
            IEnvironmentService environmentService)
            : base(navigationService)
        {
            _permissionsService = permissionsService;
            _environmentService = environmentService;
        }

        #region -- Public properties --

        private string _videoSource;
        public string VideoSource
        {
            get => _videoSource;
            set => SetProperty(ref _videoSource, value);
        }

        private ICommand _goBackCommand;
        public ICommand GoBackCommand => _goBackCommand = SingleExecutionCommand.FromFunc(OnGoBackCommandAsync);

        #endregion

        #region -- INavigationAware implementation --

        public async override void OnNavigatedTo(INavigationParameters parameters)
        {
            if (parameters.Count > 0)
            {
                if (parameters.ContainsKey(Constants.Messages.MEDIA))
                {
                    var list = parameters.GetValue<ObservableCollection<MiniCardViewModel>>(Constants.Messages.MEDIA);

                    if (list.FirstOrDefault() != null)
                    {
                        VideoSource = list.FirstOrDefault().PathImage;
                    }
                }

                status = _environmentService.GetUseDarkStatusBarTint();
                color = _environmentService.GetStatusBarColor();

                _environmentService.SetStatusBarColor(Color.Green, false);
            }

            status = _environmentService.GetUseDarkStatusBarTint();
            color = _environmentService.GetStatusBarColor();

            _environmentService.SetStatusBarColor(Color.Green, false);
        }

        #endregion

        #region -- Private methods --

        private async Task OnGoBackCommandAsync()
        {
            _environmentService.SetStatusBarColor(color, status);

            await _navigationService.GoBackAsync();
        }

        #endregion
    }
}
