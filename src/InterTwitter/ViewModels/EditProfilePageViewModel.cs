using MapNotepad.Helpers;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace InterTwitter.ViewModels
{
    public class EditProfilePageViewModel : BaseViewModel
    {
        public EditProfilePageViewModel(INavigationService navigationService)
            : base(navigationService)
        {
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

        private string _userBackgroundImage = "https://picsum.photos/500/500?image=182";
        public string UserBackgroundImage
        {
            get => _userBackgroundImage;
            set => SetProperty(ref _userBackgroundImage, value);
        }

        private string _userImagePath = "https://picsum.photos/500/500?image=290";
        public string UserImagePath
        {
            get => _userImagePath;
            set => SetProperty(ref _userImagePath, value);
        }

        public ICommand NavigationCommandAsync => SingleExecutionCommand.FromFunc(() => NavigationService.GoBackAsync());

        #endregion

    }
}
