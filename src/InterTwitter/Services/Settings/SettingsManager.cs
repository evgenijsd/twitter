using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Essentials;

namespace InterTwitter.Services.Settings
{
    public class SettingsManager : ISettingsManager
    {
        #region -- ISettings implementation --
        public int UserId
        {
            get => Preferences.Get(nameof(UserId), 1);
            set => Preferences.Set(nameof(UserId), value);
        }

        #endregion

    }
}
