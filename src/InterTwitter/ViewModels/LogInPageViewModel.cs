using InterTwitter.Helpers;
using InterTwitter.Models;
using InterTwitter.Services;
using InterTwitter.ViewModels.Validators;
using InterTwitter.Views;
using Prism.Navigation;
using Prism.Services.Dialogs;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Windows.Input;

namespace InterTwitter.ViewModels
{
    public class LogInPageViewModel : BaseViewModel
    {
        private readonly IRegistrationService _registrationService;

        private readonly IAuthorizationService _autorizationService;

        private readonly IDialogService _dialogs;

        private readonly IKeyboardHelper _keyboardHelper;

        public LogInPageViewModel(
            INavigationService navigationService,
            IDialogService dialogs,
            IRegistrationService registrationService,
            IAuthorizationService autorizationService,
            IKeyboardHelper keyboardHelper)
            : base(navigationService)
        {
            _registrationService = registrationService;
            _autorizationService = autorizationService;
            _dialogs = dialogs;
            _keyboardHelper = keyboardHelper;
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

        private ICommand _CreateCommand;

        public ICommand CreateCommand => _CreateCommand ??= SingleExecutionCommand.FromFunc(OnCreateCommandAsync);

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

        public override async void OnNavigatedTo(INavigationParameters parameters)
        {
            var result = await _registrationService?.GetByIdAsync(_autorizationService.UserId);
            if (result.IsSuccess)
            {
                var user = result.Result;
                var parametrs = new NavigationParameters { { Constants.Navigation.USER, user } };
                await NavigationService.NavigateAsync($"/{nameof(FlyOutPage)}", parametrs);
            }
        }

        #endregion

        #region -- Private helpers --

        private async Task OnCreateCommandAsync()
        {
            _keyboardHelper.HideKeyboard();

            await NavigationService.NavigateAsync($"/{nameof(CreatePage)}");
        }

        private async Task OnTwitterCommandAsync()
        {
            var validator = ValidatorsExtension.LogInPageValidator.Validate(this);
            if (validator.IsValid)
            {
                var result = await _autorizationService.CheckUserAsync(Email, Password);
                if (result.IsSuccess)
                {
                    _keyboardHelper.HideKeyboard();

                    var user = result.Result;
                    _autorizationService.UserId = user.Id;
                    var parametrs = new NavigationParameters { { Constants.Navigation.USER, user } };
                    await NavigationService.NavigateAsync($"/{nameof(FlyOutPage)}", parametrs);
                }
                else
                {
                    _keyboardHelper.HideKeyboard();

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
            }
        }

        #endregion
    }
}
