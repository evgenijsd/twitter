using InterTwitter.Helpers;
using InterTwitter.Models;
using InterTwitter.Services.Authorization;
using InterTwitter.Services.Registration;
using InterTwitter.ViewModels.Validators;
using InterTwitter.Views;
using Prism.Navigation;
using Prism.Services;
using System;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace InterTwitter.ViewModels
{
    public class StartPageViewModel : BaseViewModel
    {
        private IRegistrationService _registrationService { get; }
        private IAuthorizationService _autorizationService { get; }
        private IPageDialogService _dialogs { get; }
        private StartPageValidator _StartPageValidator { get; }
        private bool IsAutoLogin = true;

        public StartPageViewModel(INavigationService navigationService, IPageDialogService dialogs, IRegistrationService registrationService, IAuthorizationService autorizationService)
            : base(navigationService)
        {
            _registrationService = registrationService;
            _autorizationService = autorizationService;
            UserId = _autorizationService.UserId;
            _dialogs = dialogs;
            _StartPageValidator = new StartPageValidator();
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

        private string _messageErrorName = string.Empty;
        public string MessageErrorName
        {
            get => _messageErrorName;
            set => SetProperty(ref _messageErrorName, value);
        }

        private string _messageErrorEmail = string.Empty;
        public string MessageErrorEmail
        {
            get => _messageErrorEmail;
            set => SetProperty(ref _messageErrorEmail, value);
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
                MessageErrorName = string.Empty;
                var validator = _StartPageValidator.Validate(this);
                IsWrongName = !string.IsNullOrEmpty(MessageErrorName) && !string.IsNullOrEmpty(Name);
            }

            if (args.PropertyName == nameof(Email))
            {
                MessageErrorEmail = string.Empty;
                var validator = _StartPageValidator.Validate(this);
                IsWrongEmail = !string.IsNullOrEmpty(MessageErrorEmail) && !string.IsNullOrEmpty(Email);
            }

            if (args.PropertyName == nameof(IsVisibleButton))
            {
                IsUnVisibleButton = !IsVisibleButton;
            }
        }

        public override void OnNavigatedTo(INavigationParameters parameters)
        {
            if (parameters.ContainsKey(nameof(User)))
            {
                User = parameters.GetValue<UserModel>(nameof(User));
                Name = User.Name;
                Email = User.Email;
                IsAutoLogin = false;
            }
        }

        public override async void Initialize(INavigationParameters parameters)
        {
            await Task.Delay(TimeSpan.FromSeconds(0.1));
            App.Current.UserAppTheme = OSAppTheme.Light;
            var user = await _registrationService?.GetByIdAsync(UserId);
            if (user.IsSuccess && IsAutoLogin)
            {
                User = user.Result;
                var p = new NavigationParameters { { nameof(User), User } };
                await NavigationService.NavigateAsync($"/{nameof(FlyOutPage)}", p);
            }

            IsAutoLogin = true;
        }
        #endregion

        #region -- Private helpers --

        private async Task OnLogInCommandAsync()
        {
            DependencyService.Get<IKeyboardHelper>().HideKeyboard();

            var emailCheck = await _registrationService?.CheckTheCorrectEmailAsync(Email);
            if (!emailCheck.Result && !string.IsNullOrEmpty(Email))
            {
                await _dialogs.DisplayAlertAsync(Resources.Resource.Alert, Resources.Resource.AlertLoginNotExist, Resources.Resource.Ok);
            }
            else
            {
                if (IsWrongEmail || IsWrongName)
                {
                    var validator = _StartPageValidator.Validate(this);
                    if (string.IsNullOrEmpty(MessageErrorName))
                    {
                        MessageErrorName = MessageErrorEmail;
                    }

                    if (!string.IsNullOrEmpty(MessageErrorName))
                    {
                        await _dialogs.DisplayAlertAsync(Resources.Resource.Alert, MessageErrorName, Resources.Resource.Ok);
                    }
                }
            }

            DependencyService.Get<IKeyboardHelper>().HideKeyboard();

            User.Email = Email;
            User.Name = Name;
            var p = new NavigationParameters { { nameof(User), User } };
            await NavigationService.NavigateAsync($"{nameof(LogInPage)}", p);
        }

        private async Task OnCreateCommandAsync()
        {
            var emailCheck = await _registrationService?.CheckTheCorrectEmailAsync(Email);
            if (emailCheck.Result)
            {
                await _dialogs.DisplayAlertAsync(Resources.Resource.Alert, Resources.Resource.AlertLoginTaken, Resources.Resource.Ok);
            }
            else
            {
                DependencyService.Get<IKeyboardHelper>().HideKeyboard();

                if (IsWrongEmail || IsWrongName)
                {
                    await _dialogs.DisplayAlertAsync(Resources.Resource.Alert, Resources.Resource.AlertDataIncorrect, Resources.Resource.Ok);
                }

                User.Email = Email;
                User.Name = Name;
                var p = new NavigationParameters { { nameof(User), User } };
                await NavigationService.NavigateAsync($"{nameof(CreatePage)}", p);
            }
        }

        #endregion
    }
}
