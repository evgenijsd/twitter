using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using Xamarin.Forms;

namespace InterTwitter.Controls
{
    public class CustomTabbedPage : TabbedPage
    {
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
    }
}
