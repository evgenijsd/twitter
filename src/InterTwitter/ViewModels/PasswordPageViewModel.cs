using InterTwitter.Helpers;
using InterTwitter.Models;
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
    public class PasswordPageViewModel : BaseViewModel
    {
        private readonly IRegistrationService _registrationService;

        private readonly IDialogService _dialogs;

        private readonly PasswordPageValidator _PasswordPageValidator;

        public PasswordPageViewModel (
            INavigationService navigationService,
            IDialogService dialogs,
            IRegistrationService registrationService)
            : base(navigationService)
        {
            _registrationService = registrationService;
            _dialogs = dialogs;
            _PasswordPageValidator = new PasswordPageValidator();
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
                IsWrongPassword = false;
            }

            if (args.PropertyName == nameof(ConfirmPassword))
            {
                IsWrongConfirmPassword = false;
            }
        }

        public override void OnNavigatedTo(INavigationParameters parameters)
        {
            if (parameters.ContainsKey(nameof(User)))
            {
                User = parameters.GetValue<UserModel>(nameof(User));
            }
        }

        #endregion

        #region -- Private helpers --

        private async Task OnCreateCommandAsync()
        {
            DependencyService.Get<IKeyboardHelper>().HideKeyboard();

            await NavigationService.GoBackAsync();
        }

        private async Task OnStartCommandAsync()
        {
            var validator = _PasswordPageValidator.Validate(this);
            if (validator.IsValid)
            {
                User.Password = Password;
                Password = string.Empty;
                ConfirmPassword = string.Empty;
                User.AvatarPath = "pic_profile_big";
                User.BackgroundUserImagePath = "pic_profile_big";
                var result = await _registrationService.AddAsync(User);
                if (result.IsSuccess)
                {
                    DependencyService.Get<IKeyboardHelper>().HideKeyboard();

                    var p = new NavigationParameters { { nameof(User), User } };
                    await NavigationService.NavigateAsync($"/{nameof(StartPage)}", p);
                }
                else
                {
                    var p = new DialogParameters { { "message", Resources.Resource.AlertDatabase } };
                    await _dialogs.ShowDialogAsync(nameof(AlertView), p);
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

                    if (error.PropertyName == nameof(ConfirmPassword))
                    {
                        IsWrongConfirmPassword = true;
                    }
                }

                var p = new DialogParameters { { "message", validator.Errors[0].ErrorMessage } };
                await _dialogs.ShowDialogAsync(nameof(AlertView), p);
            }
        }

        #endregion
    }
}
