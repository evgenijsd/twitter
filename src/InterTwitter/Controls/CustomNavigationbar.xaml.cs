using System.Windows.Input;
using Xamarin.Forms;

namespace InterTwitter.Controls
{
    public partial class CustomNavigationbar : ContentView
    {
        public CustomNavigationbar()
        {
            InitializeComponent();
        }

        #region -- Public properties --

        public static readonly BindableProperty RightButtonImageSourceProperty = BindableProperty.Create(
            propertyName: nameof(RightButtonImageSource),
            returnType: typeof(string),
            declaringType: typeof(CustomNavigationbar),
            defaultBindingMode: BindingMode.TwoWay);

        public string RightButtonImageSource
        {
            get => (string)GetValue(RightButtonImageSourceProperty);
            set => SetValue(RightButtonImageSourceProperty, value);
        }

        public static readonly BindableProperty RightButtonCommandProperty = BindableProperty.Create(
            propertyName: nameof(RightButtonCommand),
            returnType: typeof(ICommand),
            declaringType: typeof(CustomNavigationbar),
            defaultBindingMode: BindingMode.TwoWay);

        public ICommand RightButtonCommand
        {
            get => (ICommand)GetValue(RightButtonCommandProperty);
            set => SetValue(RightButtonCommandProperty, value);
        }

        public static readonly BindableProperty LeftButtonImageSourceProperty = BindableProperty.Create(
            propertyName: nameof(LeftButtonImageSource),
            returnType: typeof(string),
            declaringType: typeof(CustomNavigationbar),
            defaultBindingMode: BindingMode.TwoWay);

        public string LeftButtonImageSource
        {
            get => (string)GetValue(LeftButtonImageSourceProperty);
            set => SetValue(LeftButtonImageSourceProperty, value);
        }

        public static readonly BindableProperty LeftButtonCommandProperty = BindableProperty.Create(
            propertyName: nameof(LeftButtonCommand),
            returnType: typeof(ICommand),
            declaringType: typeof(CustomNavigationbar),
            defaultBindingMode: BindingMode.TwoWay);

        public ICommand LeftButtonCommand
        {
            get => (ICommand)GetValue(LeftButtonCommandProperty);
            set => SetValue(LeftButtonCommandProperty, value);
        }

        public static readonly BindableProperty TitleProperty = BindableProperty.Create(
            propertyName: nameof(Title),
            returnType: typeof(string),
            declaringType: typeof(CustomNavigationbar),
            defaultBindingMode: BindingMode.TwoWay);

        public string Title
        {
            get => (string)GetValue(TitleProperty);
            set => SetValue(TitleProperty, value);
        }

        public static readonly BindableProperty TitleColorProperty = BindableProperty.Create(
            propertyName: nameof(Title),
            returnType: typeof(Color),
            declaringType: typeof(CustomNavigationbar),
            defaultBindingMode: BindingMode.TwoWay);

        public Color TitleColor
        {
            get => (Color)GetValue(TitleColorProperty);
            set => SetValue(TitleColorProperty, value);
        }

        #endregion

    }
}