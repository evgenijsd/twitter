using InterTwitter.Enums;
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
    public partial class TestLabel : ContentView
    {
        public TestLabel()
        {
            InitializeComponent();
            SizeChanged += OnSizeChanged;
        }

        public static readonly BindableProperty ModeProperty = BindableProperty.Create(
            propertyName: nameof(Mode),
            returnType: typeof(EStateMode),
            declaringType: typeof(TestLabel),
            defaultBindingMode: BindingMode.TwoWay);

        public EStateMode Mode
        {
            get => (EStateMode)GetValue(ModeProperty);
            set => SetValue(ModeProperty, value);
        }

        public static readonly BindableProperty FullTextProperty = BindableProperty.Create(
            propertyName: nameof(FullText),
            returnType: typeof(string),
            declaringType: typeof(TestLabel),
            defaultBindingMode: BindingMode.TwoWay);

        public string FullText
        {
            get => (string)GetValue(FullTextProperty);
            set => SetValue(FullTextProperty, value);
        }

        public static readonly BindableProperty TruncatedTextProperty = BindableProperty.Create(
            propertyName: nameof(TruncatedText),
            returnType: typeof(string),
            declaringType: typeof(TestLabel),
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

        //private void OnSizeChanged(object sender, EventArgs e)
        //{
        //    var currentText = FullText;

        //    if (currentText?.Length > 199)
        //    {
        //        currentText = currentText.Replace('\n', ' ');

        //        currentText = currentText.Substring(0, 192);

        //        FormattedString formattedString = new FormattedString();

        //        formattedString.Spans.Add(new Span
        //        {
        //            Text = currentText,
        //        });

        //        formattedString.Spans.Add(new Span
        //        {
        //            Text = "...",
        //        });

        //        Span span = new Span
        //        {
        //            Text = "more",
        //            ForegroundColor = Color.FromHex("#2356C5"),
        //        };

        //        span.GestureRecognizers.Add(new TapGestureRecognizer()
        //        {
        //            Command = OpenTweetCommand,
        //            CommandParameter = BindingContext,
        //        });

        //        formattedString.Spans.Add(span);

        //        TruncatedText = formattedString;
        //    }
        //}
        private void OnSizeChanged(object sender, EventArgs e)
        {
            var currentText = FullText;

            if (currentText?.Length > 131)
            {
                currentText = currentText.Replace('\n', ' ');

                currentText = currentText.Substring(0, 124);

                TruncatedText = currentText;
            }
            else
            {
                TruncatedText = currentText;
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
