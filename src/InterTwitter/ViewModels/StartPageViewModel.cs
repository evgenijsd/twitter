using InterTwitter.Enums;
using InterTwitter.Services.Registration;
using InterTwitter.Views;
using MapNotePad.Helpers;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.ComponentModel;
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
        private string _name = string.Empty;
        public string Name
        {
            get => _name;
            set => SetProperty(ref _name, value);
        }

        private bool _isWrongName = false;
        public bool IsWrongName
        {
            get => _isWrongName;
            set => SetProperty(ref _isWrongName, value);
        }

        private bool _isWrongEmail = false;
        public bool IsWrongEmail
        {
            get => _isWrongEmail;
            set => SetProperty(ref _isWrongEmail, value);
        }

        private string _email = string.Empty;
        public string Email
        {
            get => _email;
            set => SetProperty(ref _email, value);
        }

        private ICommand _LogInCommand;
        public ICommand LogInCommand => _LogInCommand ??= SingleExecutionCommand.FromFunc(OnLogInCommandAsync);
        private ICommand _CreateCommand;
        public ICommand CreateCommand => _CreateCommand ??= SingleExecutionCommand.FromFunc(OnCreateCommandAsync);
        #endregion

        #region -- Overrides --
        protected override void OnPropertyChanged(PropertyChangedEventArgs args)
        {
            base.OnPropertyChanged(args);

            if (args.PropertyName == nameof(Name))
            {
                IsWrongName = _registrationService.CheckCorrectName(Name) != ECheckEnter.ChecksArePassed;
                if (string.IsNullOrEmpty(Name))
                {
                    IsWrongName = false;
                }
            }

            if (args.PropertyName == nameof(Email))
            {
                IsWrongEmail = _registrationService.CheckCorrectEmail(Email) != ECheckEnter.ChecksArePassed;
                if (string.IsNullOrEmpty(Email))
                {
                    IsWrongEmail = false;
                }
            }
        }
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
