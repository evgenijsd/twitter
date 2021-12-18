using InterTwitter.Enums;
using InterTwitter.Helpers;
using System;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace InterTwitter.Controls
{
    public partial class CustomLabel : ContentView
    {
        public CustomLabel()
        {
            InitializeComponent();
            SizeChanged += OnSizeChanged;
        }

        public static readonly BindableProperty ModeProperty = BindableProperty.Create(
            propertyName: nameof(Mode),
            returnType: typeof(EStateMode),
            declaringType: typeof(CustomLabel),
            defaultBindingMode: BindingMode.TwoWay);

        public EStateMode Mode
        {
            get => (EStateMode)GetValue(ModeProperty);
            set => SetValue(ModeProperty, value);
        }

        public static readonly BindableProperty OriginalTextProperty = BindableProperty.Create(
            propertyName: nameof(OriginalText),
            returnType: typeof(string),
            declaringType: typeof(CustomLabel),
            defaultBindingMode: BindingMode.TwoWay);

        public string OriginalText
        {
            get => (string)GetValue(OriginalTextProperty);
            set => SetValue(OriginalTextProperty, value);
        }

        public static readonly BindableProperty TruncatedTextProperty = BindableProperty.Create(
            propertyName: nameof(TruncatedText),
            returnType: typeof(string),
            declaringType: typeof(CustomLabel),
            defaultBindingMode: BindingMode.TwoWay);

        public string TruncatedText
        {
            get => (string)GetValue(TruncatedTextProperty);
            set => SetValue(TruncatedTextProperty, value);
        }

        public static readonly BindableProperty MoreCommandProperty = BindableProperty.Create(
            propertyName: nameof(MoreCommand),
            returnType: typeof(ICommand),
            declaringType: typeof(CustomLabel),
            defaultValue: null,
            defaultBindingMode: BindingMode.TwoWay);

        public ICommand MoreCommand
        {
            get => (ICommand)GetValue(MoreCommandProperty);
            set => SetValue(MoreCommandProperty, value);
        }

        private ICommand _openTweetCommand;
        public ICommand OpenTweetCommand => _openTweetCommand ?? (_openTweetCommand = SingleExecutionCommand.FromFunc(TestAsync));

        #region  -- Private helpers --
        private void OnSizeChanged(object sender, EventArgs e)
        {
            var currentText = OriginalText;

            if (currentText?.Length > 131)
            {
                currentText = currentText.Replace('\n', ' ');

                currentText = currentText.Substring(0, 124);

                TruncatedText = currentText;
            }
            else
            {
                Mode = EStateMode.Original;
            }
        }
        #endregion

        #region -- Private helpers --
        private Task TestAsync()
        {
            Mode = EStateMode.Original;

            return Task.CompletedTask;
        }

        #endregion

    }
}
