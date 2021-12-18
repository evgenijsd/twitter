using System;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Windows.Input;
using InterTwitter.Helpers;
using InterTwitter.Models;
using InterTwitter.Services.Authorization;
using InterTwitter.Services.Registration;
using InterTwitter.ViewModels.Validators;
using InterTwitter.Views;
using Prism.Navigation;
using Xamarin.Forms;

namespace InterTwitter.ViewModels
{
    public class StartPageViewModel : BaseViewModel
    {
        private readonly IRegistrationService _registrationService;

        private readonly IAuthorizationService _autorizationService;

        private readonly StartPageValidator _StartPageValidator;

        private bool IsAutoLogin = true;

        public StartPageViewModel(
            INavigationService navigationService,
            IRegistrationService registrationService,
            IAuthorizationService autorizationService)
            : base(navigationService)
        {
            App.Current.UserAppTheme = OSAppTheme.Light;
            _registrationService = registrationService;
            _autorizationService = autorizationService;
            UserId = _autorizationService.UserId;
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
            var user = await _registrationService?.GetByIdAsync(UserId);
            if (user.IsSuccess)
            {
                User = user.Result;
                var p = new NavigationParameters { { nameof(User), User } };
                await NavigationService.NavigateAsync($"/{nameof(FlyOutPage)}", p);
            }
        }
        #endregion

        #region -- Private helpers --

        private async Task OnLogInCommandAsync()
        {
            DependencyService.Get<IKeyboardHelper>().HideKeyboard();

            User.Email = Email;
            User.Name = Name;
            var p = new NavigationParameters { { nameof(User), User } };
            await NavigationService.NavigateAsync($"{nameof(LogInPage)}", p);
        }

        private async Task OnCreateCommandAsync()
        {
            DependencyService.Get<IKeyboardHelper>().HideKeyboard();

            User.Email = Email;
            User.Name = Name;
            var p = new NavigationParameters { { nameof(User), User } };
            await NavigationService.NavigateAsync($"{nameof(CreatePage)}", p);
        }

        #endregion
    }
}
