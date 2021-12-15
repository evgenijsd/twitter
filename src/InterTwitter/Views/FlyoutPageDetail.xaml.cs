using InterTwitter.Controls;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace InterTwitter.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class FlyoutPageDetail : CustomTabbedPage
    {
        public FlyoutPageDetail()
        {
            InitializeComponent();
            NavigationPage.SetHasNavigationBar(this, false);
        }
    }
}