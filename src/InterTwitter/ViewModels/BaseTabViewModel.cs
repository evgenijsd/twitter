using InterTwitter.Helpers;
using Prism.Navigation;

namespace InterTwitter.ViewModels
{
    public class BaseTabViewModel : BaseViewModel, IPageActionsHandler
    {
        public BaseTabViewModel(INavigationService navigationService)
                                                  : base(navigationService)
        {
        }

        #region --- Public Properties ---

        private string _iconPath;
        public string IconPath
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
