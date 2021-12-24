using InterTwitter.Helpers;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace InterTwitter.Controls
{
    public class UnfoldingLabel : LineSpacingLabel
    {
        public UnfoldingLabel()
        {
            SetStyle();
        }

        #region -- Public properties --

        public static readonly BindableProperty OriginalTextProperty = BindableProperty.Create(
            propertyName: nameof(OriginalText),
            returnType: typeof(string),
            declaringType: typeof(UnfoldingLabel),
            defaultBindingMode: BindingMode.TwoWay);
        public string OriginalText
        {
            get => (string)GetValue(OriginalTextProperty);
            set => SetValue(OriginalTextProperty, value);
        }

        private ICommand _openTweetCommand;
        public ICommand OpenTweetCommand => _openTweetCommand ?? (_openTweetCommand = SingleExecutionCommand.FromFunc(OnOpenTweetCommandAsync));

        #endregion

        #region -- Overrides --

        protected override void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            base.OnPropertyChanged(propertyName);
            if (propertyName == OriginalTextProperty.PropertyName && !string.IsNullOrEmpty(OriginalText))
            {
                SetText(OriginalText);
            }
        }

        #endregion

        #region -- Private helpers --

        private void SetText(string originalText)
        {
            (this.FormattedText, this.Text) = (null, null);

            if (originalText?.Length > 184)
            {
                var formattedText = new FormattedString();
                var truncatedSpan = new Span
                {
                    Text = originalText.Replace('\n', ' ').Substring(0, 177),
                };

                var moreSpan = new Span
                {
                    Text = "...more",
                };
                moreSpan.SetDynamicResource(TextColorProperty, "appcolor_i1");

                TapGestureRecognizer tapGestureRecognizer = new TapGestureRecognizer();
                tapGestureRecognizer.Command = OpenTweetCommand;
                moreSpan.GestureRecognizers.Add(tapGestureRecognizer);

                formattedText.Spans.Add(truncatedSpan);
                formattedText.Spans.Add(moreSpan);

                FormattedText = formattedText;
            }
            else
            {
                Text = originalText;
            }
        }

        private Task OnOpenTweetCommandAsync()
        {
            this.FormattedText = null;
            this.Text = OriginalText;
            return Task.CompletedTask;
        }

        private void SetStyle()
        {
            var style = Device.RuntimePlatform == Device.Android
                ? "tstyle_i7"
                : "tstyle_i16";
            SetDynamicResource(StyleProperty, style);
        }

        #endregion
    }
}
