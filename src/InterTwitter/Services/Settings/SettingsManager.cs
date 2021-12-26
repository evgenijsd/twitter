using Xamarin.Essentials;

namespace InterTwitter.Services.Settings
{
    public class SettingsManager : ISettingsManager
    {
        #region -- ISettingsManager implementation --

        public int UserId
        {
            get => Preferences.Get(nameof(UserId), default(int));
            set => Preferences.Set(nameof(UserId), value);
        }

        #endregion
    }
}
