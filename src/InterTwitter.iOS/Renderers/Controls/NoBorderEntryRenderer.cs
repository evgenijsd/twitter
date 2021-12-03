using MapNotepad.iOS.Renderer.Controls;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(Entry), typeof(NoBorderEntryRenderer))]
namespace MapNotepad.iOS.Renderer.Controls
{
    class NoBorderEntryRenderer : EntryRenderer
    {
        protected override void OnElementChanged(ElementChangedEventArgs<Entry> e)
        {
            base.OnElementChanged(e);

            if(Control != null)
            {
                Control.BorderStyle = UITextBorderStyle.None;
            }
        }
    }
}