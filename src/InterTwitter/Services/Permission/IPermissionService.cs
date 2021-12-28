using System.Threading.Tasks;
using Xamarin.Essentials;

namespace InterTwitter.Services.Permission
{
    public interface IPermissionService
    {
        Task<PermissionStatus> CheckStatusAsync<T>()
            where T : Permissions.BasePermission, new();

        Task<PermissionStatus> RequestAsync<T>()
            where T : Permissions.BasePermission, new();
    }
}