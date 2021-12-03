using InterTwitter.Services.Autorization;
using InterTwitter.Services.Registration;
using InterTwitter.ViewModels;
using InterTwitter.Views;
using Prism;
using Prism.Ioc;
using Prism.Unity;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

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
            containerRegistry.RegisterInstance<IRegistrationService>(Container.Resolve<RegistrationService>());
            containerRegistry.RegisterInstance<IAutorizationService>(Container.Resolve<AutorizationService>());

            // Navigation
            containerRegistry.RegisterForNavigation<NavigationPage>();
            containerRegistry.RegisterForNavigation<StartPage, StartPageViewModel>();
            containerRegistry.RegisterForNavigation<CreatePage, CreatePageViewModel>();
            containerRegistry.RegisterForNavigation<LogInPage, LogInPageViewModel>();
            containerRegistry.RegisterForNavigation<PasswordPage, PasswordPageViewModel>();
        }

        protected override async void OnInitialized()
        {
            InitializeComponent();
<<<<<<< HEAD

            await NavigationService.NavigateAsync($"/{nameof(StartPage)}");
=======
            await NavigationService.NavigateAsync($"/{nameof(NavigationPage)}/{nameof(MainPage)}");
>>>>>>> 1223893a6974b67e7bc6b40385cdd6ec885eb7b8
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
