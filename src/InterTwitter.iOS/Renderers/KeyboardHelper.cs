using Foundation;
using InterTwitter.Helpers;
using InterTwitter.iOS.Renderers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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