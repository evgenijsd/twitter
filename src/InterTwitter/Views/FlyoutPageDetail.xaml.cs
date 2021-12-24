using InterTwitter.Controls;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace InterTwitter.Views
{
    public partial class FlyoutPageDetail : CustomTabbedPage
    {
        public FlyoutPageDetail()
        {
            InitializeComponent();
            NavigationPage.SetHasNavigationBar(this, false);
        }
    }
}