using InterTwitter.Views;
using MapNotepad.Helpers;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace InterTwitter.ViewModels
{
    public class NotificationPageViewModel : BaseTabViewModel
    {
        public NotificationPageViewModel(INavigationService navigationService)
                                                  : base(navigationService)
        {
            IconPath = "ic_notifications_gray.png";
        }
        #region --- Public Properties ---
        public ICommand GotoFlyoutCommand => SingleExecutionCommand.FromFunc(OnGotoFlyoutCommandAsync);

        #endregion

        #region --- Overrides ---
        public override void OnAppearing()
        {
            IconPath = "ic_notifications_blue.png";
        }

        public override void OnDisappearing()
        {
            IconPath = "ic_notifications_gray.png";
        }
        #endregion

        #region --- Private Helpers ---
        private async Task OnGotoFlyoutCommandAsync()
        {
            await NavigationService.NavigateAsync($"/{nameof(FlyOutPage)}");
            MessagingCenter.Send(this, "OpenSidebar", true);
            MessagingCenter.Send(this, "TabChange", typeof(NotificationsPage));
        }
        #endregion
    }
}
