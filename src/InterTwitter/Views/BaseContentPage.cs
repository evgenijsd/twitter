using Xamarin.Forms;
using Xamarin.Forms.PlatformConfiguration;
using Xamarin.Forms.PlatformConfiguration.iOSSpecific;

namespace InterTwitter.Views
{
    public class BaseContentPage : ContentPage
    {
        public BaseContentPage()
        {
            On<iOS>().SetUseSafeArea(true);
        }
    }

    #region -- Public properties --
    #endregion
    #region -- InterfaceName implementation --
    #endregion
    #region -- Overrides --
    #endregion
    #region -- Public helpers --
    #endregion
    #region -- Private helpers --
    #endregion
}
