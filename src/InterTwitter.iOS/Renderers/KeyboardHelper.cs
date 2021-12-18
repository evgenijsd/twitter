using Foundation;
using InterTwitter.Helpers;
using InterTwitter.iOS.Renderers;
using UIKit;
using Xamarin.Forms;

namespace InterTwitter.iOS.Renderers
{
    public class KeyboardHelper : IKeyboardHelper
    {
        public void HideKeyboard()
        {
            UIApplication.SharedApplication.KeyWindow.EndEditing(true);
        }
    }
}