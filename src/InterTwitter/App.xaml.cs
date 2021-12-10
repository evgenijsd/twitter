using InterTwitter.Resources.Strings;
using InterTwitter.Services.HashtagManager;
using InterTwitter.Services.MockService;
using InterTwitter.Services.SettingsManager;
using InterTwitter.Services.TweetService;
using InterTwitter.ViewModels;
using InterTwitter.Views;
using Prism.Ioc;
using Prism.Unity;
using System;
using Xamarin.CommunityToolkit.Helpers;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

//test
namespace InterTwitter
{
    public partial class App : PrismApplication
    {
        public static T Resolve<T>() => Current.Container.Resolve<T>();

        public App()
        {
        }

        #region -- Overrides --

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterInstance<ISettingsManager>(Container.Resolve<SettingsManager>());
            containerRegistry.RegisterInstance<IMockService>(Container.Resolve<MockService>());
            containerRegistry.RegisterInstance<ITweetService>(Container.Resolve<TweetService>());
            containerRegistry.RegisterInstance<IHashtagManager>(Container.Resolve<HashtagManager>());

            // Navigation
            containerRegistry.RegisterForNavigation<NavigationPage>();
            containerRegistry.RegisterForNavigation<MainPage>();
            containerRegistry.RegisterForNavigation<TweetSearchPage, TweetSearchPageViewModel>();
        }

        protected override async void OnInitialized()
        {
            InitializeComponent();

            Sharpnado.Shades.Initializer.Initialize(loggerEnable: false);
            LocalizationResourceManager.Current.Init(Strings.ResourceManager);

            //await NavigationService.NavigateAsync($"/{nameof(NavigationPage)}/{nameof(MainPage)}");
            await NavigationService.NavigateAsync($"/{nameof(TweetSearchPage)}");
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