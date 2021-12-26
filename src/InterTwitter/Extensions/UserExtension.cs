using InterTwitter.Models;

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

        #endregion
    }
}
