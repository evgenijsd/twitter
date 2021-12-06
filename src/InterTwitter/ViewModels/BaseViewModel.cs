using Prism.Mvvm;
using Prism.Navigation;

namespace InterTwitter.ViewModels
{
    public class BaseViewModel : BindableBase, IDestructible, IInitialize, INavigationAware
    {
        protected INavigationService _navigationService { get; }

        public BaseViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;
        }

        #region -- InterfaceName implementation --
        public virtual void Destroy()
        {
        }

        public virtual void Initialize(INavigationParameters parameters)
        {
        }

        public virtual void OnNavigatedFrom(INavigationParameters parameters)
        {
        }

        public virtual void OnNavigatedTo(INavigationParameters parameters)
        {
        }
        #endregion
    }
}
