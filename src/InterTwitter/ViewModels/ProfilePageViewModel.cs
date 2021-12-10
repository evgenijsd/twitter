using InterTwitter.Views;
using MapNotepad.Helpers;
using Prism.Navigation;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace InterTwitter.ViewModels
{
    public class ProfilePageViewModel : BaseViewModel
    {
        public ProfilePageViewModel(INavigationService navigationService)
            : base(navigationService)
        {
            MenuItems = new ObservableCollection<MenuItemViewModel>(new[]
                {
                    new MenuItemViewModel
                    {
                        Id = 0, Title = "Posts",
                        TargetType = typeof(HomePage),
                        ImageSource = "ic_home_gray",
                        TextColor = (Xamarin.Forms.Color)Prism.PrismApplicationBase.Current.Resources["appcolor_i4"],
                    },

                    new MenuItemViewModel
                    {
                        Id = 1,
                        Title = "Likes",
                        TargetType = typeof(SearchPage),
                        ImageSource = "ic_search_gray",
                        TextColor = (Xamarin.Forms.Color)Prism.PrismApplicationBase.Current.Resources["appcolor_i4"],
                    },
                });
        }
        #region --- Public Properties ---

        private ObservableCollection<MenuItemViewModel> _menuItems;
        public ObservableCollection<MenuItemViewModel> MenuItems
        {
            get => _menuItems;
            set => SetProperty(ref _menuItems, value);
        }

        public ICommand NavgationCommandAsync => SingleExecutionCommand.FromFunc(NavigationService.GoBackAsync);

        #endregion

    }
}
