using InterTwitter.Enums;
using InterTwitter.Models;
using InterTwitter.Services.Registration;
using InterTwitter.Views;
using MapNotePad.Helpers;
using Prism.Navigation;
using Prism.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace InterTwitter.ViewModels
{
    public class PasswordPageViewModel : BaseViewModel
    {
        private IRegistrationService _registrationService { get; }
        private IPageDialogService _dialogs { get; }

        public PasswordPageViewModel(INavigationService navigationService, IPageDialogService dialogs, IRegistrationService registrationService)
            : base(navigationService)
        {
            _registrationService = registrationService;
            _dialogs = dialogs;
        }

        #region -- Public properties --
        private UserModel _user = new ();
        public UserModel User
        {
            get => _user;
            set => SetProperty(ref _user, value);
        }

        private string _password = string.Empty;
        public string Password
        {
            get => _password;
            set => SetProperty(ref _password, value);
        }

        private bool _isVisiblePassword = false;
        public bool IsVisiblePassword
        {
            get => _isVisiblePassword;
            set => SetProperty(ref _isVisiblePassword, value);
        }

        private bool _isVisibleConfirmPassword = false;
        public bool IsVisibleConfirmPassword
        {
            get => _isVisibleConfirmPassword;
            set => SetProperty(ref _isVisibleConfirmPassword, value);
        }

        private bool _isWrongPassword = false;
        public bool IsWrongPassword
        {
            get => _isWrongPassword;
            set => SetProperty(ref _isWrongPassword, value);
        }

        private bool _isWrongConfirmPassword = false;
        public bool IsWrongConfirmPassword
        {
            get => _isWrongConfirmPassword;
            set => SetProperty(ref _isWrongConfirmPassword, value);
        }

        private string _confirmPassword = string.Empty;
        public string ConfirmPassword
        {
            get => _confirmPassword;
            set => SetProperty(ref _confirmPassword, value);
        }

        private bool _isVisibleButton = false;
        public bool IsVisibleButton
        {
            get => _isVisibleButton;
            set => SetProperty(ref _isVisibleButton, value);
        }

        private bool _isUnVisibleButton = true;
        public bool IsUnVisibleButton
        {
            get => _isUnVisibleButton;
            set => SetProperty(ref _isUnVisibleButton, value);
        }

        private ICommand _CreateCommand;
        public ICommand CreateCommand => _CreateCommand ??= SingleExecutionCommand.FromFunc(OnCreateCommandAsync);
        private ICommand _StartCommand;
        public ICommand StartCommand => _StartCommand ??= SingleExecutionCommand.FromFunc(OnStartCommandAsync);
        #endregion

        #region -- Overrides --
        protected override void OnPropertyChanged(PropertyChangedEventArgs args)
        {
            base.OnPropertyChanged(args);

            if (args.PropertyName != nameof(Password))
            {
            }
            else
            {
                IsWrongPassword = _registrationService.CheckTheCorrectPassword(Password, Password) != ECheckEnter.ChecksArePassed;
                if (string.IsNullOrEmpty(Password))
                {
                    IsWrongPassword = false;
                }
            }

            if (args.PropertyName == nameof(ConfirmPassword))
            {
                IsWrongConfirmPassword = _registrationService.CheckTheCorrectPassword(Password, ConfirmPassword) != ECheckEnter.ChecksArePassed;
                if (string.IsNullOrEmpty(ConfirmPassword))
                {
                    IsWrongConfirmPassword = false;
                }
            }

            if (args.PropertyName == nameof(IsVisibleButton))
            {
                IsUnVisibleButton = !IsVisibleButton;
            }
        }

        public override void OnNavigatedTo(INavigationParameters parameters)
        {
            if (parameters.ContainsKey("User"))
            {
                User = parameters.GetValue<UserModel>("User");
            }
        }
        #endregion

        #region -- Private helpers --

        private async Task OnCreateCommandAsync()
        {
            await _navigationService.GoBackAsync();
        }

        private async Task OnStartCommandAsync()
        {
            var check = _registrationService.CheckTheCorrectPassword(Password, ConfirmPassword);

            switch (check)
            {
                case ECheckEnter.PasswordBigLetterAndDigit:
                    await _dialogs.DisplayAlertAsync(Resources.Resource.Alert, Resources.Resource.AlertPasswordLetterDigit, Resources.Resource.Ok);
                    break;
                case ECheckEnter.PasswordLengthNotValid:
                    await _dialogs.DisplayAlertAsync(Resources.Resource.Alert, Resources.Resource.AlertPasswordLength, Resources.Resource.Ok);
                    break;
                case ECheckEnter.PasswordsNotEqual:
                    await _dialogs.DisplayAlertAsync(Resources.Resource.Alert, Resources.Resource.AlertPasswordNotEqual, Resources.Resource.Ok);
                    break;

                default:
                    {
                        User.Password = Password;
                        User.UserPhoto = "pic_profile_big";
                        var result = await _registrationService.UserAddAsync(User);
                        if (result.IsSuccess)
                        {
                            var p = new NavigationParameters { { "User", User } };
                            await _navigationService.NavigateAsync($"/{nameof(StartPage)}", p);
                        }
                    }

                    break;
            }
        }
        #endregion
    }
}
