using MapNotePad.Helpers;
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
    public partial class CustomEntry : Grid
    {
        public CustomEntry()
        {
            InitializeComponent();
        }

        #region -- Public properties --
        public static readonly BindableProperty TextProperty = BindableProperty.Create(
            propertyName: nameof(Text),
            returnType: typeof(string),
            declaringType: typeof(CustomEntry),
            defaultValue: string.Empty,
            defaultBindingMode: BindingMode.TwoWay);

        public string Text
        {
            get => (string)GetValue(TextProperty);
            set => SetValue(TextProperty, value);
        }

        public static readonly BindableProperty MaxLengthProperty = BindableProperty.Create(
            propertyName: nameof(MaxLength),
            returnType: typeof(string),
            declaringType: typeof(CustomEntry),
            defaultValue: string.Empty,
            defaultBindingMode: BindingMode.TwoWay);

        public string MaxLength
        {
            get => (string)GetValue(MaxLengthProperty);
            set => SetValue(MaxLengthProperty, value);
        }

        public static readonly BindableProperty TextColorProperty = BindableProperty.Create(
            propertyName: nameof(TextColor),
            returnType: typeof(Color),
            declaringType: typeof(CustomEntry),
            defaultValue: Color.Silver,
            defaultBindingMode: BindingMode.TwoWay);

        public Color TextColor
        {
            get => (Color)GetValue(TextColorProperty);
            set => SetValue(TextColorProperty, value);
        }

        public static readonly BindableProperty FontFamilyProperty = BindableProperty.Create(
            propertyName: nameof(FontFamily),
            returnType: typeof(string),
            declaringType: typeof(CustomEntry),
            defaultValue: string.Empty,
            defaultBindingMode: BindingMode.TwoWay);

        public string FontFamily
        {
            get => (string)GetValue(FontFamilyProperty);
            set => SetValue(FontFamilyProperty, value);
        }

        public static readonly BindableProperty PlaceholderProperty = BindableProperty.Create(
            propertyName: nameof(Placeholder),
            returnType: typeof(string),
            declaringType: typeof(CustomEntry),
            defaultValue: string.Empty,
            defaultBindingMode: BindingMode.TwoWay);

        public string Placeholder
        {
            get => (string)GetValue(PlaceholderProperty);
            set => SetValue(PlaceholderProperty, value);
        }

        public static readonly BindableProperty PlaceholderColorProperty = BindableProperty.Create(
            propertyName: nameof(PlaceholderColor),
            returnType: typeof(Color),
            declaringType: typeof(CustomEntry),
            defaultValue: Color.Silver,
            defaultBindingMode: BindingMode.TwoWay);

        public Color PlaceholderColor
        {
            get => (Color)GetValue(PlaceholderColorProperty);
            set => SetValue(PlaceholderColorProperty, value);
        }

        public static readonly BindableProperty IsPasswordProperty = BindableProperty.Create(
            propertyName: nameof(IsPassword),
            returnType: typeof(bool),
            declaringType: typeof(CustomEntry),
            defaultValue: false,
            defaultBindingMode: BindingMode.TwoWay);

        public bool IsPassword
        {
            get => (bool)GetValue(IsPasswordProperty);
            set => SetValue(IsPasswordProperty, value);
        }

        public static readonly BindableProperty IsPasswordHideProperty = BindableProperty.Create(
            propertyName: nameof(IsPasswordHide),
            returnType: typeof(bool),
            declaringType: typeof(CustomEntry),
            defaultValue: false,
            defaultBindingMode: BindingMode.TwoWay);

        public bool IsPasswordHide
        {
            get => (bool)GetValue(IsPasswordHideProperty);
            set => SetValue(IsPasswordHideProperty, value);
        }

        public static readonly BindableProperty IsFocusedVisibleProperty = BindableProperty.Create(
            propertyName: nameof(IsFocusedVisible),
            returnType: typeof(bool),
            declaringType: typeof(CustomEntry),
            defaultValue: false,
            defaultBindingMode: BindingMode.TwoWay);

        public bool IsFocusedVisible
        {
            get => (bool)GetValue(IsFocusedVisibleProperty);
            set => SetValue(IsFocusedVisibleProperty, value);
        }

        public static readonly BindableProperty IsFocusedButtonProperty = BindableProperty.Create(
            propertyName: nameof(IsFocusedButton),
            returnType: typeof(bool),
            declaringType: typeof(CustomEntry),
            defaultValue: false,
            defaultBindingMode: BindingMode.TwoWay);

        public bool IsFocusedButton
        {
            get => (bool)GetValue(IsFocusedButtonProperty);
            set => SetValue(IsFocusedButtonProperty, value);
        }

        public static readonly BindableProperty IsButtonEyeVisibleProperty = BindableProperty.Create(
            propertyName: nameof(IsButtonEyeVisible),
            returnType: typeof(bool),
            declaringType: typeof(CustomEntry),
            defaultValue: false,
            defaultBindingMode: BindingMode.TwoWay);

        public bool IsButtonEyeVisible
        {
            get => (bool)GetValue(IsButtonEyeVisibleProperty);
            set => SetValue(IsButtonEyeVisibleProperty, value);
        }

        public static readonly BindableProperty IsButtonClearVisibleProperty = BindableProperty.Create(
            propertyName: nameof(IsButtonClearVisible),
            returnType: typeof(bool),
            declaringType: typeof(CustomEntry),
            defaultValue: false,
            defaultBindingMode: BindingMode.TwoWay);

        public bool IsButtonClearVisible
        {
            get => (bool)GetValue(IsButtonClearVisibleProperty);
            set => SetValue(IsButtonClearVisibleProperty, value);
        }

        public static readonly BindableProperty ClearImageSourceProperty = BindableProperty.Create(
            propertyName: nameof(ClearImageSource),
            returnType: typeof(string),
            declaringType: typeof(CustomEntry),
            defaultValue: string.Empty,
            defaultBindingMode: BindingMode.TwoWay);

        public string ClearImageSource
        {
            get => (string)GetValue(ClearImageSourceProperty);
            set => SetValue(ClearImageSourceProperty, value);
        }

        public static readonly BindableProperty EyeOnImageSourceProperty = BindableProperty.Create(
            propertyName: nameof(EyeOnImageSource),
            returnType: typeof(string),
            declaringType: typeof(CustomEntry),
            defaultValue: string.Empty,
            defaultBindingMode: BindingMode.TwoWay);

        public string EyeOnImageSource
        {
            get => (string)GetValue(EyeOnImageSourceProperty);
            set => SetValue(EyeOnImageSourceProperty, value);
        }

        public static readonly BindableProperty EyeOffImageSourceProperty = BindableProperty.Create(
            propertyName: nameof(EyeOffImageSource),
            returnType: typeof(string),
            declaringType: typeof(CustomEntry),
            defaultValue: string.Empty,
            defaultBindingMode: BindingMode.TwoWay);

        public string EyeOffImageSource
        {
            get => (string)GetValue(EyeOffImageSourceProperty);
            set => SetValue(EyeOffImageSourceProperty, value);
        }

        public static readonly BindableProperty ImageSourceProperty = BindableProperty.Create(
            propertyName: nameof(ImageSource),
            returnType: typeof(string),
            declaringType: typeof(CustomEntry),
            defaultValue: string.Empty,
            defaultBindingMode: BindingMode.TwoWay);

        public string ImageSource
        {
            get => (string)GetValue(ImageSourceProperty);
            set => SetValue(ImageSourceProperty, value);
        }

        private ICommand _buttonEyeCommand;
        public ICommand ButtonEyeCommand => _buttonEyeCommand ??= SingleExecutionCommand.FromFunc(OnButtonEyeCommandAsync);

        private ICommand _buttonClearCommand;
        public ICommand ButtonClearCommand => _buttonClearCommand ??= SingleExecutionCommand.FromFunc(OnButtonClearCommandAsync);

        private ICommand _focusedCommand;
        public ICommand FocusedCommand => _focusedCommand ??= SingleExecutionCommand.FromFunc(OnFocusedCommandAsync);

        private ICommand _unfocusedCommand;
        public ICommand UnFocusedCommand => _unfocusedCommand ??= SingleExecutionCommand.FromFunc(OnUnfocusedCommandAsync);

        #endregion

        #region -- Overrides --

        protected override void OnPropertyChanged(string propertyName)
        {
            base.OnPropertyChanged(propertyName);

            switch (propertyName)
            {
                case nameof(IsPassword):
                    IsPasswordHide = IsPassword;
                    break;
                case nameof(Text):
                case nameof(ClearImageSource):
                case nameof(EyeOnImageSource):
                case nameof(EyeOffImageSource):

                    if (IsPassword)
                    {
                        if (IsPasswordHide)
                        {
                            ImageSource = EyeOnImageSource;
                        }
                        else
                        {
                            ImageSource = EyeOffImageSource;
                        }

                        if (string.IsNullOrEmpty(Text))
                        {
                            IsButtonEyeVisible = false;
                        }
                        else
                        {
                            IsButtonEyeVisible = true;
                        }
                    }

                    if (string.IsNullOrEmpty(Text))
                    {
                        IsButtonClearVisible = false;
                    }
                    else
                    {
                        IsButtonClearVisible = true;
                    }

                    break;
            }
        }

        #endregion

        #region -- Private methods --

        private Task OnButtonEyeCommandAsync()
        {
            if (IsPassword)
            {
                if (ImageSource == EyeOnImageSource)
                {
                    IsPasswordHide = false;
                    ImageSource = EyeOffImageSource;
                }
                else
                {
                    IsPasswordHide = true;
                    ImageSource = EyeOnImageSource;
                }
            }

            return Task.CompletedTask;
        }

        private Task OnFocusedCommandAsync()
        {
            if (!IsPassword)
            {
                IsButtonEyeVisible = true;
            }

            IsFocusedVisible = true;
            IsFocusedButton = true;
            CustomEntryLocal.Placeholder = string.Empty;

            return Task.CompletedTask;
        }

        private Task OnUnfocusedCommandAsync()
        {
            if (!IsPassword)
            {
                IsButtonEyeVisible = false;
            }

            if (string.IsNullOrEmpty(Text))
            {
                IsFocusedVisible = false;
            }

            IsFocusedButton = false;
            CustomEntryLocal.Placeholder = Placeholder;

            return Task.CompletedTask;
        }

        private Task OnButtonClearCommandAsync()
        {
            Text = string.Empty;
            IsFocusedVisible = false;

            return Task.CompletedTask;
        }

        #endregion

    }
}
