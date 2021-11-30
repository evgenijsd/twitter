using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace InterTwitter.Controls
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CustomToolbar : ContentView
    {
        public CustomToolbar()
        {
            InitializeComponent();
        }

        #region -- Public properties --

        #endregion
        public static readonly BindableProperty RightUserImageButtonProperty = BindableProperty.Create(
            propertyName: nameof(RightUserImageButton),
            returnType: typeof(string),
            declaringType: typeof(CustomToolbar),
            defaultBindingMode: BindingMode.TwoWay);
        public string RightUserImageButton
        {
            get => (string)GetValue(RightUserImageButtonProperty);
            set => SetValue(RightUserImageButtonProperty, value);
        }

        public static readonly BindableProperty RightUserButtonCommandProperty = BindableProperty.Create(
          propertyName: nameof(RightUserButtonCommand),
          returnType: typeof(ICommand),
          declaringType: typeof(CustomToolbar),
          defaultBindingMode: BindingMode.TwoWay);
        public ICommand RightUserButtonCommand
        {
            get => (ICommand)GetValue(RightUserButtonCommandProperty);
            set => SetValue(RightUserButtonCommandProperty, value);
        }

        public static readonly BindableProperty LeftImageAddButtonProperty = BindableProperty.Create(
           propertyName: nameof(LeftImageAddButton),
           returnType: typeof(string),
           declaringType: typeof(CustomToolbar),
           defaultBindingMode: BindingMode.TwoWay);
        public string LeftImageAddButton
        {
            get => (string)GetValue(LeftImageAddButtonProperty);
            set => SetValue(LeftImageAddButtonProperty, value);
        }

        public static readonly BindableProperty TitleProperty = BindableProperty.Create(
           propertyName: nameof(Title),
           returnType: typeof(string),
           declaringType: typeof(CustomToolbar),
           defaultBindingMode: BindingMode.TwoWay);
        public string Title
        {
            get => (string)GetValue(TitleProperty);
            set => SetValue(TitleProperty, value);
        }

        public static readonly BindableProperty TitleColorProperty = BindableProperty.Create(
        propertyName: nameof(Title),
        returnType: typeof(Color),
        declaringType: typeof(CustomToolbar),
        defaultBindingMode: BindingMode.TwoWay);
        public Color TitleColor
        {
            get => (Color)GetValue(TitleColorProperty);
            set => SetValue(TitleColorProperty, value);
        }
    }
}