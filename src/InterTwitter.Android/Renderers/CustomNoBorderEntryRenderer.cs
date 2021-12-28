using Android.Content;
using InterTwitter.Controls;
using InterTwitter.Droid.Renderers;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(CustomNoBorderEntry), typeof(CustomNoBorderEntryRenderer))]
namespace InterTwitter.Droid.Renderers
{
    public class CustomNoBorderEntryRenderer : EntryRenderer
    {
        public CustomNoBorderEntryRenderer(Context context) 
            : base(context)
        {
        }

        protected override void OnElementChanged(ElementChangedEventArgs<Entry> e)
        {
            base.OnElementChanged(e);

            if (e.OldElement == null && Control != null)
            {
                Control.Background = null;

                Control.SetPadding(0, 0, 0, 0);
            }
        }
    }
}