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
    public class PasswordPageViewModel : BaseViewModel
    {
        public PasswordPageViewModel(INavigationService navigationService)
            : base(navigationService)
        {
        }

        #region -- Public properties --

        private ICommand _CreateCommand;
        public ICommand CreateCommand => _CreateCommand ??= SingleExecutionCommand.FromFunc(OnCreateCommandAsync);
        private ICommand _StartCommand;
        public ICommand StartCommand => _StartCommand ??= SingleExecutionCommand.FromFunc(OnStartCommandAsync);
        #endregion

        #region -- Overrides --
        #endregion

        #region -- Private helpers --

        private async Task OnCreateCommandAsync()
        {
            await _navigationService.GoBackAsync();
        }

        private async Task OnStartCommandAsync()
        {
            await _navigationService.NavigateAsync($"/{nameof(StartPage)}");
        }
        #endregion
    }
}
