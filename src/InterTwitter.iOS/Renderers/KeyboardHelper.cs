using InterTwitter.Helpers;
using UIKit;

namespace InterTwitter.iOS.Renderers
{
    public class KeyboardHelper : IKeyboardHelper
    {
        public void HideKeyboard()
        {
            UIApplication.SharedApplication.KeyWindow.EndEditing(true);
        }

        public void ShowKeyboard()
        {
            UIApplication.SharedApplication.KeyWindow.EndEditing(false);
        }
    }
}