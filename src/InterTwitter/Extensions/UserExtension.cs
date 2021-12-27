using InterTwitter.Models;
using InterTwitter.ViewModels;

namespace InterTwitter.Extensions
{
    public static class UserExtension
    {
        #region -- Public methods --

        public static VcfUser ToVcfUser(this UserModel userModel) => new VcfUser
        {
            Name = userModel.Name,
            Email = userModel.Email,
            Avatar = userModel.AvatarPath,
        };

        public static UserViewModel ToUserViewModel(this UserModel userModel)
        {
            return new UserViewModel
            {
                Id = userModel.Id,
                Name = userModel.Name,
                Email = userModel.Email,
                Password = userModel.Password,
                AvatarPath = userModel.AvatarPath,
                BackgroundUserImagePath = userModel.BackgroundUserImagePath,
            };
        }

        public static UserModel ToUserModel(this UserViewModel userViewModel)
        {
            return new UserModel
            {
                Id = userViewModel.Id,
                Name = userViewModel.Name,
                Email = userViewModel.Email,
                Password = userViewModel.Password,
                AvatarPath = userViewModel.AvatarPath,
                BackgroundUserImagePath = userViewModel.BackgroundUserImagePath,
            };
        }

        #endregion
    }
}
