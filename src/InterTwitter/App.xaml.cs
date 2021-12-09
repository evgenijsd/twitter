using InterTwitter.ViewModels;
using InterTwitter.Views;
using Prism;
using Prism.Ioc;
using Prism.Unity;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

//test
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
            // Navigation
            containerRegistry.RegisterForNavigation<NavigationPage>();
            containerRegistry.RegisterForNavigation<MainPage>();
            containerRegistry.RegisterForNavigation<CreateTweetPage, CreateTweetPageViewModel>();
        }

        protected override async void OnInitialized()
        {
            InitializeComponent();

            Sharpnado.Shades.Initializer.Initialize(loggerEnable: false);

            await NavigationService.NavigateAsync($"/{nameof(CreateTweetPage)}");
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