using InterTwitter.Views;
using Prism.Navigation;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using Xamarin.Forms;

namespace InterTwitter.ViewModels.Flyout
{
    public class FlyoutPageFlyoutViewModel : BaseViewModel
    {
        public FlyoutPageFlyoutViewModel(INavigationService navigationService)
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
                    new MenuItemViewModel
                    {
                        Id = 2,
                        Title = "Notification",
                        TargetType = typeof(NotificationsPage),
                        ImageSource = "ic_notifications_gray",
                    },
                    new MenuItemViewModel
                    {
                        Id = 3,
                        Title = "Bookmarks",
                        TargetType = typeof(BookmarksPage),
                        ImageSource = "ic_bookmarks_gray",
                    },
                });
            Subscribe();
        }

        #region --- Public Properties ---
        private ObservableCollection<MenuItemViewModel> _menuItems;
        public ObservableCollection<MenuItemViewModel> MenuItems
        {
            get => _menuItems;
            set => SetProperty(ref _menuItems, value);
        }

        private MenuItemViewModel _selectedItem;
        public MenuItemViewModel SelectedItem
        {
            get => _selectedItem;
            set => SetProperty(ref _selectedItem, value);
        }

        private string _profileName;
        public string ProfileName
        {
            get => _profileName;
            set => SetProperty(ref _profileName, value);
        }

        private string _profileEmail;
        public string ProfileEmail
        {
            get => _profileEmail;
            set => SetProperty(ref _profileEmail, value);
        }

        #endregion

        #region --- Overrides ---
        protected override void OnPropertyChanged(PropertyChangedEventArgs args)
        {
            base.OnPropertyChanged(args);
            if (args.PropertyName == nameof(SelectedItem) && SelectedItem != null)
            {
                var type = SelectedItem.TargetType;
                SelectedItem = null;
                MessagingCenter.Send(this, "ItemSelected", type);
            }
        }
        #endregion

        #region --- Private Helpers ---
        private void Subscribe()
        {
            MessagingCenter.Subscribe<SearchPageViewModel, Type>(this, "TabChange", ChangeVisualState);
            MessagingCenter.Subscribe<BookmarksPageViewModel, Type>(this, "TabChange", ChangeVisualState);
            MessagingCenter.Subscribe<NotificationPageViewModel, Type>(this, "TabChange", ChangeVisualState);
            MessagingCenter.Subscribe<FlyoutPageDetailViewModel, Type>(this, "TabChange", ChangeVisualState);
        }

        private void ChangeVisualState(object sender, Type selectedTabType)
        {
            if (MenuItems != null)
            {
                foreach (var item in MenuItems)
                {
                    if (item.TargetType == selectedTabType)
                    {
                        item.TextColor = (Color)App.Current.Resources["appcolor_i1"];
                        switch (item.TargetType.Name)
                        {
                            case nameof(HomePage):
                                item.ImageSource = "ic_home_blue";
                                break;
                            case nameof(SearchPage):
                                item.ImageSource = "ic_search_blue";
                                break;
                            case nameof(NotificationsPage):
                                item.ImageSource = "ic_notifications_blue";
                                break;
                            case nameof(BookmarksPage):
                                item.ImageSource = "ic_bookmarks_blue";
                                break;
                        }
                    }
                    else
                    {
                        item.TextColor = (Color)App.Current.Resources["appcolor_i3"];
                        switch (item.TargetType.Name)
                        {
                            case nameof(HomePage):
                                item.ImageSource = "ic_home_gray";
                                break;
                            case nameof(SearchPage):
                                item.ImageSource = "ic_search_gray";
                                break;
                            case nameof(NotificationsPage):
                                item.ImageSource = "ic_notifications_gray";
                                break;
                            case nameof(BookmarksPage):
                                item.ImageSource = "ic_bookmarks_gray";
                                break;
                        }
                    }
                }
            }
        }
        #endregion
    }
}
