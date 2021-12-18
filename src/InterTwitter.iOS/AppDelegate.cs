using System;
using System.Collections.Generic;
using System.Linq;
using FFImageLoading.Forms.Platform;
using Foundation;
using InterTwitter.Helpers;
using InterTwitter.iOS.Renderers;
using Prism;
using Prism.Ioc;
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
            CachedImageRenderer.Init();
            CachedImageRenderer.InitImageSourceHandler();
            Sharpnado.Shades.iOS.iOSShadowsRenderer.Initialize();
            LoadApplication(new App(new iOSInitializer()));

            return base.FinishedLaunching(app, options);
        }

        #endregion

        public class iOSInitializer : IPlatformInitializer
        {
            public void RegisterTypes(IContainerRegistry containerRegistry)
            {
                containerRegistry.RegisterSingleton<IKeyboardHelper, KeyboardHelper>();
            }
        }
    }
}
