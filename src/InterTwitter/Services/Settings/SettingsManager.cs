using Xamarin.Essentials;

namespace InterTwitter.Services
{
    public class SettingsManager : ISettingsManager
    {
        #region -- Public properties --

        public int UserId
        {
            get => Preferences.Get(nameof(UserId), 0);
            set => Preferences.Set(nameof(UserId), value);
        }

        #endregion
    }
}
