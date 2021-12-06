using Foundation;
using InterTwitter.Helpers;
using InterTwitter.iOS.Renderers;
using UIKit;
using Xamarin.Forms;

[assembly: Dependency(typeof(KeyboardHelper))]
namespace InterTwitter.iOS.Renderers
{
    [Preserve(AllMembers = true)]
    public class KeyboardHelper : IKeyboardHelper
    {
        public void HideKeyboard()
        {
            UIApplication.SharedApplication.KeyWindow.EndEditing(true);
        }
    }
}