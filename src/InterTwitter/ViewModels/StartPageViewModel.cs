using InterTwitter.Helpers;
using InterTwitter.Models;
using InterTwitter.Services.Authorization;
using InterTwitter.Services.Registration;
using InterTwitter.Views;
using Prism.Navigation;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace InterTwitter.ViewModels
{
    public class StartPageViewModel : BaseViewModel
    {
        private readonly IRegistrationService _registrationService;

        private readonly IAuthorizationService _autorizationService;

        private readonly IKeyboardHelper _keyboardHelper;

        private int _userId = 0;
        private UserModel _user;

        public StartPageViewModel(
            INavigationService navigationService,
            IRegistrationService registrationService,
            IAuthorizationService autorizationService,
            IKeyboardHelper keyboardHelper)
            : base(navigationService)
        {
            App.Current.UserAppTheme = OSAppTheme.Light;
            _registrationService = registrationService;
            _autorizationService = autorizationService;
            _userId = _autorizationService.UserId;
            _keyboardHelper = keyboardHelper;
        }

        #region -- Public properties --

        public static bool IsAutoLogin = true;

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

        public override async void OnNavigatedTo(INavigationParameters parameters)
        {
            var result = await _registrationService?.GetByIdAsync(_userId);
            if (result.IsSuccess && IsAutoLogin)
            {
                _user = result.Result;
                var parametrs = new NavigationParameters { { Constants.Navigation.USER, _user } };
                await NavigationService.NavigateAsync($"/{nameof(FlyOutPage)}", parametrs);
            }

            if (parameters.TryGetValue(Constants.Navigation.USER, out UserModel user))
            {
                _user = user;
                Name = _user.Name;
                Email = _user.Email;
            }
        }

        #endregion

        #region -- Private helpers --

        private async Task OnLogInCommandAsync()
        {
            _keyboardHelper.HideKeyboard();

            var user = _user ?? new UserModel();
            user.Email = Email;
            user.Name = Name;
            IsAutoLogin = false;
            var parametrs = new NavigationParameters { { Constants.Navigation.USER, user } };
            await NavigationService.NavigateAsync($"{nameof(LogInPage)}", parametrs);
        }

        private async Task OnCreateCommandAsync()
        {
            _keyboardHelper.HideKeyboard();

            var user = _user ?? new UserModel();
            user.Email = Email;
            user.Name = Name;
            IsAutoLogin = false;
            var parametrs = new NavigationParameters { { Constants.Navigation.USER, user } };
            await NavigationService.NavigateAsync($"{nameof(CreatePage)}", parametrs);
        }

        #endregion
    }
}