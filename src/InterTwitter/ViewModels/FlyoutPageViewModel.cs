using InterTwitter.ViewModels.Flyout;
using Prism.Navigation;
using Xamarin.Forms;

namespace InterTwitter.ViewModels
{
    public class FlyOutPageViewModel : BaseViewModel
    {
        public FlyOutPageViewModel(INavigationService navigationService)
                                               : base(navigationService)
        {
            Subscribe();
        }

        #region --- Public Properties ---

        private bool _isPresented;
        public bool IsPresented
        {
            get => _isPresented;
            set => SetProperty(ref _isPresented, value);
        }

        #endregion

        #region --- Private Helpers ---

        private void Subscribe()
        {
            MessagingCenter.Subscribe<HomePageViewModel, bool>(this, "OpenSidebar", (sender, arg) => IsPresented = arg);
            MessagingCenter.Subscribe<SearchPageViewModel, bool>(this, "OpenSidebar", (sender, arg) => IsPresented = arg);
            MessagingCenter.Subscribe<BookmarksPageViewModel, bool>(this, "OpenSidebar", (sender, arg) => IsPresented = arg);
            MessagingCenter.Subscribe<NotificationPageViewModel, bool>(this, "OpenSidebar", (sender, arg) => IsPresented = arg);
            MessagingCenter.Subscribe<FlyoutPageFlyoutViewModel, bool>(this, "OpenSidebar", (sender, arg) => IsPresented = arg);
        }

        #endregion
    }
}
