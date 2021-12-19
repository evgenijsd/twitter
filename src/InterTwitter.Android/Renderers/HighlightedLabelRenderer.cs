using Android.Content;
using Android.Text;
using Android.Text.Style;
using InterTwitter.Controls.HighlightedLabel;
using InterTwitter.Droid.Renderers;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using Color = Android.Graphics.Color;

[assembly: ExportRenderer(typeof(HighlightedLabel), typeof(HighlightedLabelRenderer))]
namespace InterTwitter.Droid.Renderers
{
    public class HighlightedLabelRenderer : LabelRenderer
    {
        private HighlightedLabel _highlightedLabel;
        private Color _defaultBackgroundColor;
        private Color _defaultTextColor;
        private Color _keywordBackgroundColor;
        private Color _hashtagTextColor;

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
                _highlightedLabel = Element as HighlightedLabel;

                _defaultBackgroundColor = _highlightedLabel.BackgroundColor.ToAndroid();
                _defaultTextColor = _highlightedLabel.TextColor.ToAndroid();
                _keywordBackgroundColor = _highlightedLabel.KeywordBackgroundColor.ToAndroid();
                _hashtagTextColor = _highlightedLabel.HashtagTextColor.ToAndroid();
            }

            HighlightWordsInText();
        }

        #endregion

        #region -- Private helpers --

        private void HighlightWordsInText()
        {
            _highlightedLabel = Element as HighlightedLabel;

            if (_highlightedLabel != null
                && !string.IsNullOrEmpty(_highlightedLabel.Text)
                && _highlightedLabel.WordsToHighlight != null)
            {
                var highlightedWords = _highlightedLabel.GetHighlightedWords();

                SpannableString spannableString = new SpannableString(_highlightedLabel.Text);

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