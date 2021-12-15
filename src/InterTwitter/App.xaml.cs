using DLToolkit.Forms.Controls;
using InterTwitter.Services;
using InterTwitter.ViewModels;
using InterTwitter.ViewModels.Flyout;
using InterTwitter.Views;
using Prism;
using Prism.Ioc;
using Prism.Unity;
using Xamarin.Forms;

namespace InterTwitter
{
    public partial class App : PrismApplication
    {
        public App()
        {
        }

        public App(IPlatformInitializer platformInitializer)
            : base(platformInitializer)
        {
        }

        #region -- Overrides --

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            //Services
            containerRegistry.RegisterSingleton<IMockService, MockService>();
            containerRegistry.RegisterSingleton<ITweetService, TweetService>();

            // Navigation
            containerRegistry.RegisterForNavigation<NavigationPage>();
            containerRegistry.RegisterForNavigation<FlyOutPage, FlyOutPageViewModel>();
            containerRegistry.RegisterForNavigation<FlyoutPageDetail, FlyoutPageDetailViewModel>();
            containerRegistry.RegisterForNavigation<FlyoutPageFlyout, FlyoutPageFlyoutViewModel>();
            containerRegistry.RegisterForNavigation<HomePage, HomePageViewModel>();
            containerRegistry.RegisterForNavigation<SearchPage, SearchPageViewModel>();
            containerRegistry.RegisterForNavigation<BookmarksPage, BookmarksPageViewModel>();
            containerRegistry.RegisterForNavigation<NotificationsPage, NotificationPageViewModel>();
            containerRegistry.RegisterForNavigation<ProfilePage, ProfilePageViewModel>();
            containerRegistry.RegisterForNavigation<MainPage>();
            containerRegistry.RegisterForNavigation<CreateTweetPage, CreateTweetPageViewModel>();
        }

        protected override async void OnInitialized()
        {
            InitializeComponent();
            Sharpnado.Shades.Initializer.Initialize(loggerEnable: false);
            FlowListView.Init();
            await NavigationService.NavigateAsync(nameof(FlyOutPage));
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }

        #endregion
    }
}