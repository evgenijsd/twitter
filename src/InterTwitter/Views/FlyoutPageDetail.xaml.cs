using InterTwitter.Controls;
using InterTwitter.Helpers;
using System.Runtime.CompilerServices;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace InterTwitter.Views
{
    public partial class FlyoutPageDetail : CustomTabbedPage
    {
        private bool _initDone;

        public FlyoutPageDetail()
        {
            InitializeComponent();
            NavigationPage.SetHasNavigationBar(this, false);
        }

        protected override void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            base.OnPropertyChanged(propertyName);

            if (!_initDone && propertyName == nameof(CurrentPage) && CurrentPage is HomePage page && page.BindingContext is IPageActionsHandler vm)
            {
                vm.OnAppearing();

                _initDone = true;
            }
        }
    }
}