using InterTwitter.Views;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

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
                        Id = 0, Title = "Home",
                        TargetType = typeof(HomePage),
                        ImageSource = "ic_home_gray",
                    },

                    new MenuItemViewModel
                    {
                        Id = 1,
                        Title = "Search",
                        TargetType = typeof(SearchPage),
                        ImageSource = "ic_search_gray",
                    },
                });
        }

        private ObservableCollection<MenuItemViewModel> _menuItems;
        public ObservableCollection<MenuItemViewModel> MenuItems
        {
            get => _menuItems;
            set => SetProperty(ref _menuItems, value);
        }
    }
}
