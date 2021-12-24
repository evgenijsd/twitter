﻿using Foundation;
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

        #region -- Overrides --

        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            SetLineSpacing();
        }

        #endregion

        #region -- Private helpers --

        protected void SetLineSpacing()
        {
            var lineSpacingLabel = Element as LineSpacingLabel;

            if (Control != null && lineSpacingLabel != null && lineSpacingLabel.Text != null)
            {
                var paragraphStyle = new NSMutableParagraphStyle()
                {
                    LineSpacing = (nfloat)lineSpacingLabel.LineSpacing
                };

                var text = new NSMutableAttributedString(lineSpacingLabel.Text);
                var style = UIStringAttributeKey.ParagraphStyle;
                var range = new NSRange(0, text.Length);

                text.AddAttribute(style, paragraphStyle, range);

                Control.AttributedText = text;
                Control.TextAlignment = (UITextAlignment)lineSpacingLabel.HorizontalTextAlignment;
            }
        }

        #endregion
    }
}