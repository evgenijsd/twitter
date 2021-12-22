﻿using InterTwitter.Helpers;
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
    public class CreatePageViewModel : BaseViewModel
    {
        private readonly IRegistrationService _registrationService;

        private readonly IDialogService _dialogs;

        private readonly IKeyboardHelper _keyboardHelper;

        private UserModel _user;

        public CreatePageViewModel(
            INavigationService navigationService,
            IDialogService dialogs,
            IRegistrationService registrationService,
            IKeyboardHelper keyboardHelper)
            : base(navigationService)
        {
            _registrationService = registrationService;
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

        private bool _isFocusedName = false;

        public bool IsFocusedName
        {
            get => _isFocusedName;
            set => SetProperty(ref _isFocusedName, value);
        }

        private bool _isFocusedEmail = false;

        public bool IsFocusedEmail
        {
            get => _isFocusedEmail;
            set => SetProperty(ref _isFocusedEmail, value);
        }

        private string _buttonText = Resources.Resource.Next;

        public string ButtonText
        {
            get => _buttonText;
            set => SetProperty(ref _buttonText, value);
        }

        private ICommand _LogInCommand;

        public ICommand LogInCommand => _LogInCommand ??= SingleExecutionCommand.FromFunc(OnLogInCommandAsync);

        private ICommand _PasswordCommand;

        public ICommand PasswordCommand => _PasswordCommand ??= SingleExecutionCommand.FromFunc(OnPasswordCommandAsync);

        #endregion

        #region -- Overrides --

        protected override async void OnPropertyChanged(PropertyChangedEventArgs args)
        {
            base.OnPropertyChanged(args);

            if (args.PropertyName == nameof(IsFocusedName))
            {
                if (IsFocusedName)
                {
                    await Task.Delay(100);
                    IsVisibleButton = true;
                }
                else
                {
                    if (!string.IsNullOrEmpty(Name))
                    {
                        ButtonText = Resources.Resource.SignUp;
                    }
                    else
                    {
                        ButtonText = Resources.Resource.Next;
                    }

                    IsVisibleButton = false;
                }
            }

            if (args.PropertyName == nameof(IsFocusedEmail))
            {
                if (IsFocusedEmail)
                {
                    await Task.Delay(200);
                    IsVisibleButton = true;
                }
                else
                {
                    IsVisibleButton = false;
                }
            }

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

            await NavigationService.NavigateAsync($"/{nameof(LogInPage)}");
        }

        private async Task OnPasswordCommandAsync()
        {
            var result = await _registrationService.CheckTheCorrectEmailAsync(Email);
            if (result.IsSuccess)
            {
                _keyboardHelper.HideKeyboard();

                var parametrs = new DialogParameters { { Constants.Navigation.MESSAGE, Resources.Resource.AlertLoginTaken } };
                await _dialogs.ShowDialogAsync(nameof(AlertView), parametrs);
            }
            else
            {
                var validator = ValidatorsExtension.CreatePageValidator.Validate(this);
                if (validator.IsValid)
                {
                    _keyboardHelper.HideKeyboard();

                    _user = new UserModel();
                    _user.Email = Email;
                    _user.Name = Name;
                    var parametrs = new NavigationParameters { { Constants.Navigation.USER, _user } };
                    await NavigationService.NavigateAsync($"{nameof(PasswordPage)}", parametrs);
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
                }
            }
        }

        #endregion
    }
}
