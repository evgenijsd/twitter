using Xamarin.Forms;

namespace InterTwitter.Controls
{
    public class CustomCollectionView : CollectionView
    {
        private double _scrollState;

        #region -- Public properties --

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

        public static readonly BindableProperty IsNavigationBarVisibleProperty = BindableProperty.Create(
          propertyName: nameof(IsNavigationBarVisible),
          returnType: typeof(bool),
          declaringType: typeof(CustomCollectionView),
          defaultValue: true,
          defaultBindingMode: BindingMode.TwoWay);

        public bool IsNavigationBarVisible
        {
            get => (bool)GetValue(IsNavigationBarVisibleProperty);
            set => SetValue(IsNavigationBarVisibleProperty, value);
        }

        #endregion

        #region -- Overrides --

        protected override void OnScrolled(ItemsViewScrolledEventArgs e)
        {
            base.OnScrolled(e);
            if (e.VerticalOffset <= 5 && e.VerticalOffset >= 0)
            {
                IsNavigationBarVisible = true;
            }
            else
            {
                IsNavigationBarVisible = false;
            }

            IsAddButtonVisible = (e.VerticalOffset > _scrollState) || (e.VerticalOffset < _scrollState) ? true : false;

            _scrollState = 0;
        }

        #endregion

    }
}
