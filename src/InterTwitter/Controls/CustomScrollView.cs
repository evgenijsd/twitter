using Xamarin.Forms;

namespace InterTwitter.Controls
{
    public class CustomScrollView : ScrollView
    {
        #region -- Public properties --

        public static readonly BindableProperty IsBouncesProperty = BindableProperty.Create(
            propertyName: nameof(IsBounces),
            returnType: typeof(bool),
            declaringType: typeof(CustomScrollView),
            defaultBindingMode: BindingMode.TwoWay);

        public bool IsBounces
        {
            get => (bool)GetValue(IsBouncesProperty);
            set => SetValue(IsBouncesProperty, value);
        }

        #endregion
    }
}