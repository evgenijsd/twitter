﻿using Foundation;
using InterTwitter.Services.PermissionsService;
using System.Threading.Tasks;
using UIKit;
using Xamarin.Essentials;

namespace InterTwitter.iOS.Services.PermissionsService
{
    public class PermissionsService : IPermissionsService
    {
        public Task<PermissionStatus> CheckStatusAsync<T>()
            where T : Permissions.BasePermission, new()
        {
            return Permissions.CheckStatusAsync<T>();
        }

        public async Task<PermissionStatus> RequestAsync<T>()
            where T : Permissions.BasePermission, new()
        {
            var status = await Permissions.CheckStatusAsync<T>();

            //HACK for iOS 14
            if (status == PermissionStatus.Unknown)
            {
                await Permissions.RequestAsync<T>();

                while ((status = await Permissions.CheckStatusAsync<T>()) == PermissionStatus.Unknown)
                {
                    await Task.Delay(50);
                }
            }

            if (status == PermissionStatus.Denied)
            {
                var okCanselAlertController = UIAlertController.Create(
                    "Permission denied", 
                    "Change permission settings", 
                    UIAlertControllerStyle.Alert);

                okCanselAlertController.AddAction(UIAlertAction.Create("Ok", UIAlertActionStyle.Default, alert =>
                    UIApplication.SharedApplication.OpenUrl(new NSUrl("app-settings:"))));

                okCanselAlertController.AddAction(UIAlertAction.Create("Cansel", UIAlertActionStyle.Default, null));

                var vc = UIApplication.SharedApplication.KeyWindow.RootViewController;

                while (vc.PresentedViewController != null)
                {
                    vc = vc.PresentedViewController;
                }

                vc.PresentViewController(okCanselAlertController, true, null);
            }

            return status;
        }
    }
}