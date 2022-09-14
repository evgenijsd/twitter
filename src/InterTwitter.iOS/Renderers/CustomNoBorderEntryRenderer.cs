﻿using InterTwitter.Controls;
using InterTwitter.iOS.Renderers;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(CustomNoBorderEntry), typeof(CustomNoBorderEntryRenderer))]
namespace InterTwitter.iOS.Renderers
{
    class CustomNoBorderEntryRenderer : EntryRenderer
    {

        #region -- Ovverides --

        protected override void OnElementChanged(ElementChangedEventArgs<Entry> e)
        {
            base.OnElementChanged(e);

            if (Control != null)
            {
                Control.BorderStyle = UITextBorderStyle.None;
            }
        }

        #endregion

    }
}
