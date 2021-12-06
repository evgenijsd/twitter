using System;
using System.Threading.Tasks;
using Xamarin.Forms.Xaml;

namespace InterTwitter.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class FlyoutPageFlyout : BaseContentPage
    {
        public FlyoutPageFlyout()
        {
            InitializeComponent();
        }

        public async void SettingsTappedHandler(object sender, EventArgs e)
        {
            settingsLabel.TextColor = (Xamarin.Forms.Color)Prism.PrismApplicationBase.Current.Resources["appcolor_i1"];
            await Task.Delay(300);
            settingsLabel.TextColor = (Xamarin.Forms.Color)Prism.PrismApplicationBase.Current.Resources["appcolor_i3"];
        }

        public async void LogoutTappedHandler(object sender, EventArgs e)
        {
            logoutLabel.TextColor = (Xamarin.Forms.Color)Prism.PrismApplicationBase.Current.Resources["appcolor_i3"];
            logoutImage.Source = (Xamarin.Forms.ImageSource)Prism.PrismApplicationBase.Current.Resources["ic_logout_gray"];
            await Task.Delay(300);
            logoutImage.Source = (Xamarin.Forms.ImageSource)Prism.PrismApplicationBase.Current.Resources["ic_logout_blue"];
            logoutLabel.TextColor = (Xamarin.Forms.Color)Prism.PrismApplicationBase.Current.Resources["appcolor_i1"];
        }
    }
}