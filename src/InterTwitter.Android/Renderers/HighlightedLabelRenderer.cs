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

            HighlightWordsInText();
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