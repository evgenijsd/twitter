using Prism.Mvvm;
using Prism.Navigation;

namespace InterTwitter.ViewModels
{
    public class BaseViewModel : BindableBase
    {
        public BaseViewModel(INavigationService navigationService)
        {
            NavigationService = navigationService;
        }

        #region -- Public properies --

        protected INavigationService NavigationService { get; private set; }

        #endregion
    }
}
