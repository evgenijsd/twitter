using Xamarin.Forms;

namespace InterTwitter.Views
{
    public partial class FlyOutPage : FlyoutPage
    {
        public FlyOutPage()
        {
            InitializeComponent();
            NavigationPage.SetHasNavigationBar(this, false);
        }
    }
}