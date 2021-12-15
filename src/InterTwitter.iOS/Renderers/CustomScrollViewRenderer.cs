﻿using InterTwitter.Controls;
using InterTwitter.iOS.Renderers;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(CustomScrollView), typeof(CustomScrollViewRenderer))]
namespace InterTwitter.iOS.Renderers
{
    public class CustomScrollViewRenderer : ScrollViewRenderer
    {
        #region -- Overrides --

        protected override void OnElementChanged(VisualElementChangedEventArgs e)
        {
            base.OnElementChanged(e);
            Bounces = ((CustomScrollView)e.NewElement).IsBounces;
        }

        #endregion
    }
}