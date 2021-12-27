using Android.Content;
using Android.Widget;
using InterTwitter.Controls;
using InterTwitter.Droid.Renderers.Controls;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using Color = Android.Graphics.Color;

[assembly: ExportRenderer(typeof(SearchEntry), typeof(SearchEntryRenderer))]
namespace InterTwitter.Droid.Renderers.Controls
{
    public class SearchEntryRenderer : EntryRenderer
    {
        public SearchEntryRenderer(Context context)
            : base(context)
        {
        }

        protected override void OnElementChanged(ElementChangedEventArgs<Entry> e)
        {
            base.OnElementChanged(e);

            if (Control != null)
            {
                Control.SetPadding(0, 0, 0, 0);
                Control.SetBackgroundColor(Color.Transparent);
            }

            if (e.OldElement == null)
            {
                var editText = Control as EditText;
                editText.SetSelectAllOnFocus(true);
            }
        }
    }
}