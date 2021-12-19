using Android.Content;
using Android.Text;
using Android.Text.Style;
using Android.Widget;
using InterTwitter.Controls;
using InterTwitter.Droid.Renderers;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text.RegularExpressions;
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

        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            base.OnElementPropertyChanged(sender, e);

            switch (e.PropertyName)
            {
                case nameof(_highlightedLabel.Text):
                case nameof(_highlightedLabel.WordsToHighlight):
                    HighlightWordsInText();
                    break;
            }
        }

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
            DateTime start = DateTime.Now;

            _highlightedLabel = (HighlightedLabel)Element;

            if (_highlightedLabel != null 
                && !string.IsNullOrEmpty(_highlightedLabel.Text) 
                && _highlightedLabel.WordsToHighlight != null)
            {
                List<string> wordsToHighlight = new List<string>(_highlightedLabel.WordsToHighlight);

                // слова-ключи, которые нужно выделить
                var positionsAndKeys = new List<KeyValuePair<int, string>>();

                // хештеги, которые нужно выделить
                var positionsAndTags = new List<KeyValuePair<int, string>>();

                // все уникальные хештеги в тексте
                var hashtagsInText = _highlightedLabel.Text
                    .Split(' ')
                    .Where(x => Regex.IsMatch(x, Constants.RegexPatterns.HASHTAG_PATTERN))
                    .Distinct();

                // ищем все вхождения для каждого слова в тексте
                foreach (var word in wordsToHighlight)
                {
                    int wordPosition = 0;
                    int positionOfNextWord = 0;

                    do
                    {
                        wordPosition = _highlightedLabel.Text.IndexOf(word, positionOfNextWord, StringComparison.OrdinalIgnoreCase);

                        if (wordPosition != -1)
                        {
                            positionOfNextWord = wordPosition + word.Length;
                            var pair = new KeyValuePair<int, string>(wordPosition, word);

                            // если любой из хештегов текста совпадает с словом для выделения, то добавляем его в список хештегов
                            if (hashtagsInText.Any(x => x.Equals(pair.Value, StringComparison.OrdinalIgnoreCase)))
                            {
                                positionsAndTags.Add(pair);
                            }
                            else
                            {
                                positionsAndKeys.Add(pair);
                            }
                        }

                    }
                    while (wordPosition != -1);
                }

                SpannableString spannableString = new SpannableString(_highlightedLabel.Text);

                foreach (var item in positionsAndKeys)
                {
                    SetSpan(spannableString, item);
                }

                foreach (var item in positionsAndTags)
                {
                    SetSpan(spannableString, item, true);
                }
                
                ((TextView)Control).TextFormatted = spannableString;
            }

            // query - 

            DateTime end = DateTime.Now;
            var time = end - start;
        }
    
        private void SetSpan(SpannableString spannableString, KeyValuePair<int, string> item, bool isHashtag = false)
        {
            spannableString.SetSpan(
                new ForegroundColorSpan(isHashtag ? _hashtagTextColor : _defaultTextColor),
                item.Key,
                item.Key + item.Value.Length,
                SpanTypes.ExclusiveExclusive);

            spannableString.SetSpan(
                new BackgroundColorSpan(isHashtag ? _defaultBackgroundColor : _keywordBackgroundColor),
                item.Key,
                item.Key + item.Value.Length,
                SpanTypes.ExclusiveExclusive);
        }

        #endregion
    }
}