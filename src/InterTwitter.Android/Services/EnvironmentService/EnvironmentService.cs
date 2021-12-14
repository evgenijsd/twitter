using Android.OS;
using Android.Views;
using InterTwitter.Services.EnvironmentService;
using System.Drawing;
using Xamarin.Essentials;

namespace InterTwitter.Droid.Services.EnvironmentService
{
    public class EnvironmentService : IEnvironmentService
    {
        private StatusBarVisibility flag = (StatusBarVisibility)SystemUiFlags.LightStatusBar;

        public Color GetStatusBarColor()
        {
            Color color;

            if (Build.VERSION.SdkInt < Android.OS.BuildVersionCodes.Lollipop)
            {
                color = Color.Blue;
            }
            else
            {
                color = Color.FromArgb(Platform.CurrentActivity.Window.StatusBarColor);
            }

            return color;
        }

        public void SetStatusBarColor(System.Drawing.Color color, bool darkStatusBarTint)
        {
            if (Build.VERSION.SdkInt >= Android.OS.BuildVersionCodes.Lollipop)
            {
                var window = Platform.CurrentActivity.Window;
                window.AddFlags(Android.Views.WindowManagerFlags.DrawsSystemBarBackgrounds);
                window.ClearFlags(Android.Views.WindowManagerFlags.TranslucentStatus);
                window.SetStatusBarColor(color.ToPlatformColor());

                if (Build.VERSION.SdkInt >= Android.OS.BuildVersionCodes.M)
                {
                    window.DecorView.SystemUiVisibility = darkStatusBarTint ? flag : 0;
                }
            }
        }

        public bool GetUseDarkStatusBarTint()
        {
            bool result = false;

            if (Build.VERSION.SdkInt >= Android.OS.BuildVersionCodes.Lollipop)
            {
                var window = Platform.CurrentActivity.Window;
                result = window.DecorView.SystemUiVisibility == flag;
            }

            return result;
        }
    }
}