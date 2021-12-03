using System;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Windows.Input;
using InterTwitter.Enums;
using InterTwitter.Models;
using InterTwitter.Services.Autorization;
using InterTwitter.Services.Registration;
using InterTwitter.Views;
using MapNotePad.Helpers;
using Prism.Navigation;
using Prism.Services;

namespace InterTwitter.ViewModels
{
    public class LogInPageViewModel : BaseViewModel
    {
        private IRegistrationService _registrationService { get; }
        private IAutorizationService _autorizationService { get; }
        private IPageDialogService _dialogs { get; }

        public LogInPageViewModel(INavigationService navigationService, IPageDialogService dialogs, IRegistrationService registrationService, IAutorizationService autorizationService)
            : base(navigationService)
        {
            _registrationService = registrationService;
            _autorizationService = autorizationService;
            _dialogs = dialogs;
        }

        #region -- Public properties --
        private UserModel _user = new ();
        public UserModel User
        {
            get => _user;
            set => SetProperty(ref _user, value);
        }

        private string _name = string.Empty;
        public string Name
        {
            get => _name;
            set => SetProperty(ref _name, value);
        }

        private bool _isVisibleEmail = false;
        public bool IsVisibleEmail
        {
            get => _isVisibleEmail;
            set => SetProperty(ref _isVisibleEmail, value);
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

        private bool _isWrongPassword = false;
        public bool IsWrongPassword
        {
            get => _isWrongPassword;
            set => SetProperty(ref _isWrongPassword, value);
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

        private ICommand _StartCommand;
        public ICommand StartCommand => _StartCommand ??= SingleExecutionCommand.FromFunc(OnStartCommandAsync);
        private ICommand _TwitterCommand;
        public ICommand TwitterCommand => _TwitterCommand ??= SingleExecutionCommand.FromFunc(OnTwitterCommandAsync);
        #endregion

        #region -- Overrides --
        protected override void OnPropertyChanged(PropertyChangedEventArgs args)
        {
            base.OnPropertyChanged(args);

            if (args.PropertyName == nameof(Email))
            {
                IsWrongEmail = _registrationService.CheckCorrectEmail(Email) != ECheckEnter.ChecksArePassed;
                if (string.IsNullOrEmpty(Email))
                {
                    IsWrongEmail = false;
                }
            }

            if (args.PropertyName == nameof(Password))
            {
                IsWrongPassword = _registrationService.CheckTheCorrectPassword(Password, Password) != ECheckEnter.ChecksArePassed;
                if (string.IsNullOrEmpty(Password))
                {
                    IsWrongPassword = false;
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
                Email = User.Email;
                Password = User.Password;
            }
        }
        #endregion

        #region -- Private helpers --

        private async Task OnStartCommandAsync()
        {
            await _navigationService.GoBackAsync();
        }

        private async Task OnTwitterCommandAsync()
        {
            var result = await _autorizationService.CheckUserAsync(Email, Password);
            if (result.IsSuccess)
            {
                User = result.Result;
                _autorizationService.UserId = User.Id;
                await _dialogs.DisplayAlertAsync("Alert", $"TwitterCommand id - {User.Id}", "Ok");
                //var p = new NavigationParameters { { "User", User } };
                //await _navigationService.NavigateAsync($"/{nameof(TwitterPage)}", p);
            }
            else
            {
                await _dialogs.DisplayAlertAsync("Alert", "Invalid login or password!", "Ok");
                Password = string.Empty;
            }
        }
        #endregion
    }
}
