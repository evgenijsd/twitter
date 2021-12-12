using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace InterTwitter.Controls
{
    public class CustomCollectionView : CollectionView
    {
        #region -- Public properties --
        private double _scrollState;

        public static readonly BindableProperty IsAddButtonVisibleProperty = BindableProperty.Create(
          propertyName: nameof(IsAddButtonVisible),
          returnType: typeof(bool),
          declaringType: typeof(CustomCollectionView),
          defaultBindingMode: BindingMode.TwoWay);

        public bool IsAddButtonVisible
        {
            get => (bool)GetValue(IsAddButtonVisibleProperty);
            set => SetValue(IsAddButtonVisibleProperty, value);
        }

        #endregion

        #region -- Overrides --
        protected override void OnScrolled(ItemsViewScrolledEventArgs e)
        {
            base.OnScrolled(e);
            IsAddButtonVisible = (e.VerticalOffset > _scrollState) || (e.VerticalOffset < _scrollState) ? true : false;
            _scrollState = default;
        }

        #endregion
    }
}
