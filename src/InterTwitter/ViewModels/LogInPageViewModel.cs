using InterTwitter.Views;
using MapNotePad.Helpers;
using Prism.Navigation;
using System.Threading.Tasks;
using System.Windows.Input;

namespace InterTwitter.ViewModels
{
    public class LogInPageViewModel : BaseViewModel
    {
        public LogInPageViewModel(INavigationService navigationService)
            : base(navigationService)
        {
        }

        #region -- Public properties --

        private ICommand _StartCommand;
        public ICommand StartCommand => _StartCommand ??= SingleExecutionCommand.FromFunc(OnStartCommandAsync);
        private ICommand _TwitterCommand;
        public ICommand TwitterCommand => _TwitterCommand ??= SingleExecutionCommand.FromFunc(OnTwitterCommandAsync);
        #endregion

        #region -- Overrides --
        #endregion

        #region -- Private helpers --

        private async Task OnStartCommandAsync()
        {
            await _navigationService.GoBackAsync();
        }

        private async Task OnTwitterCommandAsync()
        {
            await _navigationService.NavigateAsync($"/{nameof(StartPage)}");
        }
        #endregion
    }
}
