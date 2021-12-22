using InterTwitter.Enums;
using InterTwitter.Helpers;
using System;
using System.Collections.Generic;
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

        #region -- Public properties --

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

        private ICommand _openTweetCommand;
        public ICommand OpenTweetCommand => _openTweetCommand ?? (_openTweetCommand = SingleExecutionCommand.FromFunc(OnOpenTweetCommandAsync));

        #endregion
        #region  -- Private helpers --
        private void OnSizeChanged(object sender, EventArgs e)
        {
            var currentText = OriginalText;

            if (currentText != null)
            {
                if (currentText?.Length > 184)
                {
                    currentText = currentText.Replace('\n', ' ');

                    currentText = currentText.Substring(0, 177);

                    TruncatedText = currentText;
                }
                else
                {
                    Mode = EStateMode.Original;
                }
            }
        }

        #endregion
        #region -- Private helpers --
        private Task OnOpenTweetCommandAsync()
        {
            Mode = EStateMode.Original;

            return Task.CompletedTask;
        }

        #endregion
    }
}