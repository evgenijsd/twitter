using Android.App;
using Android.Content;
using Android.OS;
using AndroidX.AppCompat.App;

namespace InterTwitter.Droid
{
    [Activity(
        Theme = "@style/LaunchTheme", 
        MainLauncher = true, 
        NoHistory = true)]
    public class SplashActivity : AppCompatActivity
    {
        protected override void OnResume()
        {
            base.OnResume();

            StartActivity(typeof(MainActivity));
        }
    }
}