﻿using InterTwitter.Views;
using MapNotepad.Helpers;
using Prism.Navigation;
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
            IconPath = Prism.PrismApplicationBase.Current.Resources["ic_notifications_gray"] as ImageSource;
        }

        #region --- Public Properties ---

        public ICommand OpenFlyoutCommand => SingleExecutionCommand.FromFunc(OnOpenFlyoutCommand);

        #endregion

        #region --- Overrides ---

        public override void OnAppearing()
        {
            IconPath = Prism.PrismApplicationBase.Current.Resources["ic_notifications_blue"] as ImageSource;
        }

        public override void OnDisappearing()
        {
            IconPath = Prism.PrismApplicationBase.Current.Resources["ic_notifications_gray"] as ImageSource;
        }

        #endregion

        #region --- Private Helpers ---

        private Task OnOpenFlyoutCommand()
        {
            MessagingCenter.Send(this, Constants.Messages.OPEN_SIDEBAR, true);
            MessagingCenter.Send(this, Constants.Messages.TAB_CHANGE, typeof(NotificationsPage));
            return Task.CompletedTask;
        }

        #endregion
    }
}
