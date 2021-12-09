﻿using InterTwitter.Services.PermissionsService;
using System.Threading.Tasks;
using Xamarin.Essentials;

namespace InterTwitter.Droid.Services.PermissionsService
{
    public class PermissionsService : IPermissionsService
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