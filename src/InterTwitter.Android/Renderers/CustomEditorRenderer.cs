using Android.Content;
using Android.Text;
using Android.Text.Style;
using Android.Widget;
using InterTwitter.Controls;
using InterTwitter.Droid.Renderers;
using System.ComponentModel;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(CustomEditor), typeof(CustomEditorRenderer))]
namespace InterTwitter.Droid.Renderers
{
    public class CustomEditorRenderer : EditorRenderer
    {
        private bool _clear;

        public CustomEditorRenderer(Context context) 
            : base(context)
        {
        }

        #region -- Overrides --

        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            base.OnElementPropertyChanged(sender, e);

            switch (e.PropertyName)
            {
                case "Text":
                    Check();
                    break;

                case "IsExpandable":
                    Control.VerticalScrollBarEnabled = !((CustomEditor)Element).IsExpandable;
                    break;
            }
        }

        protected override void OnElementChanged(ElementChangedEventArgs<Editor> e)
        {
            base.OnElementChanged(e);

            Control.SetPadding(0, 0, 0, 0);
            Control.SetBackgroundColor(e.NewElement.BackgroundColor.ToAndroid());
            Control.VerticalScrollBarEnabled = !((CustomEditor)Element).IsExpandable;

            if (e.NewElement != null)
            {
                Check();
            }
        }

        #endregion

        #region -- Private methods --

        private void Check()
        {
            var text = EditText.Text;

            if (!string.IsNullOrEmpty(text))
            {
                EditText.SetTextColor(Element.TextColor.ToAndroid());
                
                var length = text.Length;
                var correctLength = ((CustomEditor)Element).CorrectLength;

                var pos = Control.SelectionEnd;

                if (length > correctLength)
                {
                    _clear = true;

                    SpannableString spannable = new SpannableString(text);

                    spannable.SetSpan(new ForegroundColorSpan(
                        ((CustomEditor)Element).OverflowLengthColor.ToAndroid()),
                        correctLength,
                        length,
                        SpanTypes.ExclusiveExclusive);

                    EditText.TextFormatted = spannable;

                    Control.SetSelection(pos);
                }
                else
                {
                    if (_clear)
                    {
                        EditText.Text = text;
                        Control.SetSelection(pos);

                        _clear = false;
                    }
                }
            }
        }

        #endregion
    }
}