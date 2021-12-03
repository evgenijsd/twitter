using InterTwitter.Enums;
using InterTwitter.Models;
using InterTwitter.Services.Autorization;
using InterTwitter.Services.Registration;
using InterTwitter.Views;
using MapNotePad.Helpers;
using Prism.Navigation;
using Prism.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace InterTwitter.ViewModels
{
    public class StartPageViewModel : BaseViewModel
    {
        private IRegistrationService _registrationService { get; }
        private IAutorizationService _autorizationService { get; }
        private IPageDialogService _dialogs { get; }

        public StartPageViewModel(INavigationService navigationService, IPageDialogService dialogs, IRegistrationService registrationService, IAutorizationService autorizationService)
            : base(navigationService)
        {
            _registrationService = registrationService;
            _autorizationService = autorizationService;
            UserId = _autorizationService.UserId;
            _dialogs = dialogs;
        }

        #region -- Public properties --
        private UserModel _user = new ();
        public UserModel User
        {
            get => _user;
            set => SetProperty(ref _user, value);
        }

        private int _userId;
        public int UserId
        {
            get => _userId;
            set => SetProperty(ref _userId, value);
        }

        private string _name = string.Empty;
        public string Name
        {
            get => _name;
            set => SetProperty(ref _name, value);
        }

        private bool _isVisibleName = false;
        public bool IsVisibleName
        {
            get => _isVisibleName;
            set => SetProperty(ref _isVisibleName, value);
        }

        private bool _isVisibleEmail = false;
        public bool IsVisibleEmail
        {
            get => _isVisibleEmail;
            set => SetProperty(ref _isVisibleEmail, value);
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
                Name = User.Name;
                Email = User.Email;
            }
        }

        public override async void Initialize(INavigationParameters parameters)
        {
            await Task.Delay(TimeSpan.FromSeconds(0.1));
            var user = _registrationService.GetUsers().FirstOrDefault(x => x.Id == UserId);
            if (user != null)
            {
                await _dialogs.DisplayAlertAsync("Alert", $"TwitterCommand id - {user.Id}", "Ok");
                //var p = new NavigationParameters { { "User", User } };
                //await _navigationService.NavigateAsync($"/{nameof(TwitterPage)}", p);
            }
        }
        #endregion

        #region -- Private helpers --

        private async Task OnLogInCommandAsync()
        {
            var emailCheck = await _registrationService.CheckTheCorrectEmailAsync(Email);
            if (emailCheck.Result != ECheckEnter.LoginExist && !string.IsNullOrEmpty(Email))
            {
                await _dialogs.DisplayAlertAsync("Alert", "This login does not exist", "Ok");
            }

            User.Email = Email;
            User.Name = Name;
            var p = new NavigationParameters { { "User", User } };
            await _navigationService.NavigateAsync($"{nameof(LogInPage)}", p);
        }

        private async Task OnCreateCommandAsync()
        {
            var emailCheck = await _registrationService.CheckTheCorrectEmailAsync(Email);
            if (emailCheck.Result == ECheckEnter.LoginExist)
            {
                await _dialogs.DisplayAlertAsync("Alert", "This login is already taken", "Ok");
            }
            else
            {
                if (IsWrongEmail || IsWrongName)
                {
                    await _dialogs.DisplayAlertAsync("Alert", "The data is incorrect field!", "Ok");
                }

                User.Email = Email;
                User.Name = Name;
                var p = new NavigationParameters { { "User", User } };
                await _navigationService.NavigateAsync($"{nameof(CreatePage)}", p);
            }
        }
        #endregion
    }
}
