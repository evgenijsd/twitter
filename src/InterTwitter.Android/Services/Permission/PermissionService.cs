using InterTwitter.Services.Permission;
using System.Threading.Tasks;
using Xamarin.Essentials;

namespace InterTwitter.Droid.Services.Permission
{
    public class PermissionService : IPermissionService
    {
        public async Task<PermissionStatus> CheckStatusAsync<T>() where T : Permissions.BasePermission, new()
        {
            return await Permissions.CheckStatusAsync<T>();
        }

        public async Task<PermissionStatus> RequestAsync<T>() where T : Permissions.BasePermission, new()
        {
            var status = await CheckStatusAsync<T>();

            if (status != PermissionStatus.Granted)
            {
                return await Permissions.RequestAsync<T>();
            }

            return status;
        }
    }
}