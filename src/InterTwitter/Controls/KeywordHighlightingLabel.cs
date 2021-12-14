using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using Xamarin.Forms;

namespace InterTwitter.Controls
{
    public class KeywordHighlightingLabel : LineSpacingLabel
    {
        private Color _defaultForeColor = (Color)Prism.PrismApplicationBase.Current.Resources["appcolor_i3"];
        private Color _highlightForeColor = (Color)Prism.PrismApplicationBase.Current.Resources["appcolor_i1"];
        private Color _highlightBackgroundColor = (Color)Prism.PrismApplicationBase.Current.Resources["appcolor_i8"];

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
                case nameof(this.Text):
                case nameof(this.Keywords):
                    if (!string.IsNullOrEmpty(this.Text) && this.Keywords?.Count > 0)
                    {
                        var positionsAndKeywordsPairs = GetPositionsAndKeywordsPairs(this.Keywords);

                        if (positionsAndKeywordsPairs.Count > 0)
                        {
                            FormattedString formattedString = GetKeywordsMergedWithSimpleText(positionsAndKeywordsPairs);

                            MergeWithRestOfSimpleText(positionsAndKeywordsPairs.Last(), formattedString);

                            this.FormattedText = formattedString;
                        }
                    }

                    break;
            }
        }

        #endregion

        #region -- Private helpers --

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

        private FormattedString GetKeywordsMergedWithSimpleText(SortedDictionary<int, string> positionKeywordPairs)
        {
            FormattedString formattedString = new FormattedString();
            int previousKeywordPosition = 0;

            foreach (var pairs in positionKeywordPairs)
            {
                if (pairs.Key - previousKeywordPosition > 0)
                {
                    string str = Text.Substring(previousKeywordPosition, pairs.Key - previousKeywordPosition);

                    Span span = new Span
                    {
                        Text = str,
                    };

                    formattedString.Spans.Add(span);
                }

                formattedString.Spans.Add(GetKeywordSpan(pairs.Value));

                previousKeywordPosition = pairs.Key + pairs.Value.Length;
            }

            return formattedString;
        }

        private void MergeWithRestOfSimpleText(KeyValuePair<int, string> lastPairKeywordAndPosition, FormattedString formattedString)
        {
            if (lastPairKeywordAndPosition.Key < this.Text.Length)
            {
                Span lastSpan = new Span
                {
                    Text = this.Text.Substring(lastPairKeywordAndPosition.Key + lastPairKeywordAndPosition.Value.Length),
                };

                formattedString.Spans.Add(lastSpan);
            }
        }

        private Span GetKeywordSpan(string keyword)
        {
            Span keySpan = new Span()
            {
                Text = keyword,
            };

            if (keyword.FirstOrDefault() == '#')
            {
                keySpan.ForegroundColor = _highlightForeColor;
            }
            else
            {
                keySpan.ForegroundColor = _defaultForeColor;
                keySpan.BackgroundColor = _highlightBackgroundColor;
            }

            return keySpan;
        }

        #endregion
    }
}
