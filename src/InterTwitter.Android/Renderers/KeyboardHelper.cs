using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Views.InputMethods;
using Android.Widget;
using InterTwitter.Droid.Renderers;
using InterTwitter.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xamarin.Forms;

[assembly: Dependency(typeof(KeyboardHelper))]
namespace InterTwitter.Droid.Renderers
{
    [Preserve(AllMembers = true)]
    public class KeyboardHelper : IKeyboardHelper
    {
        static Context _context;

        public static void Init(Context context)
        {
            _context = context;
        }

        public void HideKeyboard()
        {
            var inputMethodManager = _context?.GetSystemService(Context.InputMethodService) as InputMethodManager;
            if (inputMethodManager != null && _context is Activity activity)
            {
                var token = activity.CurrentFocus?.WindowToken;
                inputMethodManager.HideSoftInputFromWindow(token, HideSoftInputFlags.None);
                activity.Window.DecorView.ClearFocus();
            }
        }
    }
}