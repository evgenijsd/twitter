using DLToolkit.Forms.Controls;
using InterTwitter.Resources.Strings;
using InterTwitter.Services;
using InterTwitter.Services.Hashtag;
using InterTwitter.ViewModels;
using InterTwitter.ViewModels.Flyout;
using InterTwitter.Views;
using Prism;
using Prism.Ioc;
using Prism.Unity;
using System.Globalization;
using Xamarin.CommunityToolkit.Helpers;
using Xamarin.Forms;

namespace InterTwitter
{
    public partial class App : PrismApplication
    {
        public App(IPlatformInitializer initializer = null)
            : base(initializer)
        {
        }

        #region -- Overrides --

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterDialog<AlertView, AlertViewModel>();

            //Services
            containerRegistry.RegisterSingleton<IMockService, MockService>();
            containerRegistry.RegisterSingleton<ITweetService, TweetService>();
            containerRegistry.RegisterSingleton<IHashtagService, HashtagService>();
            containerRegistry.RegisterSingleton<IRegistrationService, RegistrationService>();
            containerRegistry.RegisterSingleton<IAuthorizationService, AuthorizationService>();

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
            containerRegistry.RegisterForNavigation<StartPage, StartPageViewModel>();
            containerRegistry.RegisterForNavigation<CreatePage, CreatePageViewModel>();
            containerRegistry.RegisterForNavigation<LogInPage, LogInPageViewModel>();
            containerRegistry.RegisterForNavigation<PasswordPage, PasswordPageViewModel>();
            containerRegistry.RegisterForNavigation<CreateTweetPage, CreateTweetPageViewModel>();
        }

        protected override async void OnInitialized()
        {
            LocalizationResourceManager.Current.PropertyChanged += (sender, e) => Strings.Culture = LocalizationResourceManager.Current.CurrentCulture;
            LocalizationResourceManager.Current.Init(Strings.ResourceManager);
            LocalizationResourceManager.Current.CurrentCulture = new CultureInfo("en");

            InitializeComponent();

            Sharpnado.Shades.Initializer.Initialize(loggerEnable: false);
            FlowListView.Init();
            await NavigationService.NavigateAsync($"/{nameof(StartPage)}");
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
