using InterTwitter.ViewModels.Flyout;
using System;
using System.Runtime.CompilerServices;
using Xamarin.Forms;

namespace InterTwitter.Controls
{
    public class CustomTabbedPage : TabbedPage
    {
        public CustomTabbedPage()
        {
            Subscribe();
        }

        #region -- Public properties --

        public static readonly BindableProperty SelectedTabTypeProperty = BindableProperty.Create(
            propertyName: nameof(SelectedTabType),
            returnType: typeof(Type),
            declaringType: typeof(CustomTabbedPage),
            defaultBindingMode: BindingMode.TwoWay);

        public Type SelectedTabType
        {
            get => (Type)GetValue(SelectedTabTypeProperty);
            set => SetValue(SelectedTabTypeProperty, value);
        }

        #endregion

        #region -- Overrides --

        protected override void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            base.OnPropertyChanged(propertyName);

            if (propertyName == nameof(CurrentPage))
            {
                SelectedTabType = CurrentPage?.GetType();
            }
        }
        #endregion

        #region --- Private Helpers ---
        private void Subscribe()
        {
            MessagingCenter.Subscribe<FlyoutPageFlyoutViewModel, int>(this, "TabSelected", ChangeTab);
        }

        private void ChangeTab(object obj, int id)
        {
            this.CurrentPage = this.Children[id];
        }
        #endregion
    }
}
