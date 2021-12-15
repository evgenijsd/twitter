using Android.Content;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android.AppCompat;
using Xamarin.Forms.Platform.Android;
using Google.Android.Material.BottomNavigation;
using InterTwitter.Controls;
using InterTwitter.Droid;

[assembly: ExportRenderer(typeof(CustomTabbedPage), typeof(CustomTabbedPageRenderer))]
namespace InterTwitter.Droid
{
    public class CustomTabbedPageRenderer : TabbedPageRenderer
    {
        private TabbedPage _tabbedPage;
        private BottomNavigationView _bottomNavigationView;
        public CustomTabbedPageRenderer(Context context) : base(context)
        {
        }
        protected override void OnElementChanged(ElementChangedEventArgs<TabbedPage> e)
        {
            base.OnElementChanged(e);

            if(e.NewElement != null)
            {
                _tabbedPage = e.NewElement as CustomTabbedPage;
                _bottomNavigationView = (GetChildAt(0) as Android.Widget.RelativeLayout).GetChildAt(1) as BottomNavigationView;
                _bottomNavigationView.LabelVisibilityMode = LabelVisibilityMode.LabelVisibilityUnlabeled;
            }

        }
    }
}