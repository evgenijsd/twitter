using System.Threading.Tasks;
using Xamarin.Essentials;

namespace InterTwitter.Droid.Services.Permission
{
    interface IPermissionService
    {
        Task<PermissionStatus> CheckStatusAsync<T>()
            where T : Permissions.BasePermission, new();

        Task<PermissionStatus> RequestAsync<T>()
            where T : Permissions.BasePermission, new();
    }
}