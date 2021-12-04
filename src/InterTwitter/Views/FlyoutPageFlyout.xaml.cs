using System;
using System.Threading;
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

        public async void OnSettingsTapped(object sender, EventArgs e)
        {
            settingsLabel.TextColor = (Xamarin.Forms.Color)Prism.PrismApplicationBase.Current.Resources["appcolor_i1"];
            await Task.Delay(300);
            settingsLabel.TextColor = (Xamarin.Forms.Color)Prism.PrismApplicationBase.Current.Resources["appcolor_i3"];
        }
    }
}