using FluentValidation;
using InterTwitter.Enums;
using InterTwitter.Helpers;
using InterTwitter.Models;
using InterTwitter.Services.Registration;
using InterTwitter.Validators;
using InterTwitter.ViewModels.Validators;
using InterTwitter.Views;
using MapNotePad.Helpers;
using Prism.Navigation;
using Prism.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace InterTwitter.ViewModels
{
    public class CreatePageViewModel : BaseViewModel
    {
        private IRegistrationService _registrationService { get; }
        private IPageDialogService _dialogs { get; }

        public CreatePageViewModel(INavigationService navigationService, IPageDialogService dialogs, IRegistrationService registrationService)
            : base(navigationService)
        {
            _registrationService = registrationService;
            _dialogs = dialogs;
            CreatePageValidator = new CreatePageValidator();
        }

        #region -- Public properties --
        public CreatePageValidator CreatePageValidator;

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

        private bool _isUnVisibleButton = true;
        public bool IsUnVisibleButton
        {
            get => _isUnVisibleButton;
            set => SetProperty(ref _isUnVisibleButton, value);
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
                MessageErrorName = string.Empty;
                var validator = CreatePageValidator.Validate(this);
                IsWrongName = !string.IsNullOrEmpty(MessageErrorName) && !string.IsNullOrEmpty(Name);
            }

            if (args.PropertyName == nameof(Email))
            {
                MessageErrorEmail = string.Empty;
                var validator = CreatePageValidator.Validate(this);
                IsWrongEmail = !string.IsNullOrEmpty(MessageErrorEmail) && !string.IsNullOrEmpty(Email);
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
                Name = User.Name + string.Empty;
                Email = User.Email + string.Empty;
            }
        }
        #endregion

        #region -- Private helpers --

        private async Task OnStartCommandAsync()
        {
            DependencyService.Get<IKeyboardHelper>().HideKeyboard();

            await _navigationService.GoBackAsync();
        }

        private async Task OnPasswordCommandAsync()
        {
            var check = await _registrationService.CheckTheCorrectEmailAsync(Email);
            if (check.Result)
            {
                await _dialogs.DisplayAlertAsync(Resources.Resource.Alert, Resources.Resource.AlertLoginTaken, Resources.Resource.Ok);
            }
            else
            {
                var validator = CreatePageValidator.Validate(this);
                if (validator.IsValid)
                {
                    DependencyService.Get<IKeyboardHelper>().HideKeyboard();

                    User.Email = Email;
                    User.Name = Name;
                    var p = new NavigationParameters { { "User", User } };
                    await _navigationService.NavigateAsync($"{nameof(PasswordPage)}", p);
                }
                else
                {
                    if (string.IsNullOrEmpty(MessageErrorName))
                    {
                        MessageErrorName = MessageErrorEmail;
                    }

                    await _dialogs.DisplayAlertAsync(Resources.Resource.Alert, MessageErrorName, Resources.Resource.Ok);
                }
            }
        }
        #endregion
    }
}
