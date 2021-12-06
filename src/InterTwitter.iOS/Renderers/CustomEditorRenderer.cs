using CoreAnimation;
using CoreGraphics;
using Foundation;
using ObjCRuntime;
using System;
using System.ComponentModel;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;
using Xamarin.Forms.PlatformConfiguration.iOSSpecific;

namespace InterTwitter.Droid.Renderers
{
    public class CustomEditorRenderer : EditorRenderer
    {
        protected override void OnElementChanged(ElementChangedEventArgs<Editor> e)
        {
            base.OnElementChanged(e);

            NSMutableAttributedString str = new NSMutableAttributedString("Hello, World");

            NSString attributeName = (NSString)"textColor";
            NSObject value = UIColor.Red;
            NSRange range = new NSRange(1, 5);

            str.AddAttribute(attributeName, value, range);

            this.TextView.AttributedText = str;
        }
    }
}