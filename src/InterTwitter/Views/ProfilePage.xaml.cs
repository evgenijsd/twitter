using InterTwitter.ViewModels;
using System;
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

        private void OnMenuTappedHandler(object sender, EventArgs e)
        {
            menu.IsVisible = true;
        }

        private void OnAnywhereTappedHandler(object sender, EventArgs e)
        {
            menu.IsVisible = false;
        }
    }
}