using InterTwitter.Helpers;
using InterTwitter.Models;
using InterTwitter.Services.Authorization;
using InterTwitter.Services.Registration;
using InterTwitter.ViewModels.Validators;
using InterTwitter.Views;
using Prism.Navigation;
using Prism.Services;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace InterTwitter.ViewModels
{
    public class LogInPageViewModel : BaseViewModel
    {
        private IRegistrationService _registrationService { get; }
        private IAuthorizationService _autorizationService { get; }
        private IPageDialogService _dialogs { get; }
        private LogInPageValidator _LogInPageValidator { get; }

        public LogInPageViewModel(INavigationService navigationService, IPageDialogService dialogs, IRegistrationService registrationService, IAuthorizationService autorizationService)
            : base(navigationService)
        {
            _registrationService = registrationService;
            _autorizationService = autorizationService;
            _dialogs = dialogs;
            _LogInPageValidator = new LogInPageValidator();
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

        private string _messageErrorEmail = string.Empty;
        public string MessageErrorEmail
        {
            get => _messageErrorEmail;
            set => SetProperty(ref _messageErrorEmail, value);
        }

        private string _messageErrorPassword = string.Empty;
        public string MessageErrorPassword
        {
            get => _messageErrorPassword;
            set => SetProperty(ref _messageErrorPassword, value);
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
                MessageErrorEmail = string.Empty;
                var validator = _LogInPageValidator.Validate(this);
                IsWrongEmail = !string.IsNullOrEmpty(MessageErrorEmail) && !string.IsNullOrEmpty(Email);
            }

            if (args.PropertyName == nameof(Password))
            {
                MessageErrorPassword = string.Empty;
                var validator = _LogInPageValidator.Validate(this);
                IsWrongPassword = !string.IsNullOrEmpty(MessageErrorPassword) && !string.IsNullOrEmpty(Password);
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
                Email = User.Email + string.Empty;
                Password = User.Password + string.Empty;
            }
        }
        #endregion

        #region -- Private helpers --

        private async Task OnStartCommandAsync()
        {
            DependencyService.Get<IKeyboardHelper>().HideKeyboard();

            await NavigationService.GoBackAsync();
        }

        private async Task OnTwitterCommandAsync()
        {
            var validator = _LogInPageValidator.Validate(this);
            if (validator.IsValid)
            {
                var result = await _autorizationService.CheckUserAsync(Email, Password);
                if (result.IsSuccess)
                {
                    if (result.Result.Password == Password)
                    {
                        DependencyService.Get<IKeyboardHelper>().HideKeyboard();

                        User = result.Result;
                        _autorizationService.UserId = User.Id;
                        var p = new NavigationParameters { { nameof(User), User } };
                        await NavigationService.NavigateAsync($"/{nameof(FlyOutPage)}", p);
                    }
                    else
                    {
                        await _dialogs.DisplayAlertAsync(Resources.Resource.Alert, Resources.Resource.AlertInvalidPassword, Resources.Resource.Ok);
                        Password = string.Empty;
                    }
                }
                else
                {
                    await _dialogs.DisplayAlertAsync(Resources.Resource.Alert, Resources.Resource.AlertInvalidLogin, Resources.Resource.Ok);
                }
            }
            else
            {
                if (string.IsNullOrEmpty(MessageErrorEmail))
                {
                    MessageErrorEmail = MessageErrorPassword;
                }

                await _dialogs.DisplayAlertAsync(Resources.Resource.Alert, MessageErrorEmail, Resources.Resource.Ok);
            }
        }
        #endregion
    }
}
