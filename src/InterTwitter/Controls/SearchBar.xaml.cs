using Xamarin.Forms;

namespace InterTwitter.Controls
{
    public partial class SearchBar : ContentView
    {
        public SearchBar()
        {
            InitializeComponent();
        }

        #region --- Public properties ---

        public static BindableProperty TextProperty = BindableProperty.Create(
            propertyName: nameof(Text),
            returnType: typeof(string),
            declaringType: typeof(SearchBar),
            defaultBindingMode: BindingMode.TwoWay);

        public string Text
        {
            get => (string)GetValue(TextProperty);
            set => SetValue(TextProperty, value);
        }

        public static BindableProperty TextColorProperty = BindableProperty.Create(
            propertyName: nameof(TextColor),
            returnType: typeof(Color),
            declaringType: typeof(SearchBar),
            defaultBindingMode: BindingMode.TwoWay);

        public Color TextColor
        {
            get => (Color)GetValue(TextColorProperty);
            set => SetValue(TextColorProperty, value);
        }

        public static BindableProperty PlaceholderProperty = BindableProperty.Create(
            propertyName: nameof(Placeholder),
            returnType: typeof(string),
            declaringType: typeof(SearchBar),
            defaultBindingMode: BindingMode.TwoWay);

        public string Placeholder
        {
            get => (string)GetValue(PlaceholderProperty);
            set => SetValue(PlaceholderProperty, value);
        }

        public static BindableProperty PlaceholderColorProperty = BindableProperty.Create(
            propertyName: nameof(PlaceholderColor),
            returnType: typeof(Color),
            declaringType: typeof(SearchBar),
            defaultBindingMode: BindingMode.TwoWay);

        public Color PlaceholderColor
        {
            get => (Color)GetValue(PlaceholderColorProperty);
            set => SetValue(PlaceholderColorProperty, value);
        }

        #endregion
    }
}