using System.Threading.Tasks;
using Xamarin.Essentials;

namespace InterTwitter.Services
{
    public class PermissionService : IPermissionService
    {
        public Task<PermissionStatus> CheckStatusAsync<T>()
            where T : Permissions.BasePermission, new()
        {
            return Permissions.CheckStatusAsync<T>();
        }

        public Task<PermissionStatus> RequestAsync<T>()
            where T : Permissions.BasePermission, new()
        {
            var status = CheckStatusAsync<T>();

            if (status.Result != PermissionStatus.Granted)
            {
                status = Permissions.RequestAsync<T>();
            }

            return status;
        }
    }
}