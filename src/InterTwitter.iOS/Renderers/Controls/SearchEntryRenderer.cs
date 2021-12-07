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
        protected override void OnElementChanged(ElementChangedEventArgs<Entry> e)
        {
            base.OnElementChanged(e);

            if(Control != null)
            {
                Control.BorderStyle = UITextBorderStyle.None;

                var uTextField = (UITextField)Control;

                //FIX ME
                uTextField.EditingDidBegin += (object sender, EventArgs eIos) =>
                {
                    uTextField.PerformSelector(new ObjCRuntime.Selector("selectAll"), null, 0.0f);
                };
            }
        }
    }
}