using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Text;
using System.Threading.Tasks;

namespace InterTwitter.ViewModels
{
    public class BasePageViewModel : BindableBase, IDestructible, IInitializeAsync, IInitialize, INavigationAware
    {
        protected INavigationService _navigationService { get; }

        public BasePageViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;
        }

        protected INavigationService NavigationService { get; }

        #region -- IDestructible implementation --

        public virtual void Destroy()
        {
        }

        #endregion

        #region -- IInitializeAsync implementation --

        public async virtual Task InitializeAsync(INavigationParameters parameters)
        {
        }

        #endregion

        #region -- IInitialize implementation --

        public virtual void Initialize(INavigationParameters parameters)
        {
        }

        #endregion

        #region -- INavigationAware implementation --

        public virtual void OnNavigatedFrom(INavigationParameters parameters)
        {
        }

        public virtual void OnNavigatedTo(INavigationParameters parameters)
        {
        }

        #endregion

    }
}
