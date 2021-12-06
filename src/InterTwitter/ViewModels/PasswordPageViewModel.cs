using InterTwitter.Enums;
using InterTwitter.Helpers;
using InterTwitter.Models;
using InterTwitter.Services.Registration;
using InterTwitter.Validators;
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
using Xamarin.Forms;

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
            PasswordPageValidator = new PasswordPageValidator();
        }

        #region -- Public properties --
        public PasswordPageValidator PasswordPageValidator;

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

        private string _messageErrorPassword = string.Empty;
        public string MessageErrorPassword
        {
            get => _messageErrorPassword;
            set => SetProperty(ref _messageErrorPassword, value);
        }

        private string _messageErrorConfirmPassword = string.Empty;
        public string MessageErrorConfirmPassword
        {
            get => _messageErrorConfirmPassword;
            set => SetProperty(ref _messageErrorConfirmPassword, value);
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

            if (args.PropertyName == nameof(Password))
            {
                MessageErrorPassword = string.Empty;
                var validator = PasswordPageValidator.Validate(this);
                IsWrongPassword = !string.IsNullOrEmpty(MessageErrorPassword) && !string.IsNullOrEmpty(Password);
            }

            if (args.PropertyName == nameof(ConfirmPassword))
            {
                MessageErrorConfirmPassword = string.Empty;
                var validator = PasswordPageValidator.Validate(this);
                IsWrongConfirmPassword = !string.IsNullOrEmpty(MessageErrorConfirmPassword) && !string.IsNullOrEmpty(ConfirmPassword);
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
            DependencyService.Get<IKeyboardHelper>().HideKeyboard();

            await _navigationService.GoBackAsync();
        }

        private async Task OnStartCommandAsync()
        {
            var validator = PasswordPageValidator.Validate(this);
            if (validator.IsValid)
            {
                User.Password = Password;
                User.UserPhoto = "pic_profile_big";
                User.BackgroundPhoto = "pic_profile_big";
                var result = await _registrationService.UserAddAsync(User);
                if (result.IsSuccess)
                {
                    DependencyService.Get<IKeyboardHelper>().HideKeyboard();

                    var p = new NavigationParameters { { "User", User } };
                    await _navigationService.NavigateAsync($"/{nameof(StartPage)}", p);
                }
                else
                {
                    await _dialogs.DisplayAlertAsync(Resources.Resource.Alert, Resources.Resource.AlertDatabase, Resources.Resource.Ok);
                }
            }
            else
            {
                if (string.IsNullOrEmpty(MessageErrorPassword))
                {
                    MessageErrorPassword = MessageErrorConfirmPassword;
                }

                await _dialogs.DisplayAlertAsync(Resources.Resource.Alert, MessageErrorPassword, Resources.Resource.Ok);
            }
        }
        #endregion
    }
}
