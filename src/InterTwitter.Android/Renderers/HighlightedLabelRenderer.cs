using Android.Content;
using Android.Text;
using Android.Text.Style;
using Android.Widget;
using InterTwitter.Controls.HighlightedLabel;
using InterTwitter.Droid.Renderers;
using System;
using System.Collections.Generic;
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
                _highlightedLabel = (HighlightedLabel)Element;
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
            var start = DateTime.Now;

            _highlightedLabel = (HighlightedLabel)Element;

            if (_highlightedLabel != null
                && !string.IsNullOrEmpty(_highlightedLabel.Text)
                && _highlightedLabel.WordsToHighlight != null)
            {
                List<string> wordsToHighlight = new List<string>(_highlightedLabel.WordsToHighlight);

                var highlightedWords = _highlightedLabel.GetHighlightedWords();

                SpannableString spannableString = new SpannableString(_highlightedLabel.Text);

                foreach (var item in highlightedWords)
                {
                    SetSpan(spannableString, item);
                }

                ((TextView)Control).TextFormatted = spannableString;
            }
            var end = (DateTime.Now - start).Milliseconds;
        }
    
        private void SetSpan(SpannableString spannableString, HighlightedWord highlightedWord)
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