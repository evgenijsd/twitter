using Xamarin.Forms;

namespace InterTwitter.Controls
{
    public class SearchEntry : Entry
    {
        #region -- Public properties --

        public static readonly BindableProperty HighlightColorProperty = BindableProperty.Create(
            propertyName: nameof(HighlightColor),
            returnType: typeof(Color),
            declaringType: typeof(SearchBar),
            defaultBindingMode: BindingMode.TwoWay);

        public Color HighlightColor
        {
            get => (Color)GetValue(HighlightColorProperty);
            set => SetValue(HighlightColorProperty, value);
        }

        #endregion
    }
}
