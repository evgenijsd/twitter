using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Text;
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
            MessagingCenter.Subscribe<HomePageViewModel, bool>(this, "OpenSidebar", OpenSidebar);
            MessagingCenter.Subscribe<SearchPageViewModel, bool>(this, "OpenSidebar", OpenSidebar);
            MessagingCenter.Subscribe<BookmarksPageViewModel, bool>(this, "OpenSidebar", OpenSidebar);
            MessagingCenter.Subscribe<NotificationPageViewModel, bool>(this, "OpenSidebar", OpenSidebar);
        }

        private void OpenSidebar(object sender, bool arg)
        {
            IsPresented = arg;
        }
        #endregion
    }
}
