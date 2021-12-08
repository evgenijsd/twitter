using Prism.Mvvm;
using Prism.Navigation;
using System.Threading.Tasks;

namespace InterTwitter.ViewModels
{
    public class BaseViewModel : BindableBase, IInitialize, INavigationAware, IInitializeAsync, IDestructible
    {
        public BaseViewModel(INavigationService navigationService)
        {
            NavigationService = navigationService;
        }

        protected INavigationService NavigationService { get; }

        #region --- IDestructible implementation ---

        public void Destroy()
        {
        }

        #endregion

        #region --- IInitialize, IInitializeAsync implementation ---

        public void Initialize(INavigationParameters parameters)
        {
        }

        public Task InitializeAsync(INavigationParameters parameters)
        {
            return Task.CompletedTask;
        }

        #endregion

        #region --- INavigationAware implementation ---

        public void OnNavigatedFrom(INavigationParameters parameters)
        {
        }

        public void OnNavigatedTo(INavigationParameters parameters)
        {
        }

        #endregion
    }
}
