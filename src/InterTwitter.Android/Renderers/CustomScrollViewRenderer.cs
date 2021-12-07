using Android.Content;
using InterTwitter.Controls;
using InterTwitter.Droid.Renderers;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(CustomScrollView), typeof(CustomScrollViewRenderer))]
namespace InterTwitter.Droid.Renderers
{
    public class CustomScrollViewRenderer : ScrollViewRenderer
    {
        public CustomScrollViewRenderer(Context context)
            : base(context)
        {

        }

        protected override void OnElementChanged(VisualElementChangedEventArgs e)
        {
            base.OnElementChanged(e);

            if (e.NewElement as CustomScrollView != null)
            {
                OverScrollMode = ((CustomScrollView)e.NewElement).IsBounces ?
                Android.Views.OverScrollMode.IfContentScrolls :
                Android.Views.OverScrollMode.Never;
            }
        }
    }
}