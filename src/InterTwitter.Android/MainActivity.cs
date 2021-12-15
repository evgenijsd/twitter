using Android.App;
using Android.Content.PM;
using Android.OS;
using Android.Runtime;
using FFImageLoading.Forms.Platform;
using InterTwitter.Droid.Services.PermissionsService;
using InterTwitter.Droid.Services.VideoService;
using InterTwitter.Services.PermissionsService;
using InterTwitter.Services.VideoService;
using Prism;
using Prism.Ioc;

namespace InterTwitter.Droid
{
    [Activity(Label = "InterTwitter", Icon = "@mipmap/icon", Theme = "@style/MainTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation | ConfigChanges.UiMode | ConfigChanges.ScreenLayout | ConfigChanges.SmallestScreenSize )]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        private static IPermissionsService _permissionsService = new PermissionsService();
        private static IVideoService _videoService = new VideoService();

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            global::Xamarin.Forms.Forms.Init(this, savedInstanceState);
            FFImageLoading.Forms.Platform.CachedImageRenderer.Init(true);
            CachedImageRenderer.Init(true); 
            CachedImageRenderer.InitImageViewHandler();

            LoadApplication(new App(new AndroidInitializer()));
        }
        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }

        public class AndroidInitializer : IPlatformInitializer
        {
            public void RegisterTypes(IContainerRegistry containerRegistry)
            {
                containerRegistry.RegisterInstance(_permissionsService);
                containerRegistry.RegisterInstance(_videoService);
            }
        }
    }
}