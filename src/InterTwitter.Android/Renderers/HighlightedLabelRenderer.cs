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

[assembly: ExportRenderer(typeof(HighlightedLabel), typeof(HighlightedLabelRenderer))]
namespace InterTwitter.Droid.Renderers
{
    public class HighlightedLabelRenderer : LabelRenderer
    {
        public HighlightedLabelRenderer(Context context) 
            : base(context)
        {
        }

        #region -- Overrides --
        
        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            base.OnElementPropertyChanged(sender, e);

            var highlightedLabel = (HighlightedLabel)Element;

            switch (e.PropertyName)
            {
                case nameof(highlightedLabel.Text):
                case nameof(highlightedLabel.WordsToHighlight):
                    HighlightWordsInText(highlightedLabel);
                    break;
            }
        }

        #endregion

        #region -- Private helpers --

        private void HighlightWordsInText(HighlightedLabel highlightedLabel)
        {
            string text = highlightedLabel.Text;
            List<string> wordsToHighlight = new List<string>(highlightedLabel.WordsToHighlight);

            if (!string.IsNullOrEmpty(text) && wordsToHighlight?.Count > 0)
            {
                var positionAndWords = GetPositionsAndKeyLengthsPairs(text, wordsToHighlight);

                positionAndWords = positionAndWords
                    .OrderBy(x => x.Key)
                    .ThenByDescending(x => x.Value.Length)
                    .ToList();

                var testForDebugging = positionAndWords.Select(x => $"{x.Value} {x.Key}").ToArray();

                SpannableString spannableString = new SpannableString(text);

                var keywordBackgroundColor = highlightedLabel.KeywordBackgroundColor.ToAndroid();
                var hashtagTextColor = highlightedLabel.HashtagTextColor.ToAndroid();

                foreach (var item in positionAndWords)
                {
                    bool isHashtag = Regex.IsMatch(
                        item.Value,
                        Constants.RegexPatterns.HASHTAG_PATTERN,
                        RegexOptions.IgnoreCase);

                    if (isHashtag)
                    {
                        spannableString.SetSpan(
                            new ForegroundColorSpan(hashtagTextColor),
                            item.Key,
                            item.Key + item.Value.Length,
                            SpanTypes.ExclusiveExclusive);

                        spannableString.SetSpan(
                            new BackgroundColorSpan(keywordBackgroundColor),
                            item.Key,
                            item.Key + item.Value.Length,
                            SpanTypes.ExclusiveExclusive);
                    }
                    else
                    {
                        spannableString.SetSpan(
                            new BackgroundColorSpan(keywordBackgroundColor),
                            item.Key,
                            item.Key + item.Value.Length,
                            SpanTypes.ExclusiveExclusive);
                    }
                    
                    ((TextView)Control).TextFormatted = spannableString;
                }


                //spannable.SetSpan(
                //    new BackgroundColorSpan(highlightedLabel.KeywordBackgroundColor.ToAndroid()),
                //        0,
                //        10,
                //        SpanTypes.ExclusiveExclusive);

                //spannable.SetSpan(
                //    new ForegroundColorSpan(highlightedLabel.HashtagTextColor.ToAndroid()),
                //        0,
                //        10,
                //        SpanTypes.ExclusiveExclusive);

            }
        }

        private List<KeyValuePair<int, string>> GetPositionsAndKeyLengthsPairs(string text, List<string> wordsToHighlight)
        {
            List<KeyValuePair<int, string>> positionsAndKeyLengths = new List<KeyValuePair<int, string>>();

            foreach (var word in wordsToHighlight)
            {
                int wordPosition = 0;
                int positionOfNextWord = 0;

                do
                {
                    wordPosition = text.IndexOf(word, positionOfNextWord, StringComparison.OrdinalIgnoreCase);

                    if (wordPosition != -1)
                    {
                        positionOfNextWord = wordPosition + word.Length;

                        positionsAndKeyLengths.Add(new KeyValuePair<int, string>(wordPosition, word));
                    }
                }
                while (wordPosition != -1);
            }

            return positionsAndKeyLengths;
        } 

        #endregion
    }
}