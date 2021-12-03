using InterTwitter.Enums;
using InterTwitter.Models;
using InterTwitter.Services.Registration;
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

        private bool _isUnVisibleButton = true;
        public bool IsUnVisibleButton
        {
            get => _isUnVisibleButton;
            set => SetProperty(ref _isUnVisibleButton, value);
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
                IsWrongName = _registrationService.CheckCorrectName(Name) != ECheckEnter.ChecksArePassed;
                if (string.IsNullOrEmpty(Name))
                {
                    IsWrongName = false;
                }
            }

            if (args.PropertyName == nameof(Email))
            {
                IsWrongEmail = _registrationService.CheckCorrectEmail(Email) != ECheckEnter.ChecksArePassed;
                if (string.IsNullOrEmpty(Email))
                {
                    IsWrongEmail = false;
                }
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
                Name = User.Name;
                Email = User.Email;
            }
        }
        #endregion

        #region -- Private helpers --

        private async Task OnStartCommandAsync()
        {
            await _navigationService.GoBackAsync();
        }

        private async Task OnPasswordCommandAsync()
        {
            var result = await _registrationService.CheckTheCorrectEmailAsync(Email);
            var check = result.Result;
            if (check == ECheckEnter.ChecksArePassed)
            {
                check = _registrationService.CheckCorrectEmail(Email);
            }

            if (check == ECheckEnter.ChecksArePassed)
            {
                check = _registrationService.CheckCorrectName(Name);
            }

            switch (check)
                {
                    case ECheckEnter.LoginExist:
                        await _dialogs.DisplayAlertAsync(Resources.Resource.Alert, Resources.Resource.AlertLoginTaken, Resources.Resource.Ok);
                        break;
                    case ECheckEnter.EmailANotVaid:
                        await _dialogs.DisplayAlertAsync(Resources.Resource.Alert, Resources.Resource.AlertEmailNoA, Resources.Resource.Ok);
                        break;
                    case ECheckEnter.EmailNotValid:
                        await _dialogs.DisplayAlertAsync(Resources.Resource.Alert, Resources.Resource.AlertEmailInvalid, Resources.Resource.Ok);
                        break;
                    case ECheckEnter.EmailLengthNotValid:
                        await _dialogs.DisplayAlertAsync(Resources.Resource.Alert, Resources.Resource.AlertEmailLength, Resources.Resource.Ok);
                        break;
                    case ECheckEnter.NameLengthNotValid:
                        await _dialogs.DisplayAlertAsync(Resources.Resource.Alert, Resources.Resource.AlertNameLength, Resources.Resource.Ok);
                        break;
                    case ECheckEnter.NameNotValid:
                        await _dialogs.DisplayAlertAsync(Resources.Resource.Alert, Resources.Resource.AlertNameLetter, Resources.Resource.Ok);
                        break;

                    default:
                        {
                            User.Email = Email;
                            User.Name = Name;
                            var p = new NavigationParameters { { "User", User } };
                            await _navigationService.NavigateAsync($"{nameof(PasswordPage)}", p);
                        }

                        break;
                }
        }
        #endregion
    }
}
