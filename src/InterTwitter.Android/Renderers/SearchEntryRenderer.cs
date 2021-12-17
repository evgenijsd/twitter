using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Widget;
using InterTwitter.Controls;
using InterTwitter.Droid.Renderers.Controls;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

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
                Control.SetBackgroundColor(Android.Graphics.Color.Transparent);
            }

            if (e.OldElement == null)
            {
                var editText = (EditText)Control;
                editText.SetSelectAllOnFocus(true);
            }

            var searchEntry = (SearchEntry)Element;

             
            if (Build.VERSION.SdkInt >= BuildVersionCodes.Q)
            {
                Control.SetTextCursorDrawable(Resource.Drawable.my_cursor); //This API Intrduced in android 10
            }
            else
            {
                IntPtr IntPtrtextViewClass = JNIEnv.FindClass(typeof(TextView));
                IntPtr mCursorDrawableResProperty = JNIEnv.GetFieldID(IntPtrtextViewClass, "my_cursor", "I");
                JNIEnv.SetField(Control.Handle, mCursorDrawableResProperty, Resource.Drawable.my_cursor);
            }
        }
    }
}