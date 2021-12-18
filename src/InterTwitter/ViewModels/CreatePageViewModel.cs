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
    public class CreatePageViewModel : BaseViewModel
    {
        private readonly IRegistrationService _registrationService;

        private readonly IDialogService _dialogs;

        private readonly CreatePageValidator _CreatePageValidator;

        public CreatePageViewModel(
            INavigationService navigationService,
            IDialogService dialogs,
            IRegistrationService registrationService)
            : base(navigationService)
        {
            _registrationService = registrationService;
            _dialogs = dialogs;
            _CreatePageValidator = new CreatePageValidator();
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

        private string _email = string.Empty;

        public string Email
        {
            get => _email;
            set => SetProperty(ref _email, value);
        }

        private bool _isVisibleButton = false;

        public bool IsVisibleButton
        {
            get => _isVisibleButton;
            set => SetProperty(ref _isVisibleButton, value);
        }

        private ICommand _StartCommand;
        public ICommand StartCommand => _StartCommand ??= SingleExecutionCommand.FromFunc(OnStartCommandAsync);
        private ICommand _PasswordCommand;
        public ICommand PasswordCommand => _PasswordCommand ??= SingleExecutionCommand.FromFunc(OnPasswordCommandAsync);

        #endregion

        #region -- Overrides --

        protected override void OnPropertyChanged(PropertyChangedEventArgs args)
        {
            base.OnPropertyChanged(args);

            if (args.PropertyName == nameof(Name))
            {
                IsWrongName = false;
            }

            if (args.PropertyName == nameof(Email))
            {
                IsWrongEmail = false;
            }
        }

        public override void OnNavigatedTo(INavigationParameters parameters)
        {
            if (parameters.ContainsKey("User"))
            {
                User = parameters.GetValue<UserModel>("User");
                Name = User.Name + string.Empty;
                Email = User.Email + string.Empty;
            }
        }

        #endregion

        #region -- Private helpers --

        private async Task OnStartCommandAsync()
        {
            DependencyService.Get<IKeyboardHelper>().HideKeyboard();

            await NavigationService.GoBackAsync();
        }

        private async Task OnPasswordCommandAsync()
        {
            var result = await _registrationService.CheckTheCorrectEmailAsync(Email);
            if (result.IsSuccess)
            {
                var p = new DialogParameters { { "message", Resources.Resource.AlertLoginTaken } };
                await _dialogs.ShowDialogAsync(nameof(AlertView), p);
            }
            else
            {
                var validator = _CreatePageValidator.Validate(this);
                if (validator.IsValid)
                {
                    DependencyService.Get<IKeyboardHelper>().HideKeyboard();

                    User.Email = Email;
                    User.Name = Name;
                    var p = new NavigationParameters { { "User", User } };
                    await NavigationService.NavigateAsync($"{nameof(PasswordPage)}", p);
                }
                else
                {
                    foreach (var error in validator.Errors)
                    {
                        if (error.PropertyName == nameof(Name))
                        {
                            IsWrongName = true;
                        }

                        if (error.PropertyName == nameof(Email))
                        {
                            IsWrongEmail = true;
                        }
                    }

                    var p = new DialogParameters { { "message", validator.Errors[0].ErrorMessage } };
                    await _dialogs.ShowDialogAsync(nameof(AlertView), p);
                }
            }
        }

        #endregion
    }
}
