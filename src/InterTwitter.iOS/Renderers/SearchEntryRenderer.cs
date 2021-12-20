using InterTwitter.Controls;
using InterTwitter.iOS.Renderers.Controls;
using System;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(SearchEntry), typeof(SearchEntryRenderer))]
namespace InterTwitter.iOS.Renderers.Controls
{
    class SearchEntryRenderer : EntryRenderer
    {
        #region -- Overrides --

        protected override void OnElementChanged(ElementChangedEventArgs<Entry> e)
        {
            base.OnElementChanged(e);

            if (Control != null)
            {
                Control.BorderStyle = UITextBorderStyle.None;

                var uTextField = Control as UITextField;

                uTextField.EditingDidBegin += (object sender, EventArgs eIos) =>
                {
                    uTextField.PerformSelector(new ObjCRuntime.Selector("selectAll"), null, 0.0f);
                };

                var searchEntry = Element as SearchEntry;

                var tintColor = UIColor.FromCGColor(searchEntry.HighlightColor.ToCGColor());
                Control.TintColor = tintColor;
            }
        }

        #endregion
    }
}