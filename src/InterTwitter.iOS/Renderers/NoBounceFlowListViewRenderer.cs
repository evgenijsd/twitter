using DLToolkit.Forms.Controls;
using Foundation;
using InterTwitter.Controls;
using InterTwitter.iOS.Renderers;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(FlowListView), typeof(FlowListViewRenderer))]
namespace InterTwitter.iOS.Renderers
{
    public class FlowListViewRenderer : ListViewRenderer
    {
        #region -- Overrides --

        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            base.OnElementPropertyChanged(sender, e);

            if (Control != null)
            {
                Control.Bounces = false;
            }
        }

        #endregion
    }
}