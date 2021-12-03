using Prism.Navigation;
using System;
using System.ComponentModel;
using Xamarin.Forms;

namespace InterTwitter.ViewModels.Flyout
{
    public class FlyoutPageDetailViewModel : BaseViewModel
    {
        public FlyoutPageDetailViewModel(INavigationService navigationService)
                                                     : base(navigationService)
        {
            Subscribe();
        }

        #region -- Public properties --

        private Type _selectedTabType;
        public Type SelectedTabType
        {
            get => _selectedTabType;
            set => SetProperty(ref _selectedTabType, value);
        }

        #endregion

        #region -- Overrides --

        protected override void OnPropertyChanged(PropertyChangedEventArgs args)
        {
            base.OnPropertyChanged(args);

            if (args.PropertyName == nameof(SelectedTabType))
            {
                MessagingCenter.Send(this, "TabChange", SelectedTabType);
            }
        }

        #endregion

        #region --- Private Helpers ---
        private void Subscribe()
        {
            MessagingCenter.Subscribe<FlyoutPageFlyoutViewModel, Type>(this, "ItemSelected", OnItemSelected);
        }

        private void OnItemSelected(object sender, Type selectedItemType)
        {
            NavigationService.NavigateAsync($"FlyoutPageDetail?selectedTab={selectedItemType.Name}");
        }
        #endregion
    }
}
