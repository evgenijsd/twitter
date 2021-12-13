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
            var lineSpacingLabel = (LineSpacingLabel)this.Element;
            var paragraphStyle = new NSMutableParagraphStyle()
            {
                LineSpacing = (nfloat)lineSpacingLabel.LineSpacing
            };
            var text = new NSMutableAttributedString(lineSpacingLabel.Text);
            var style = UIStringAttributeKey.ParagraphStyle;
            var range = new NSRange(0, text.Length);
                
            text.AddAttribute(style, paragraphStyle, range);

            this.Control.AttributedText = text;
        }
    }
}