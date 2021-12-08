﻿using InterTwitter.Views;
using MapNotepad.Helpers;
using Prism.Navigation;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace InterTwitter.ViewModels
{
    public class SearchPageViewModel : BaseTabViewModel
    {
        public SearchPageViewModel(INavigationService navigationService)
                                                  : base(navigationService)
        {
            IconPath = Prism.PrismApplicationBase.Current.Resources["ic_search_gray"] as ImageSource;
        }

        #region --- Public Properties ---

        public ICommand OpenFlyoutCommand => SingleExecutionCommand.FromFunc(OnOpenFlyoutCommand);

        #endregion

        #region --- Overrides ---

        public override void OnAppearing()
        {
            IconPath = Prism.PrismApplicationBase.Current.Resources["ic_search_blue"] as ImageSource;
        }

        public override void OnDisappearing()
        {
            IconPath = Prism.PrismApplicationBase.Current.Resources["ic_search_gray"] as ImageSource;
        }

        #endregion

        #region --- Private Helpers ---

        private Task OnOpenFlyoutCommand()
        {
            MessagingCenter.Send(this, Constants.Messages.OPEN_SIDEBAR, true);
            MessagingCenter.Send(this, Constants.Messages.TAB_CHANGE, typeof(SearchPage));
            return Task.CompletedTask;
        }

        #endregion
    }
}
