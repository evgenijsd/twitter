using InterTwitter.Controls;
using InterTwitter.iOS.Renderers.Controls;
using System;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(ListView), typeof(NonSelectableListViewRenderer))]
namespace InterTwitter.iOS.Renderers.Controls
{
    class NonSelectableListViewRenderer : ListViewRenderer
    {
        protected override void OnElementChanged(ElementChangedEventArgs<ListView> e)
        {
            base.OnElementChanged(e);

            if(Control != null)
            {
                Control.DeselectRow(Control.IndexPathForSelectedRow, true);
            }
        }
    }
}