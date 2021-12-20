using Android.Content;
using Android.Graphics;
using Android.Graphics.Drawables;
using Android.OS;
using Android.Runtime;
using Android.Widget;
using InterTwitter.Controls;
using InterTwitter.Droid.Renderers.Controls;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using Color = Android.Graphics.Color;

[assembly: ExportRenderer(typeof(SearchEntry), typeof(SearchEntryRenderer))]
namespace InterTwitter.Droid.Renderers.Controls
{
    public class SearchEntryRenderer : EntryRenderer
    {
        public SearchEntryRenderer(Context context)
            : base(context)
        {
        }

        protected override void OnElementChanged(ElementChangedEventArgs<Entry> e)
        {
            base.OnElementChanged(e);

            if (Control != null)
            {
                Control.SetPadding(0, 0, 0, 0);
                Control.SetBackgroundColor(Color.Transparent);
            }

            if (e.OldElement == null)
            {
                var editText = Control as EditText;
                editText.SetSelectAllOnFocus(true);
            }

            //if (this.Control != null)
            //{
            //    if (e.NewElement != null)
            //    {
            //        //You can also change other colors
            //        this.ChangeCursorColor(Color.Orange);
            //    }
            //}

            //Control.SetHighlightColor((Element as SearchEntry).HighlightColor.ToAndroid());

            //if (Build.VERSION.SdkInt >= BuildVersionCodes.Q)
            //{
            //    Control.SetTextCursorDrawable(Resource.Drawable.my_cursor);
            //}
            //else
            //{
            //    IntPtr IntPtrtextViewClass = JNIEnv.FindClass(typeof(TextView));
            //    IntPtr mCursorDrawableResProperty = JNIEnv.GetFieldID(IntPtrtextViewClass, "my_cursor", "I");
            //    JNIEnv.SetField(Control.Handle, mCursorDrawableResProperty, Resource.Drawable.my_cursor);
            //}
        }

        private void ChangeCursorColor(Color color)
        {
            if (this.Control == null)
            {
                return;
            }

            var cursorResource = Java.Lang.Class.FromType(typeof(TextView)).GetDeclaredField("mCursorDrawableRes");

            cursorResource.Accessible = true;

            int resId = cursorResource.GetInt(this.Control);

            Drawable cursorDrawable = Context.GetDrawable(resId);

            cursorDrawable.SetColorFilter(color, PorterDuff.Mode.SrcIn);

            if (Build.VERSION.SdkInt >= Android.OS.BuildVersionCodes.Q)
            {
                this.Control.TextCursorDrawable = cursorDrawable;
            }
        }
    }
}