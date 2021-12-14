using Xamarin.Forms;
using System.Collections.Generic;
using System;
using System.Runtime.CompilerServices;
using System.Linq;
using System.Text.RegularExpressions;

namespace InterTwitter.Controls
{
    public class KeywordHighlightingLabel : LineSpacingLabel
    {
        private Color defaultForeColor = (Color)Prism.PrismApplicationBase.Current.Resources["appcolor_i3"];
        private Color highlightForeColor = (Color)Prism.PrismApplicationBase.Current.Resources["appcolor_i1"];
        private Color highlightBackgroundColor = (Color)Prism.PrismApplicationBase.Current.Resources["appcolor_i8"];

        public KeywordHighlightingLabel()
        {
        }

        #region -- Public helpers --

        public static readonly BindableProperty KeywordsProperty = BindableProperty.Create(
            propertyName: nameof(Keywords),
            returnType: typeof(List<string>),
            declaringType: typeof(KeywordHighlightingLabel),
            defaultBindingMode: BindingMode.TwoWay);

        public List<string> Keywords
        {
            get => (List<string>)GetValue(KeywordsProperty);
            set => SetValue(KeywordsProperty, value);
        }

        #endregion

        #region -- Overrides --

        protected override void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            base.OnPropertyChanged(propertyName);

            switch (propertyName)
            {
                case nameof(Text):
                case nameof(Keywords):
                    if (!string.IsNullOrEmpty(Text) && Keywords?.Count > 0)
                    {
                        var positionKeywordPairs = GetPositionsAndKeywordsPairs(Keywords);

                        if (positionKeywordPairs.Count > 0)
                        {
                            FormattedString formattedString = new FormattedString();

                            int previousKeywordPosition = 0;
                            int textLen = Text.Length;

                            foreach (var pairs in positionKeywordPairs)
                            {
                                if (pairs.Key - previousKeywordPosition > 0)
                                {
                                    string str = Text.Substring(previousKeywordPosition, pairs.Key - previousKeywordPosition);
                                    formattedString.Spans.Add(new Span { Text = str, });
                                }

                                formattedString.Spans.Add(GetKeywordSpan(pairs.Value));

                                previousKeywordPosition = pairs.Key + pairs.Value.Length;
                            }

                            KeyValuePair<int, string> positionKeyworPairsLast = positionKeywordPairs.Last();

                            if (positionKeyworPairsLast.Key < Text.Length)
                            {
                                Span lastSpan = new Span
                                {
                                    Text = this.Text.Substring(positionKeyworPairsLast.Key + positionKeyworPairsLast.Value.Length),
                                };

                                formattedString.Spans.Add(lastSpan);
                            }

                            this.FormattedText = formattedString;
                        }
                    }

                    break;
            }
        }

        #endregion

        #region -- Private helpers --

        private void FormFormattedString()
        {
        }

        private void MergeKeywordsWidthOtherText()
        {
        }

        private SortedDictionary<int, string> GetPositionsAndKeywordsPairs(List<string> keywords)
        {
            SortedDictionary<int, string> positionKeywordSpanPairs = new SortedDictionary<int, string>();

            foreach (var keyword in keywords)
            {
                int keywordPosition = -1;
                int lastKeywordPosition = 0;

                do
                {
                    keywordPosition = Text.IndexOf(keyword, lastKeywordPosition, StringComparison.OrdinalIgnoreCase);

                    if (keywordPosition != -1)
                    {
                        lastKeywordPosition = keywordPosition + keyword.Length;

                        positionKeywordSpanPairs.Add(keywordPosition, keyword);
                    }
                }
                while (keywordPosition != -1);
            }

            return positionKeywordSpanPairs;
        }

        private Span GetKeywordSpan(string keyword)
        {
            Span keySpan = new Span()
            {
                Text = keyword,
            };

            if (keyword.FirstOrDefault() == '#')
            {
                keySpan.ForegroundColor = highlightForeColor;
            }
            else
            {
                keySpan.ForegroundColor = defaultForeColor;
                keySpan.BackgroundColor = highlightBackgroundColor;
            }

            return keySpan;
        }

        #endregion
    }
}
