using InterTwitter.Views;
using MapNotepad.Helpers;
using Prism.Navigation;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Windows.Input;
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
                        TapCommand = new Command(OnItemTapCommand),
                    },

                    new MenuItemViewModel
                    {
                        Id = 1,
                        Title = "Search",
                        TargetType = typeof(SearchPage),
                        ImageSource = "ic_search_gray",
                        TapCommand = new Command(OnItemTapCommand),
                    },
                    new MenuItemViewModel
                    {
                        Id = 2,
                        Title = "Notification",
                        TargetType = typeof(NotificationsPage),
                        ImageSource = "ic_notifications_gray",
                        TapCommand = new Command(OnItemTapCommand),
                    },
                    new MenuItemViewModel
                    {
                        Id = 3,
                        Title = "Bookmarks",
                        TargetType = typeof(BookmarksPage),
                        ImageSource = "ic_bookmarks_gray",
                        TapCommand = new Command(OnItemTapCommand),
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

        public ICommand LogoutTapCommand => SingleExecutionCommand.FromFunc(OnLogoutTapCommand);
        public ICommand ChangeProfileTapCommand => SingleExecutionCommand.FromFunc(OnChangeProfileTapCommand);

        #endregion

        #region --- Overrides ---

        protected override void OnPropertyChanged(PropertyChangedEventArgs args)
        {
            base.OnPropertyChanged(args);
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
                string[] selectedItemImageSource = new string[8];

                selectedItemImageSource[0] = "ic_home_blue";
                selectedItemImageSource[1] = "ic_search_blue";
                selectedItemImageSource[2] = "ic_notifications_blue";
                selectedItemImageSource[3] = "ic_bookmarks_blue";

                selectedItemImageSource[4] = "ic_home_gray";
                selectedItemImageSource[5] = "ic_search_gray";
                selectedItemImageSource[6] = "ic_notifications_gray";
                selectedItemImageSource[7] = "ic_bookmarks_gray";

                foreach (var item in MenuItems)
                {
                    if (item.TargetType == selectedTabType)
                    {
                        item.TextColor = (Color)Prism.PrismApplicationBase.Current.Resources["appcolor_i1"];
                        item.ImageSource = selectedItemImageSource[item.Id];
                    }
                    else
                    {
                        item.TextColor = (Color)Prism.PrismApplicationBase.Current.Resources["appcolor_i3"];
                        item.ImageSource = selectedItemImageSource[item.Id + 4];
                    }
                }
            }
        }

        private void OnItemTapCommand(object param)
        {
            var menuItem = param as MenuItemViewModel;
            MessagingCenter.Send(this, "OpenSidebar", false);
            MessagingCenter.Send(this, "TabSelected", menuItem.Id);
            NavigationService.NavigateAsync(nameof(menuItem.TargetType));
        }

        private Task OnLogoutTapCommand()
        {
            return Task.FromResult(false);
        }

        private async Task OnChangeProfileTapCommand()
        {
            MessagingCenter.Send(this, "OpenSidebar", false);
            await NavigationService.NavigateAsync($"{nameof(ProfilePage)}");
        }

        #endregion
    }
}
