using InterTwitter.Services.PermissionsService;
using InterTwitter.Services.Settings;
using InterTwitter.Services.UserService;
using MapNotepad.Helpers;
using Prism.Navigation;
using System;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Essentials;

namespace InterTwitter.ViewModels
{
    public class EditProfilePageViewModel : BaseViewModel
    {
        private readonly IPermissionsService _permissionsService;
        private readonly ISettingsManager _settingsManager;
        private readonly IUserService _userService;

        public EditProfilePageViewModel(INavigationService navigationService, IPermissionsService permissionsService, ISettingsManager settingsManager, IUserService userService)
            : base(navigationService)
        {
            _permissionsService = permissionsService;
            _settingsManager = settingsManager;
            _userService = userService;
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

        public ICommand NavigationCommandAsync => SingleExecutionCommand.FromFunc(() => NavigationService.GoBackAsync());

        public ICommand CheckCommandAsync => SingleExecutionCommand.FromFunc(OnCheckCommandAsync);

        public ICommand PickBackgroundImageAsync => SingleExecutionCommand.FromFunc(OnPickBackgroundImageAsync);

        public ICommand PickUserImageAsync => SingleExecutionCommand.FromFunc(OnPickUserImageAsync);

        #endregion

        #region -- Overrides --

        public override Task InitializeAsync(INavigationParameters parameters)
        {
            UserBackgroundImage = "https://picsum.photos/500/500?image=182";
            UserImagePath = "https://picsum.photos/500/500?image=290";
            UserMail = "aaa@asd.ru";
            UserName = "Vasya";
            OldPassword = "irtkegrokjojoijongo6@@JJJK@4556";
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

        private Task OnCheckCommandAsync()
        {
            return Task.CompletedTask;
        }

        #endregion

    }
}
