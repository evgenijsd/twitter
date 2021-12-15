using InterTwitter.Helpers;
using InterTwitter.Views;
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
                    ImageSource = Prism.PrismApplicationBase.Current.Resources["ic_home_gray"] as ImageSource,
                    TapCommand = new Command(OnItemTapCommand),
                },

                new MenuItemViewModel
                {
                    Id = 1,
                    Title = "Search",
                    TargetType = typeof(SearchPage),
                    ImageSource = Prism.PrismApplicationBase.Current.Resources["ic_search_gray"] as ImageSource,
                    TapCommand = new Command(OnItemTapCommand),
                },
                new MenuItemViewModel
                {
                    Id = 2,
                    Title = "Notification",
                    TargetType = typeof(NotificationsPage),
                    ImageSource = Prism.PrismApplicationBase.Current.Resources["ic_notifications_gray"] as ImageSource,
                    TapCommand = new Command(OnItemTapCommand),
                },
                new MenuItemViewModel
                {
                    Id = 3,
                    Title = "Bookmarks",
                    TargetType = typeof(BookmarksPage),
                    ImageSource = Prism.PrismApplicationBase.Current.Resources["ic_bookmarks_gray"] as ImageSource,
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

        private string _profileName = "Gianna Press";
        public string ProfileName
        {
            get => _profileName;
            set => SetProperty(ref _profileName, value);
        }

        private string _profileEmail = "gianap@gmail.com";
        public string ProfileEmail
        {
            get => _profileEmail;
            set => SetProperty(ref _profileEmail, value);
        }

        public ICommand LogoutCommandAsync => SingleExecutionCommand.FromFunc(OnLogoutCommandAsync);
        public ICommand ChangeProfileCommandAsync => SingleExecutionCommand.FromFunc(OnChangeProfileCommandAsync);
        public ICommand OpenProfileCommandAsync => SingleExecutionCommand.FromFunc(OnOpenProfileCommandAsync);

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
            MessagingCenter.Subscribe<SearchPageViewModel, Type>(this, Constants.Messages.TAB_CHANGE, ChangeVisualState);
            MessagingCenter.Subscribe<BookmarksPageViewModel, Type>(this, Constants.Messages.TAB_CHANGE, ChangeVisualState);
            MessagingCenter.Subscribe<NotificationPageViewModel, Type>(this, Constants.Messages.TAB_CHANGE, ChangeVisualState);
            MessagingCenter.Subscribe<FlyoutPageDetailViewModel, Type>(this, Constants.Messages.TAB_CHANGE, ChangeVisualState);
        }

        private void ChangeVisualState(object sender, Type selectedTabType)
        {
            if (MenuItems != null)
            {
                ImageSource[] selectedItemImageSource = new ImageSource[8];

                selectedItemImageSource[0] = Prism.PrismApplicationBase.Current.Resources["ic_home_blue"] as ImageSource;
                selectedItemImageSource[1] = Prism.PrismApplicationBase.Current.Resources["ic_search_blue"] as ImageSource;
                selectedItemImageSource[2] = Prism.PrismApplicationBase.Current.Resources["ic_notifications_blue"] as ImageSource;
                selectedItemImageSource[3] = Prism.PrismApplicationBase.Current.Resources["ic_bookmarks_blue"] as ImageSource;

                selectedItemImageSource[4] = Prism.PrismApplicationBase.Current.Resources["ic_home_gray"] as ImageSource;
                selectedItemImageSource[5] = Prism.PrismApplicationBase.Current.Resources["ic_search_gray"] as ImageSource;
                selectedItemImageSource[6] = Prism.PrismApplicationBase.Current.Resources["ic_notifications_gray"] as ImageSource;
                selectedItemImageSource[7] = Prism.PrismApplicationBase.Current.Resources["ic_bookmarks_gray"] as ImageSource;

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
            MessagingCenter.Send(this, Constants.Messages.OPEN_SIDEBAR, false);
            MessagingCenter.Send(this, Constants.Messages.TAB_SELECTED, menuItem.Id);
            _navigationService.NavigateAsync(nameof(menuItem.TargetType));
        }

        private Task OnLogoutCommandAsync()
        {
            return Task.CompletedTask;
        }

        private Task OnChangeProfileCommandAsync()
        {
            return Task.CompletedTask;
        }

        private async Task OnOpenProfileCommandAsync()
        {
            await _navigationService.NavigateAsync($"{nameof(ProfilePage)}");
        }

        #endregion

    }
}
