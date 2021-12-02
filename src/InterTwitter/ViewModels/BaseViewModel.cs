using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace InterTwitter.ViewModels
{
    public class BaseViewModel : BindableBase, IDestructible, IInitializeAsync, IInitialize, INavigationAware
    {
        protected readonly INavigationService _navigationService;

        public BaseViewModel()
        {
            NavigationService = App.Resolve<INavigationService>();
        }

        protected INavigationService NavigationService { get; }

        #region -- IDestructible implementation --

        public void Destroy()
        {
        }

        #endregion

        #region -- IInitializeAsync implementation --

        public async virtual Task InitializeAsync(INavigationParameters parameters)
        {
        }

        #endregion

        #region -- IInitialize implementation --

        public void Initialize(INavigationParameters parameters)
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