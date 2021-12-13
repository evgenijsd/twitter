using Foundation;
using InterTwitter.Controls;
using InterTwitter.iOS.Renderers;
using System;
using System.ComponentModel;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(LineSpacingLabel), typeof(LineSpacingLabelRenderer))]
namespace InterTwitter.iOS.Renderers
{
    public class LineSpacingLabelRenderer : LabelRenderer
    {
        public LineSpacingLabelRenderer()
        {
        }

        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            var lineSpacingLabel = (LineSpacingLabel)Element;

            if (Control != null && lineSpacingLabel != null && lineSpacingLabel.Text != null)
            {
                var paragraphStyle = new NSMutableParagraphStyle()
                {
                    LineSpacing = (nfloat)lineSpacingLabel.LineSpacing
                };

                var str = new NSMutableAttributedString(lineSpacingLabel.Text);
                var style = UIStringAttributeKey.ParagraphStyle;
                var range = new NSRange(0, str.Length);

                str.AddAttribute(style, paragraphStyle, range);

                Control.AttributedText = str;
                Control.TextAlignment = UITextAlignment.Center;
            }
        }
    }
}