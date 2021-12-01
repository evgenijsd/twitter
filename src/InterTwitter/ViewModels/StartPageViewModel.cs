using InterTwitter.Services.Registration;
using InterTwitter.Views;
using MapNotePad.Helpers;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace InterTwitter.ViewModels
{
    public class StartPageViewModel : BaseViewModel
    {
        private IRegistrationService _registrationService { get; }

        public StartPageViewModel(INavigationService navigationService, IRegistrationService registrationService)
            : base(navigationService)
        {
            _registrationService = registrationService;
        }

        #region -- Public properties --

        private ICommand _LogInCommand;
        public ICommand LogInCommand => _LogInCommand ??= SingleExecutionCommand.FromFunc(OnLogInCommandAsync);
        private ICommand _CreateCommand;
        public ICommand CreateCommand => _CreateCommand ??= SingleExecutionCommand.FromFunc(OnCreateCommandAsync);
        #endregion

        #region -- Overrides --
        #endregion

        #region -- Private helpers --

        private async Task OnLogInCommandAsync()
        {
            await _navigationService.NavigateAsync($"{nameof(LogInPage)}");
        }

        private async Task OnCreateCommandAsync()
        {
            await _navigationService.NavigateAsync($"{nameof(CreatePage)}");
        }
        #endregion
    }
}
