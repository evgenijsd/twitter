using DLToolkit.Forms.Controls;
using InterTwitter.Resources.Strings;
using InterTwitter.Services;
using InterTwitter.Services.Hashtag;
using InterTwitter.Services.SettingsManager;
using InterTwitter.Services.Share;
using InterTwitter.ViewModels;
using InterTwitter.ViewModels.Flyout;
using InterTwitter.Views;
using Prism;
using Prism.Ioc;
using Prism.Unity;
using System;
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

        protected override void OnAppLinkRequestReceived(Uri uri)
        {
            if (uri.Host.EndsWith(Constants.Values.HOST, StringComparison.OrdinalIgnoreCase))
            {
                if (uri.Segments != null && uri.Segments.Length == 3)
                {
                    var action = uri.Segments[1].Replace("/", string.Empty);
                    var msg = uri.Segments[2];

                    switch (action)
                    {
                        case Constants.Values.APP_USER_LINK_ID:
                            if (!string.IsNullOrEmpty(msg))
                            {
                                if (int.TryParse(msg, out int userId))
                                {
                                    MessagingCenter.Send(this, Constants.Messages.OPEN_PROFILE_PAGE, userId);
                                }
                            }

                            break;
                        default:
                            Xamarin.Forms.Device.OpenUri(uri);
                            break;
                    }
                }
            }
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            // Services
            containerRegistry.RegisterInstance<ISettingsManager>(Container.Resolve<SettingsManager>());
            containerRegistry.RegisterInstance<IMockService>(Container.Resolve<MockService>());
            containerRegistry.RegisterInstance<ITweetService>(Container.Resolve<TweetService>());
            containerRegistry.RegisterInstance<IHashtagService>(Container.Resolve<HashtagService>());
            containerRegistry.RegisterInstance<IRegistrationService>(Container.Resolve<RegistrationService>());
            containerRegistry.RegisterInstance<IAuthorizationService>(Container.Resolve<AuthorizationService>());
            containerRegistry.RegisterInstance<IShareService>(Container.Resolve<ShareService>());
            containerRegistry.RegisterDialog<AlertView, AlertViewModel>();

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
        }

        protected override async void OnInitialized()
        {
            InitializeComponent();

            FlowListView.Init();
            Sharpnado.Shades.Initializer.Initialize(loggerEnable: false);
            LocalizationResourceManager.Current.Init(Strings.ResourceManager);

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
