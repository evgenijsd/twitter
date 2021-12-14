using InterTwitter.Helpers;
using InterTwitter.Models;
using InterTwitter.Services.PermissionsService;
using InterTwitter.Services.Settings;
using InterTwitter.Services.UserService;
using InterTwitter.Views;
using Prism.Navigation;
using Prism.Services;
using System;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace InterTwitter.ViewModels
{
    public class EditProfilePageViewModel : BaseViewModel
    {
        private readonly IPermissionsService _permissionsService;
        private readonly ISettingsManager _settingsManager;
        private readonly IUserService _userService;
        private readonly IPageDialogService _dialogService;
        private UserModel _user;

        public EditProfilePageViewModel(INavigationService navigationService, IPermissionsService permissionsService, ISettingsManager settingsManager, IUserService userService, IPageDialogService dialogService)
            : base(navigationService)
        {
            _permissionsService = permissionsService;
            _settingsManager = settingsManager;
            _userService = userService;
            _dialogService = dialogService;
        }

        #region --- Public Properties ---

        private string _userMail;
        public string UserMail
        {
            get => _userMail;
            set => SetProperty(ref _userMail, value);
        }

        private string _userName;
        public string UserName
        {
            get => _userName;
            set => SetProperty(ref _userName, value);
        }

        private string _oldPassword;
        public string OldPassword
        {
            get => _oldPassword;
            set => SetProperty(ref _oldPassword, value);
        }

        private string _newPassword;
        public string NewPassword
        {
            get => _newPassword;
            set => SetProperty(ref _newPassword, value);
        }

        private string _userBackgroundImage;
        public string UserBackgroundImage
        {
            get => _userBackgroundImage;
            set => SetProperty(ref _userBackgroundImage, value);
        }

        private string _userImagePath;
        public string UserImagePath
        {
            get => _userImagePath;
            set => SetProperty(ref _userImagePath, value);
        }

        public ICommand NavigationCommandAsync => SingleExecutionCommand.FromFunc(OnNavigationCommandAsync);

        public ICommand CheckCommandAsync => SingleExecutionCommand.FromFunc(OnCheckCommandAsync);

        public ICommand PickBackgroundImageAsync => SingleExecutionCommand.FromFunc(OnPickBackgroundImageAsync);

        public ICommand PickUserImageAsync => SingleExecutionCommand.FromFunc(OnPickUserImageAsync);

        #endregion

        #region -- Overrides --

        public override Task InitializeAsync(INavigationParameters parameters)
        {
            _user = _userService.GetUserAsync(_settingsManager.UserId).Result.Result;

            UserBackgroundImage = _user.BackgroundUserImagePath;
            UserImagePath = _user.AvatarPath;
            UserMail = _user.Email;
            UserName = _user.Name;
           // OldPassword = user.Password;
            return Task.CompletedTask;
        }

        #endregion

        #region --- Private Helpers ---

        private bool isShowConfirmAlert = true;

        private async Task OnPickBackgroundImageAsync()
        {
            try
            {
                var status = await _permissionsService.RequestAsync<Permissions.StorageRead>();

                if (status == PermissionStatus.Granted)
                {
                    var file = await MediaPicker.PickPhotoAsync();

                    if (file == null)
                    {
                        return;
                    }

                    UserBackgroundImage = file.FullPath;
                }
            }
            catch (Exception)
            {
            }
        }

        private async Task OnPickUserImageAsync()
        {
            try
            {
                var status = await _permissionsService.RequestAsync<Permissions.StorageRead>();

                if (status == PermissionStatus.Granted)
                {
                    var file = await MediaPicker.PickPhotoAsync();

                    if (file == null)
                    {
                        return;
                    }

                    UserImagePath = file.FullPath;
                }
            }
            catch (Exception)
            {
            }
        }

        private async Task OnCheckCommandAsync()
        {
            bool isAllValid = true;

            if (!string.IsNullOrEmpty(OldPassword) || !string.IsNullOrEmpty(NewPassword))
            {
                if (string.IsNullOrEmpty(OldPassword))
                {
                    await _dialogService.DisplayAlertAsync(string.Empty, "Old password is empty", "Ok");
                }
                else if (OldPassword != _user.Password)
                {
                    await _dialogService.DisplayAlertAsync(string.Empty, "Old password is wrong", "Ok");
                }

                if (!string.IsNullOrEmpty(NewPassword) && Regex.IsMatch(NewPassword, Constants.RegexPatterns.PASSWORD_REGEX))
                {
                    _user.Password = NewPassword;
                }
                else
                {
                    await _dialogService.DisplayAlertAsync(string.Empty, "New password is not valid or empty", "Ok");
                    isAllValid = false;
                }
            }

            if (!string.IsNullOrEmpty(UserName) && Regex.IsMatch(UserName, Constants.RegexPatterns.USERNAME_REGEX))
            {
                _user.Name = UserName;
            }
            else
            {
                await _dialogService.DisplayAlertAsync(string.Empty, "Name is not validor empty", "Ok");
                isAllValid = false;
            }

            if (!string.IsNullOrEmpty(UserMail) && Regex.IsMatch(UserMail, Constants.RegexPatterns.EMAIL_REGEX))
            {
                _user.Email = UserMail;
            }
            else
            {
                await _dialogService.DisplayAlertAsync(string.Empty, "Email is not valid or empty", "Ok");
                isAllValid = false;
            }

            if (isAllValid)
            {
                if (isShowConfirmAlert)
                {
                    await _dialogService.DisplayAlertAsync(string.Empty, "Save changes?", "Ok", "Cancel");
                }

                _user.AvatarPath = UserImagePath;
                _user.BackgroundUserImagePath = UserBackgroundImage;

                await _userService.UpdateUserAsync(_user);

                MessagingCenter.Send(this, Constants.Messages.USER_PROFILE_CHANGED);
                await NavigationService.GoBackAsync();
            }
        }

        private async Task OnNavigationCommandAsync()
        {
           if (_user.Name != UserName || _user.Email != UserMail || _user.AvatarPath != UserImagePath || _user.BackgroundUserImagePath != UserBackgroundImage || !string.IsNullOrEmpty(OldPassword) || !string.IsNullOrEmpty(NewPassword))
            {
                var isSave = await _dialogService.DisplayAlertAsync(string.Empty, "Save changes?", "Ok", "Cancel");
                if (isSave)
                {
                    isShowConfirmAlert = false;
                    await OnCheckCommandAsync();
                }
                else
                {
                    await NavigationService.GoBackAsync();
                }
            }
            else
            {
                await NavigationService.GoBackAsync();
            }
        }

        #endregion

    }
}
