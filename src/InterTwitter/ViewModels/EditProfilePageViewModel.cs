using InterTwitter.Helpers;
using InterTwitter.Models;
using InterTwitter.Services.PermissionsService;
using InterTwitter.Services.Settings;
using InterTwitter.Services.UserService;
using InterTwitter.Views;
using Prism.Navigation;
using Prism.Services.Dialogs;
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
        private readonly IPermissionService _permissionsService;
        private readonly ISettingsManager _settingsManager;
        private readonly IUserService _userService;
        private readonly IDialogService _dialogService;
        private UserModel _user;

        public EditProfilePageViewModel(
            INavigationService navigationService,
            IPermissionService permissionsService,
            ISettingsManager settingsManager,
            IUserService userService,
            IDialogService dialogService)
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

        private ICommand _navigationCommandAsync;
        public ICommand NavigationCommandAsync => _navigationCommandAsync ??= SingleExecutionCommand.FromFunc(OnNavigationCommandAsync);

        private ICommand _checkCommandAsync;
        public ICommand CheckCommandAsync => _checkCommandAsync ??= SingleExecutionCommand.FromFunc(() => OnCheckCommandAsync());

        private ICommand _pickBackgroundImageAsync;
        public ICommand PickBackgroundImageAsync => _pickBackgroundImageAsync ??= SingleExecutionCommand.FromFunc(OnPickBackgroundImageAsync);

        private ICommand _pickUserImageAsync;
        public ICommand PickUserImageAsync => _pickUserImageAsync ??= SingleExecutionCommand.FromFunc(OnPickUserImageAsync);

        #endregion

        #region -- Overrides --

        public override Task InitializeAsync(INavigationParameters parameters)
        {
            var userResponse = _userService.GetUserAsync(_settingsManager.UserId).Result;
            if (userResponse.IsSuccess)
            {
                _user = userResponse.Result;
                UserBackgroundImage = _user.BackgroundUserImagePath;
                UserImagePath = _user.AvatarPath;
                UserMail = _user.Email;
                UserName = _user.Name;
                // OldPassword = user.Password;
            }

            return Task.CompletedTask;
        }

        #endregion

        #region --- Private Helpers ---

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

        private bool IsChanged => _user.Name != UserName || _user.Email != UserMail ||
            _user.AvatarPath != UserImagePath || _user.BackgroundUserImagePath != UserBackgroundImage ||
            !string.IsNullOrEmpty(OldPassword) || !string.IsNullOrEmpty(NewPassword);

        private Task OnCheckCommandAsync(bool isIndirectCall = false)
        {
            bool isAllValid = true;
            if (!IsChanged)
            {
                return Task.CompletedTask;
            }

            var param = new DialogParameters();
            param.Add(Constants.DialogParameterKeys.TITLE, $"{Resources.Resource.Save_changes}?");
            param.Add(Constants.DialogParameterKeys.OK_BUTTON_TEXT, Resources.Resource.Ok);
            param.Add(Constants.DialogParameterKeys.CANCEL_BUTTON_TEXT, Resources.Resource.Cancel);

            _dialogService.ShowDialog(nameof(AlertView), param, CloseDialogCallback);
            async void CloseDialogCallback(IDialogResult dialogResult)
            {
                var param = new DialogParameters();
                param.Add(Constants.DialogParameterKeys.OK_BUTTON_TEXT, Resources.Resource.Ok);

                bool result = (bool)dialogResult?.Parameters[Constants.DialogParameterKeys.ACCEPT];
                if (result)
                {
                    if (!string.IsNullOrEmpty(OldPassword) || !string.IsNullOrEmpty(NewPassword))
                    {
                        if (string.IsNullOrEmpty(OldPassword))
                        {
                            param.Add("title", Resources.Resource.Old_password_is_empty);
                            isAllValid = false;
                        }
                        else if (OldPassword != _user.Password)
                        {
                            param.Add("title", Resources.Resource.Old_password_is_wrong);
                            isAllValid = false;
                        }

                        if (!string.IsNullOrEmpty(NewPassword) && Regex.IsMatch(NewPassword, Constants.RegexPatterns.PASSWORD_REGEX))
                        {
                            _user.Password = NewPassword;
                        }
                        else
                        {
                            param.Add("title", Resources.Resource.New_password_is_not_valid_or_empty);
                            isAllValid = false;
                        }
                    }

                    if (!string.IsNullOrEmpty(UserName) && Regex.IsMatch(UserName, Constants.RegexPatterns.USERNAME_REGEX))
                    {
                        _user.Name = UserName;
                    }
                    else
                    {
                        param.Add("title", Resources.Resource.Name_is_not_valid_or_empty);
                        isAllValid = false;
                    }

                    if (!string.IsNullOrEmpty(UserMail) && Regex.IsMatch(UserMail, Constants.RegexPatterns.EMAIL_REGEX))
                    {
                        _user.Email = UserMail;
                    }
                    else
                    {
                        param.Add("title", Resources.Resource.Email_is_not_valid_or_empty);
                        isAllValid = false;
                    }

                    if (isAllValid)
                    {
                        _user.AvatarPath = UserImagePath;
                        _user.BackgroundUserImagePath = UserBackgroundImage;

                        await _userService.UpdateUserAsync(_user);

                        MessagingCenter.Send(this, Constants.Messages.USER_PROFILE_CHANGED);
                        await NavigationService.GoBackAsync();
                    }
                    else if (param.ContainsKey("title") && param["title"] != null)
                    {
                      _dialogService.ShowDialog(nameof(AlertView), param);
                    }
                }
                else if (isIndirectCall)
                {
                    await NavigationService.GoBackAsync();
                }
            }

            return Task.CompletedTask;
        }

        private async Task OnNavigationCommandAsync()
        {
            if (IsChanged)
            {
                await OnCheckCommandAsync(true);
            }
            else
            {
                await NavigationService.GoBackAsync();
            }
        }

        #endregion

    }
}
