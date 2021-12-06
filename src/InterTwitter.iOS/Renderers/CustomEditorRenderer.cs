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
            var text = TextView.Text;

            if (!string.IsNullOrEmpty(text))
            {
                TextView.TextColor = Element.TextColor.ToUIColor();

                var length = text.Length;
                var correctLength = ((CustomEditor)Element).CorrectLength;
                //var pos = Control.SelectionAffinity;

                if (length > correctLength)
                {
                    //_clear = true;

                    NSMutableAttributedString str = new NSMutableAttributedString(TextView.Text);

                    NSObject value = ((CustomEditor)Element).OverflowLengthColor.ToUIColor();
                    NSRange range = new NSRange(correctLength, length - correctLength);

                    str.AddAttribute(UIStringAttributeKey.ForegroundColor, value, range);

                    TextView.AttributedText = str;

                    //Control.SetSelectionAffinity(pos);
                }
                else
                {
                    //if (_clear)
                    //{
                    //    EditText.Text = text;
                    //    Control.SetSelection(pos);

                    //    _clear = false;
                    //}
                }
            }
        }

        #endregion
    }
}