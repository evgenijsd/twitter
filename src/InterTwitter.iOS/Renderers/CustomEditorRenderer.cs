using Foundation;
using InterTwitter.Controls;
using InterTwitter.iOS.Renderers;
using System.ComponentModel;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(CustomEditor), typeof(CustomEditorRenderer))]
namespace InterTwitter.iOS.Renderers
{
    public class CustomEditorRenderer : EditorRenderer
    {
        #region -- Overrides --

        protected override void OnElementChanged(ElementChangedEventArgs<Editor> e)
        {
            base.OnElementChanged(e);

            TextView.InputAccessoryView = null;
            Control.ScrollEnabled = !((CustomEditor)Element).IsExpandable;

            Check();
        }

        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            base.OnElementPropertyChanged(sender, e);

            switch (e.PropertyName)
            {
                case "Text":
                    Check();
                    break;

                case "Renderer":
                    Control.ScrollEnabled = !((CustomEditor)Element).IsExpandable;
                    break;
            }
        }

        #endregion

        #region -- Private methods --

        private void Check()
        {
            var cursorPosition = Control.SelectedTextRange;
            var text = TextView.Text;

            if (!string.IsNullOrEmpty(text))
            {
                TextView.TextColor = Element.TextColor.ToUIColor();

                var length = text.Length;
                var correctLength = ((CustomEditor)Element).CorrectLength;

                var attributedString = new NSMutableAttributedString(TextView.Text);

                var paragraphStyle = new NSMutableParagraphStyle
                {
                    LineSpacing = 5,
                };

                var style = UIStringAttributeKey.ParagraphStyle;
                var rangeAll = new NSRange(0, attributedString.Length);
                attributedString.AddAttribute(style, paragraphStyle, rangeAll);

                var styleFontSize = UIStringAttributeKey.Font;
                attributedString.AddAttribute(styleFontSize, Control.Font, rangeAll);

                if (length > correctLength)
                {
                    var value = ((CustomEditor)Element).OverflowLengthColor.ToUIColor();
                    var range = new NSRange(correctLength, length - correctLength);

                    attributedString.AddAttribute(UIStringAttributeKey.ForegroundColor, value, range);
                }

                TextView.AttributedText = attributedString;
            }

            Control.SelectedTextRange = cursorPosition;
        }

        #endregion
    }
}