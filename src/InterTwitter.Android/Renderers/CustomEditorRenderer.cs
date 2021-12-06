using Android.Animation;
using Android.App;
using Android.Content;
using Android.Content.Res;
using Android.Graphics;
using Android.Graphics.Drawables;
using Android.OS;
using Android.Runtime;
using Android.Text;
using Android.Text.Method;
using Android.Text.Style;
using Android.Util;
using Android.Views;
using Android.Views.Accessibility;
using Android.Views.Animations;
using Android.Views.Autofill;
using Android.Views.InputMethods;
using Android.Widget;
using InterTwitter.Controls;
using InterTwitter.Droid.Renderers;
using Java.Interop;
using Java.Lang;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(CustomEditor), typeof(CustomEditorRenderer))]
namespace InterTwitter.Droid.Renderers
{
    public class CustomEditorRenderer : EditorRenderer
    {
        private bool _clear;
        private bool initial = true;
        private Drawable originalBackground;

        public CustomEditorRenderer(Context context) 
            : base(context)
        {
        }

        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            base.OnElementPropertyChanged(sender, e);

            if(e.PropertyName == "Text")
            {
                Check();
            }

            Control.VerticalScrollBarEnabled = false;
            this.Control.Background = originalBackground;
        }

        protected override void OnElementChanged(ElementChangedEventArgs<Editor> e)
        {
            base.OnElementChanged(e);

            if (Control != null)
            {
                if (initial)
                {
                    originalBackground = Control.Background;
                    initial = false;
                }

            }

            if (e.NewElement != null)
            {
                Check();
            }

            Control.SetBackgroundColor(e.NewElement.BackgroundColor.ToAndroid());
            //Control.SetPadding(0, 0, 0, 0);

            Control.Hint = "Text to show";
            Control.SetHintTextColor(e.NewElement.PlaceholderColor.ToAndroid());


            if (Control != null)
            {
                if (initial)
                {
                    originalBackground = Control.Background;
                    initial = false;
                }

            }

            
        }

        private void Check()
        {
            var text = EditText.Text;

            if (!string.IsNullOrEmpty(text))
            {
                EditText.SetTextColor(Android.Graphics.Color.Black);
                
                var length = text.Length;
                var pos = Control.SelectionEnd;

                if (length > 20)
                {
                    _clear = true;

                    SpannableString spannable = new SpannableString(text);
                    spannable.SetSpan(new ForegroundColorSpan(Android.Graphics.Color.Red), 20, length, SpanTypes.ExclusiveExclusive);

                    EditText.TextFormatted = spannable;
                    Control.SetSelection(pos);
                }
                else
                {
                    if (_clear)
                    {
                        string tmp = EditText.Text[length - 1].ToString();
                        if (!string.IsNullOrWhiteSpace(tmp))
                        {
                            EditText.Text = text;
                            Control.SetSelection(pos);
                        }

                        _clear = false;
                    }
                }
            }
        }
    }
}