using InterTwitter.Helpers;
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
    public partial class CustomLabel2 : ContentView
    {
        public CustomLabel2()
        {
            InitializeComponent();
            SizeChanged += OnSizeChanged;
        }
        #region -- Public properties --

        public static readonly BindableProperty IsSpanVisibleProperty = BindableProperty.Create(
             propertyName: nameof(IsSpanVisible),
             returnType: typeof(bool),
             declaringType: typeof(CustomLabel),
             defaultBindingMode: BindingMode.TwoWay);

        public bool IsSpanVisible
        {
            get => (bool)GetValue(IsSpanVisibleProperty);
            set => SetValue(IsSpanVisibleProperty, value);
        }

        public static readonly BindableProperty MoreCommandProperty = BindableProperty.Create(
            propertyName: nameof(MoreCommand),
            returnType: typeof(ICommand),
            declaringType: typeof(CustomLabel),
            defaultValue: null,
            defaultBindingMode: BindingMode.TwoWay);

        private ICommand _openTweetCommand;
        public ICommand OpenTweetCommand => _openTweetCommand ?? (_openTweetCommand = SingleExecutionCommand.FromFunc(TestAsync));

        public ICommand MoreCommand
        {
            get => (ICommand)GetValue(MoreCommandProperty);
            set => SetValue(MoreCommandProperty, value);
        }

        public static readonly BindableProperty MoreCommandParameterProperty = BindableProperty.Create(
            propertyName: nameof(MoreCommandParameter),
            returnType: typeof(object),
            declaringType: typeof(CustomLabel),
            defaultBindingMode: BindingMode.TwoWay);

        public object MoreCommandParameter
        {
            get => GetValue(MoreCommandParameterProperty);
            set => SetValue(MoreCommandParameterProperty, value);
        }

        #endregion

        #region  -- Private helpers --

        private void OnSizeChanged(object sender, EventArgs e)
        {
            var label = sender as Label;

            var fullText = label.Text;

            var currentText = label.Text;

            if (currentText?.Length > 199)
            {
                currentText = currentText.Replace('\n', ' ');

                currentText.Substring(0, 192);

                FormattedString formattedString = new FormattedString();

                formattedString.Spans.Add(new Span
                {
                    Text = currentText,
                });

                formattedString.Spans.Add(new Span
                {
                    Text = "...",
                });

                Span span = new Span
                {
                    Text = "more",
                    ForegroundColor = Color.FromHex("#2356C5"),
                };

                span.GestureRecognizers.Add(new TapGestureRecognizer()
                {
                    Command = OpenTweetCommand,
                    CommandParameter = BindingContext,
                });

                formattedString.Spans.Add(span);

                label.FormattedText = formattedString;
            }
        }

        #endregion

        #region -- Private helpers --
        private Task TestAsync()
        {
            throw new NotImplementedException();
        }

        #endregion

    }
}