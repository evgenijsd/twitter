using Android.Content;
using Android.Text;
using Android.Text.Style;
using InterTwitter.Controls.HighlightedLabel;
using InterTwitter.Droid.Renderers;
using System.ComponentModel;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using Color = Android.Graphics.Color;

[assembly: ExportRenderer(typeof(HighlightedLabel), typeof(HighlightedLabelRenderer))]
namespace InterTwitter.Droid.Renderers
{
    public class HighlightedLabelRenderer : LineSpacingLabelRenderer
    {
        private Color _defaultBackgroundColor;
        private Color _defaultTextColor;
        private Color _keywordBackgroundColor;
        private Color _hashtagTextColor;

        private HighlightedLabel _customElement;
        private HighlightedLabel CustomElement =>  _customElement ??= (Element as HighlightedLabel);

        public HighlightedLabelRenderer(Context context)
            : base(context)
        {
        }

        #region -- Overrides --

        protected override void OnElementChanged(ElementChangedEventArgs<Label> e)
        {
            base.OnElementChanged(e);

            if (e.OldElement == null)
            {
                _defaultBackgroundColor = CustomElement.BackgroundColor.ToAndroid();
                _defaultTextColor = CustomElement.TextColor.ToAndroid();
                _keywordBackgroundColor = CustomElement.KeywordBackgroundColor.ToAndroid();
                _hashtagTextColor = CustomElement.HashtagTextColor.ToAndroid();
            }

            //HighlightWordsInText();
        }

        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            base.OnElementPropertyChanged(sender, e);

            SetLineSpacing();
        }

        #endregion

        #region -- Private helpers --

        private void HighlightWordsInText()
        {
            if (CustomElement != null
                && !string.IsNullOrEmpty(CustomElement.Text)
                && CustomElement.WordsToHighlight != null)
            {
                var highlightedWords = CustomElement.GetHighlightedWords();

                SpannableString spannableString = new SpannableString(CustomElement.Text);

                foreach (var item in highlightedWords)
                {
                    SetSpan(spannableString, item);
                }

                // test
                //CustomElement.BackgroundColor = Xamarin.Forms.Color.Violet;

                //var span1 = new Span()
                //{
                //    Text = "first",
                //    TextColor = Xamarin.Forms.Color.Red,

                //    FontSize = 20
                //};

                //span1.GestureRecognizers.Add(new TapGestureRecognizer
                //{
                //    Command = new Command(i =>
                //    {
                //        CustomElement.BackgroundColor = Xamarin.Forms.Color.Green;
                //    }),
                //    NumberOfTapsRequired = 1
                //});

                //var span2 = new Span()
                //{
                //    Text = "second",
                //    TextColor = Xamarin.Forms.Color.Red,
                //    FontSize = 20
                //};

                //span2.GestureRecognizers.Add(new TapGestureRecognizer
                //{
                //    Command = new Command(i =>
                //    {
                //        CustomElement.BackgroundColor = Xamarin.Forms.Color.Orange;
                //    }),
                //    NumberOfTapsRequired = 1
                //});

                //SpannableFactory spannableFactory = new SpannableFactory();

                //FormattedString formattedString = new FormattedString();
                //formattedString.Spans.Add(span1);
                //formattedString.Spans.Add(span2);

                //CustomElement.FormattedText = formattedString;

                //var strBuild = new SpannableStringBuilder();
                //strBuild.Append(spannableString);
                //strBuild.Append(CustomElement.FormattedText.ToAttributed(Font.Default, Xamarin.Forms.Color.Orange, Control));
                //Control.TextFormatted = strBuild;

                // test

                Control.TextFormatted = spannableString;
            }
        }
    
        private void SetSpan(SpannableString spannableString, HighlightedWordInfo highlightedWord)
        {
            spannableString.SetSpan(
                new ForegroundColorSpan(highlightedWord.IsHashtag ? _hashtagTextColor : _defaultTextColor),
                highlightedWord.Position,
                highlightedWord.Position + highlightedWord.Length,
                SpanTypes.ExclusiveExclusive);

            spannableString.SetSpan(
                new BackgroundColorSpan(highlightedWord.IsHashtag ? _defaultBackgroundColor : _keywordBackgroundColor),
                highlightedWord.Position,
                highlightedWord.Position + highlightedWord.Length,
                SpanTypes.ExclusiveExclusive);
        }

        #endregion
    }
}