using Prism.Navigation;
using System;
using System.ComponentModel;
using Xamarin.Forms;

namespace InterTwitter.ViewModels
{
    public class FlyoutPageDetailViewModel : BaseViewModel
    {
        public FlyoutPageDetailViewModel(INavigationService navigationService)
            : base(navigationService)
        {
        }

        #region -- Public properties --

        private Type _selectedTabType;
        public Type SelectedTabType
        {
            get => _selectedTabType;
            set => SetProperty(ref _selectedTabType, value);
        }

        protected override void OnPropertyChanged(PropertyChangedEventArgs args)
        {
            base.OnPropertyChanged(args);

            if (args.PropertyName == nameof(SelectedTabType))
            {
                MessagingCenter.Send(this, Constants.Messages.TAB_CHANGE, SelectedTabType);
            }
        }

        #endregion

    }
}
