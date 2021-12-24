using FFImageLoading.Forms.Platform;
using Foundation;
using InterTwitter.iOS.Services.PermissionsService;
using InterTwitter.iOS.Services.VideoService;
using InterTwitter.Services.PermissionsService;
using InterTwitter.Services.VideoService;
using Prism;
using Prism.Ioc;
using Prism.Unity;
using InterTwitter.Helpers;
using InterTwitter.iOS.Renderers;
using UIKit;

namespace InterTwitter.iOS
{
    [Register("AppDelegate")]
    public partial class AppDelegate : global::Xamarin.Forms.Platform.iOS.FormsApplicationDelegate
    {
        #region -- Overrides --

        public override bool FinishedLaunching(UIApplication app, NSDictionary options)
        {
            global::Xamarin.Forms.Forms.Init();

            FFImageLoading.Forms.Platform.CachedImageRenderer.Init();
            Sharpnado.Shades.iOS.iOSShadowsRenderer.Initialize();
            
            CachedImageRenderer.Init();
            CachedImageRenderer.InitImageSourceHandler();
            Sharpnado.Shades.iOS.iOSShadowsRenderer.Initialize();

            LoadApplication(new App(new IOSInitializer()));

            return base.FinishedLaunching(app, options);
        }

        public class IOSInitializer : IPlatformInitializer
        {
            public void RegisterTypes(IContainerRegistry containerRegistry)
            {
                containerRegistry.RegisterSingleton<IPermissionsService, PermissionsService>();
                containerRegistry.RegisterSingleton<IVideoService, VideoService>();
                containerRegistry.RegisterSingleton<IKeyboardHelper, KeyboardHelper>();
            }
        }

        public override bool ContinueUserActivity(UIApplication application, NSUserActivity userActivity, UIApplicationRestorationHandler completionHandler)
        {
            if (Xamarin.Essentials.Platform.ContinueUserActivity(application, userActivity, completionHandler))
                return true;

            return base.ContinueUserActivity(application, userActivity, completionHandler);
        }

        #endregion
    }
}
