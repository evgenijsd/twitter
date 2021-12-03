using InterTwitter.Enums;
using System.Windows.Input;
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

        public static BindableProperty AvatarIconSourceProperty = BindableProperty.Create(
            propertyName: nameof(AvatarIconSource),
            returnType: typeof(ImageSource),
            declaringType: typeof(SearchBar),
            defaultBindingMode: BindingMode.TwoWay);

        public ImageSource AvatarIconSource
        {
            get => (ImageSource)GetValue(AvatarIconSourceProperty);
            set => SetValue(AvatarIconSourceProperty, value);
        }

        public static BindableProperty SearchStateProperty = BindableProperty.Create(
            propertyName: nameof(SearchState),
            returnType: typeof(ESearchState),
            declaringType: typeof(SearchBar),
            defaultBindingMode: BindingMode.TwoWay);

        public ESearchState SearchState
        {
            get => (ESearchState)GetValue(SearchStateProperty);
            set => SetValue(SearchStateProperty, value);
        }

        public static BindableProperty AvatarTapCommandProperty = BindableProperty.Create(
            propertyName: nameof(AvatarTapCommand),
            returnType: typeof(ICommand),
            declaringType: typeof(SearchBar),
            defaultBindingMode: BindingMode.TwoWay);

        public ICommand AvatarTapCommand
        {
            get => (ICommand)GetValue(AvatarTapCommandProperty);
            set => SetValue(AvatarTapCommandProperty, value);
        }

        public static BindableProperty BackIconTapCommandProperty = BindableProperty.Create(
            propertyName: nameof(BackIconTapCommand),
            returnType: typeof(ICommand),
            declaringType: typeof(SearchBar),
            defaultBindingMode: BindingMode.TwoWay);

        public ICommand BackIconTapCommand
        {
            get => (ICommand)GetValue(BackIconTapCommandProperty);
            set => SetValue(BackIconTapCommandProperty, value);
        }

        public static BindableProperty ReturnCommandProperty = BindableProperty.Create(
            propertyName: nameof(ReturnCommand),
            returnType: typeof(ICommand),
            declaringType: typeof(SearchBar),
            defaultBindingMode: BindingMode.TwoWay);

        public ICommand ReturnCommand
        {
            get => (ICommand)GetValue(ReturnCommandProperty);
            set => SetValue(ReturnCommandProperty, value);
        }

        #endregion
    }
}