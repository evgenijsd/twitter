using InterTwitter.Helpers;
using Prism.Navigation;
using Xamarin.Forms;

namespace InterTwitter.ViewModels
{
    public class BaseTabViewModel : BaseViewModel, IPageActionsHandler
    {
        public BaseTabViewModel(INavigationService navigationService)
            : base(navigationService)
        {
        }

        #region --- Public Properties ---

        private ImageSource _iconPath;
        public ImageSource IconPath
        {
            get => _iconPath;
            set => SetProperty(ref _iconPath, value);
        }

        #endregion

        #region -- IActiveAware implementation --
        public virtual void OnAppearing()
        {
        }

        public virtual void OnDisappearing()
        {
        }
        #endregion
    }
}
