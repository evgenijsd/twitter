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
    public class CreatePageViewModel : BaseViewModel
    {
        public CreatePageViewModel(INavigationService navigationService)
            : base(navigationService)
        {
        }

        #region -- Public properties --

        private ICommand _StartCommand;
        public ICommand StartCommand => _StartCommand ??= SingleExecutionCommand.FromFunc(OnStartCommandAsync);
        private ICommand _PasswordCommand;
        public ICommand PasswordCommand => _PasswordCommand ??= SingleExecutionCommand.FromFunc(OnPasswordCommandAsync);
        #endregion

        #region -- Overrides --
        #endregion

        #region -- Private helpers --

        private async Task OnStartCommandAsync()
        {
            await _navigationService.GoBackAsync();
        }

        private async Task OnPasswordCommandAsync()
        {
            await _navigationService.NavigateAsync($"{nameof(PasswordPage)}");
        }
        #endregion
    }
}
