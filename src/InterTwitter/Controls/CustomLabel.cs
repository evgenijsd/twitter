using System;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace InterTwitter.Controls
{
    public class CustomLabel : Label
    {
        public CustomLabel()
        {
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

        public static readonly BindableProperty SpanCommandProperty = BindableProperty.Create(
            propertyName: nameof(SpanCommand),
            returnType: typeof(ICommand),
            declaringType: typeof(CustomLabel),
            defaultValue: null,
            defaultBindingMode: BindingMode.TwoWay);

        public ICommand SpanCommand
        {
            get => (ICommand)GetValue(SpanCommandProperty);
            set => SetValue(SpanCommandProperty, value);
        }

        #endregion

        #region  -- Private helpers --

        private void OnSizeChanged(object sender, EventArgs e)
        {
            var label = sender as Label;

            var countRow = label.Height / label.FontSize;

            //var maxRowNumber = 5;
            var text = label.Text;

            var textLength = text?.Length;

            if (textLength > 175)
            {
                text = text.Substring(0, 165);

                FormattedString formattedString = new FormattedString();

                formattedString.Spans.Add(new Span
                {
                    Text = text,
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
                    Command = SpanCommand,
                });

                formattedString.Spans.Add(span);

                label.FormattedText = formattedString;
            }

            //var fontSize = (sender as Label).FontSize;
            //var maxLabelHeight = fontSize * 1.166666666666669 * maxRowNumber;
            //if ((sender as Label).Height > maxLabelHeight)
            //{
            //    IsSpanVisible = true;
            //}
        }

        #endregion

    }
}
