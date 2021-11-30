using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

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
        public void Destroy()
        {
        }

        public void Initialize(INavigationParameters parameters)
        {
        }

        public void OnNavigatedFrom(INavigationParameters parameters)
        {
        }

        public void OnNavigatedTo(INavigationParameters parameters)
        {
        }
        #endregion
    }
}
