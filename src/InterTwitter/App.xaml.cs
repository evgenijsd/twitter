using DLToolkit.Forms.Controls;
using InterTwitter.Resources.Strings;
using InterTwitter.Services;
using InterTwitter.Services.Hashtag;
using InterTwitter.Services.Share;
using InterTwitter.Services.Video;
using InterTwitter.ViewModels;
using InterTwitter.Views;
using Prism;
using Prism.Ioc;
using Prism.Navigation;
using Prism.Plugin.Popups;
using Prism.Unity;
using System;
using System.Linq;
using Xamarin.CommunityToolkit.Helpers;
using Xamarin.Forms;

namespace InterTwitter
{
    public partial class App : PrismApplication
    {
        public static T Resolve<T>() => Current.Container.Resolve<T>();

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

                            int userId = 0;

                            if (!string.IsNullOrEmpty(msg))
                            {
                                if (int.TryParse(msg, out userId))
                                {
                                    var navigation = MainPage.Navigation;
                                    var lastPage = navigation.NavigationStack.LastOrDefault();

                                    if (lastPage is ProfilePage profilePage)
                                    {
                                        navigation.RemovePage(profilePage);
                                    }
                                    else if (navigation.ModalStack.LastOrDefault() is ProfilePage modalProfilePage)
                                    {
                                        navigation.PopModalAsync();
                                    }
                                }
                            }

                            MessagingCenter.Send(this, Constants.Messages.OPEN_PROFILE_PAGE, userId);

                            break;
                    }
                }
            }
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            // Services
            containerRegistry.RegisterInstance<IShareService>(Container.Resolve<ShareService>());
            containerRegistry.RegisterPopupNavigationService();
            containerRegistry.RegisterPopupDialogService();
            containerRegistry.RegisterDialog<AlertView, AlertViewModel>();
            containerRegistry.RegisterSingleton<IMockService, MockService>();
            containerRegistry.RegisterSingleton<ISettingsManager, SettingsManager>();
            containerRegistry.RegisterSingleton<IAuthorizationService, AuthorizationService>();
            containerRegistry.RegisterSingleton<ITweetService, TweetService>();
            containerRegistry.RegisterSingleton<IHashtagService, HashtagService>();
            containerRegistry.RegisterSingleton<IRegistrationService, RegistrationService>();
            containerRegistry.RegisterSingleton<IUserService, UserService>();
            containerRegistry.RegisterSingleton<IBookmarkService, BookmarkService>();
            containerRegistry.RegisterSingleton<ILikeService, LikeService>();
            containerRegistry.RegisterSingleton<INotificationService, NotificationService>();
            containerRegistry.RegisterSingleton<IVideoService, VideoService>();

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
            containerRegistry.RegisterForNavigation<CreateTweetPage, CreateTweetPageViewModel>();
        }

        protected override async void OnInitialized()
        {
            InitializeComponent();
            App.Current.UserAppTheme = OSAppTheme.Light;

            FlowListView.Init();
            Sharpnado.Shades.Initializer.Initialize(loggerEnable: false);
            LocalizationResourceManager.Current.Init(Strings.ResourceManager);

            var settingsManager = Resolve<ISettingsManager>();
            var registrationService = Resolve<IRegistrationService>();

            var getByIdResult = await registrationService.GetByIdAsync(settingsManager.UserId);

            if (getByIdResult.IsSuccess)
            {
                var user = getByIdResult.Result;

                var parameters = new NavigationParameters();

                parameters.Add(Constants.Navigation.USER, user);

                await NavigationService.NavigateAsync($"/{nameof(NavigationPage)}/{nameof(FlyOutPage)}", parameters);
            }
            else
            {
                await NavigationService.NavigateAsync($"/{nameof(NavigationPage)}/{nameof(LogInPage)}");
            }
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
