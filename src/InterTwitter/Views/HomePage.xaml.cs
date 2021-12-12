using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace InterTwitter.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class HomePage : BaseContentPage
    {
        public HomePage()
        {
            InitializeComponent();

            NavigationPage.SetHasNavigationBar(this, false);
        }
    }
}