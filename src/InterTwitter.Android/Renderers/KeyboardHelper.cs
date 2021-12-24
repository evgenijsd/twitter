using Android.App;
using Android.Content;
using Android.Views.InputMethods;
using InterTwitter.Helpers;

namespace InterTwitter.Droid.Renderers
{
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

        public void ShowKeyboard()
        {
            var inputMethodManager = _context?.GetSystemService(Context.InputMethodService) as InputMethodManager;

            if (inputMethodManager != null && _context is Activity activity)
            {
                var token = activity.CurrentFocus?.WindowToken;
                inputMethodManager.ToggleSoftInput(ShowFlags.Forced, HideSoftInputFlags.ImplicitOnly);
            }
        }
    }
}