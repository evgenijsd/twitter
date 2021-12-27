﻿using InterTwitter.Helpers;
using InterTwitter.Models;
using InterTwitter.Services;
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
        private readonly IAuthorizationService _authorizationService;
        private readonly IRegistrationService _registrationService;

        public FlyoutPageFlyoutViewModel(
            INavigationService navigationService,
            IRegistrationService registrationService,
            IAuthorizationService authorizationService)
            : base(navigationService)
        {
            _authorizationService = authorizationService;
            _registrationService = registrationService;

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

        #region -- Public Properties --

        private ObservableCollection<MenuItemViewModel> _menuItems;
        public ObservableCollection<MenuItemViewModel> MenuItems
        {
            get => _menuItems;
            set => SetProperty(ref _menuItems, value);
        }

        private string _profileIcon = "https://avatars.mds.yandex.net/i?id=9124e8d6c175c189503fa5d7883c515d-5859422-images-thumbs&n=13";
        public string ProfileIcon
        {
            get => _profileIcon;
            set => SetProperty(ref _profileIcon, value);
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

        #region -- Overrides --

        protected override void OnPropertyChanged(PropertyChangedEventArgs args)
        {
            base.OnPropertyChanged(args);
        }

        #endregion

        #region -- Private Helpers --

        private void Subscribe()
        {
            MessagingCenter.Subscribe<SearchPageViewModel, Type>(this, Constants.Messages.TAB_CHANGE, ChangeVisualState);
            MessagingCenter.Subscribe<BookmarksPageViewModel, Type>(this, Constants.Messages.TAB_CHANGE, ChangeVisualState);
            MessagingCenter.Subscribe<NotificationPageViewModel, Type>(this, Constants.Messages.TAB_CHANGE, ChangeVisualState);
            MessagingCenter.Subscribe<FlyoutPageDetailViewModel, Type>(this, Constants.Messages.TAB_CHANGE, ChangeVisualState);
            MessagingCenter.Subscribe<App, int>(this, Constants.Messages.OPEN_PROFILE_PAGE, OnOpenProfileCommandAsync);
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
            NavigationService.NavigateAsync(nameof(menuItem.TargetType));
        }

        private async Task OnLogoutCommandAsync()
        {
            _authorizationService.UserId = 0;

            await NavigationService.NavigateAsync($"/{nameof(StartPage)}");
        }

        private Task OnChangeProfileCommandAsync()
        {
            return Task.CompletedTask;
        }

        private async Task OnOpenProfileCommandAsync()
        {
           // MessagingCenter.Send(this, "OpenSidebar", false);
            await NavigationService.NavigateAsync($"{nameof(ProfilePage)}");
        }

        private async void OnOpenProfileCommandAsync(App sender, int userId)
        {
            if (userId > 0)
            {
                var aOResult = await _registrationService.GetByIdAsync(userId);

                if (aOResult.IsSuccess)
                {
                    NavigationParameters keyValuePairs = new NavigationParameters();
                    keyValuePairs.Add(Constants.Navigation.USER, aOResult.Result);

                    await NavigationService.NavigateAsync($"{nameof(ProfilePage)}", keyValuePairs);
                }
            }
        }

        #endregion
    }
}
