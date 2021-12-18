using InterTwitter.Helpers;
using InterTwitter.Models;
using InterTwitter.Services.Authorization;
using InterTwitter.Services.Registration;
using InterTwitter.ViewModels.Validators;
using InterTwitter.Views;
using Prism.Navigation;
using Prism.Services;
using Prism.Services.Dialogs;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace InterTwitter.ViewModels
{
    public class LogInPageViewModel : BaseViewModel
    {
        private readonly IRegistrationService _registrationService;

        private readonly IAuthorizationService _autorizationService;

        private readonly IDialogService _dialogs;

        private readonly LogInPageValidator _LogInPageValidator;

        private UserModel _user;

        public LogInPageViewModel(
            INavigationService navigationService,
            IDialogService dialogs,
            IRegistrationService registrationService,
            IAuthorizationService autorizationService)
            : base(navigationService)
        {
            _registrationService = registrationService;
            _autorizationService = autorizationService;
            _dialogs = dialogs;
            _LogInPageValidator = new LogInPageValidator();
        }

        #region -- Public properties --

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
                IsWrongEmail = false;
            }

            if (args.PropertyName == nameof(Password))
            {
                IsWrongPassword = false;
            }
        }

        public override void OnNavigatedTo(INavigationParameters parameters)
        {
            if (parameters.TryGetValue(Constants.Navigation.USER, out UserModel user))
            {
                _user = user;
                Email = _user.Email ?? string.Empty;
                Password = _user.Password ?? string.Empty;
            }
        }

        #endregion

        #region -- Private helpers --

        private async Task OnStartCommandAsync()
        {
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

                        _user = result.Result;
                        _autorizationService.UserId = _user.Id;
                        var parametrs = new NavigationParameters { { Constants.Navigation.USER, _user } };
                        await NavigationService.NavigateAsync($"/{nameof(FlyOutPage)}", parametrs);
                    }
                    else
                    {
                        var parametrs = new DialogParameters { { Constants.Navigation.MESSAGE, Resources.Resource.AlertInvalidPassword } };
                        await _dialogs.ShowDialogAsync(nameof(AlertView), parametrs);
                        Password = string.Empty;
                    }
                }
                else
                {
                    var parametrs = new DialogParameters { { Constants.Navigation.MESSAGE, Resources.Resource.AlertInvalidLogin } };
                    await _dialogs.ShowDialogAsync(nameof(AlertView), parametrs);
                }
            }
            else
            {
                foreach (var error in validator.Errors)
                {
                    if (error.PropertyName == nameof(Password))
                    {
                        IsWrongPassword = true;
                    }

                    if (error.PropertyName == nameof(Email))
                    {
                        IsWrongEmail = true;
                    }
                }

                var parametrs = new DialogParameters { { Constants.Navigation.MESSAGE, validator.Errors[0].ErrorMessage } };
                await _dialogs.ShowDialogAsync(nameof(AlertView), parametrs);
            }
        }

        #endregion
    }
}
