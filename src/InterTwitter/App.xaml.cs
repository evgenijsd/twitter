using DLToolkit.Forms.Controls;
using InterTwitter.Resources.Strings;
using InterTwitter.Services.Hashtag;
using InterTwitter.Services.Share;
using InterTwitter.Services.Settings;
using InterTwitter.Droid.Services.PermissionsService;
using InterTwitter.Services;
using InterTwitter.Services.PermissionsService;
using InterTwitter.Services.UserService;
using InterTwitter.ViewModels;
using InterTwitter.Views;
using Prism;
using Prism.Ioc;
using Prism.Plugin.Popups;
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
            containerRegistry.RegisterPopupNavigationService();
            containerRegistry.RegisterPopupDialogService();
            containerRegistry.RegisterDialog<AlertView, AlertViewModel>();

            //Services
            containerRegistry.RegisterSingleton<ISettingsManager, SettingsManager>();
            containerRegistry.RegisterSingleton<IMockService, MockService>();
            containerRegistry.RegisterSingleton<ITweetService, TweetService>();
            containerRegistry.RegisterSingleton<IHashtagService, HashtagService>();
            containerRegistry.RegisterSingleton<IRegistrationService, RegistrationService>();
            containerRegistry.RegisterSingleton<IAuthorizationService, AuthorizationService>();
            containerRegistry.RegisterSingleton<IUserService, UserService>();
            containerRegistry.RegisterSingleton<IPermissionService, PermissionService>();
            containerRegistry.RegisterSingleton<IBookmarkService, BookmarkService>();
            containerRegistry.RegisterSingleton<ILikeService, LikeService>();
            containerRegistry.RegisterSingleton<INotificationService, NotificationService>();

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
            containerRegistry.RegisterForNavigation<CreatePage, CreatePageViewModel>();
            containerRegistry.RegisterForNavigation<LogInPage, LogInPageViewModel>();
            containerRegistry.RegisterForNavigation<PasswordPage, PasswordPageViewModel>();
            containerRegistry.RegisterForNavigation<EditProfilePage, EditProfilePageViewModel>();
            containerRegistry.RegisterForNavigation<BlacklistPage, BlacklistPageViewModel>();
        }

        protected override async void OnInitialized()
        {
            InitializeComponent();
            App.Current.UserAppTheme = OSAppTheme.Light;

            FlowListView.Init();
            Sharpnado.Shades.Initializer.Initialize(loggerEnable: false);
            LocalizationResourceManager.Current.Init(Strings.ResourceManager);

            await NavigationService.NavigateAsync($"/{nameof(LogInPage)}");
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
            this.PopupPluginOnSleep();
        }

        protected override void OnResume()
        {
            this.PopupPluginOnResume();
        }

        #endregion
    }
}
