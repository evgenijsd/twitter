using InterTwitter.ViewModels;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace InterTwitter.Views
{
    public partial class ProfilePage : BaseContentPage
    {
        public ProfilePage()
        {
            InitializeComponent();
        }

        private void OnItemApearingHandler(object sender, EventArgs e)
        {
            if (lists.ItemsSource is not null)
            {
                var list = lists.ItemsSource;

                var id = (lists.SelectedItem as MenuItemViewModel).Id;
                foreach (MenuItemViewModel mi in list)
                {
                    if (mi.Id == id)
                    {
                        mi.TextColor = (Xamarin.Forms.Color)Prism.PrismApplicationBase.Current.Resources["appcolor_i1"];
                    }
                    else
                    {
                        mi.TextColor = (Xamarin.Forms.Color)Prism.PrismApplicationBase.Current.Resources["appcolor_i4"];
                    }
                }
            }
        }

        private void OnMenuTappedHandler(object sender, EventArgs e)
        {
            var height = Prism.PrismApplicationBase.Current.MainPage.Height;
            menuStack.Spacing = ((19 * height) - 8308) / 207;
            currentUserMenuStack.Spacing = ((19 * height) - 8308) / 207;
        }

        private void OnAnywhereTappedHandler(object sender, EventArgs e)
        {
            currentUserHiddenMenu.IsVisible = false;
            userHiddenMenu.IsVisible = false;
        }

        private async void OnChangeProfTapHandler(object sender, EventArgs e)
        {
            changeProfFrame.BorderColor = (Xamarin.Forms.Color)Prism.PrismApplicationBase.Current.Resources["appcolor_i1"];
            changeProfLabel.TextColor = (Xamarin.Forms.Color)Prism.PrismApplicationBase.Current.Resources["appcolor_i1"];
            await System.Threading.Tasks.Task.Delay(300);
            changeProfFrame.BorderColor = (Xamarin.Forms.Color)Prism.PrismApplicationBase.Current.Resources["appcolor_i4"];
            changeProfLabel.TextColor = (Xamarin.Forms.Color)Prism.PrismApplicationBase.Current.Resources["appcolor_i4"];
        }
    }
}