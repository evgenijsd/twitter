using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;
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
                case nameof(this.Keywords):
                    if (!string.IsNullOrEmpty(this.Text) && this.Keywords?.Count > 0)
                    {
                        var positionsAndKeyLenghths = GetPositionsAndKeyLengthsPairs(this.Keywords);

                        positionsAndKeyLenghths = positionsAndKeyLenghths
                            .OrderBy(x => x.Key)
                            .ThenByDescending(x => x.Value)
                            .GroupBy(x => x.Key)
                            .Select(x => x.First()).ToList();

                        // trash - не светит дубли
                        for (int i = 0; i < positionsAndKeyLenghths.Count; i++)
                        {
                            var itemA = positionsAndKeyLenghths.ElementAt(i);

                            for (int j = i + 1; j <= positionsAndKeyLenghths.Count - 1; j++)
                            {
                                var itemB = positionsAndKeyLenghths.ElementAt(j);

                                //if (itemA.Key + itemA.Value.Length >= itemB.Key + itemB.Value.Length)
                                if (Contain(itemA, itemB))
                                {
                                    positionsAndKeyLenghths.RemoveAt(j);
                                    j--;
                                }
                            }
                        }

                        if (positionsAndKeyLenghths.Count > 0)
                        {
                            FormattedString formattedString = GetKeywordsMergedWithSimpleText(positionsAndKeyLenghths);

                            MergeWithRestOfSimpleText(positionsAndKeyLenghths.Last(), formattedString);

                            this.FormattedText = formattedString;
                        }
                    }

                    break;
            }
        }

        #endregion

        #region -- Private helpers --

        private bool Contain(KeyValuePair<int, string> pairA, KeyValuePair<int, string> pairB)
        {
            return pairA.Key + pairA.Value.Length >= pairB.Key + pairB.Value.Length;
        }

        private List<KeyValuePair<int, string>> GetPositionsAndKeyLengthsPairs(List<string> keywords)
        {
            List<KeyValuePair<int, string>> positionsAndKeyLengths = new List<KeyValuePair<int, string>>();

            foreach (var keyword in keywords)
            {
                int keywordPosition = -1;
                int positionOfNextKeyword = 0;

                do
                {
                    try
                    {
                        keywordPosition = this.Text.IndexOf(keyword, positionOfNextKeyword, StringComparison.OrdinalIgnoreCase);
                    }
                    catch (Exception)
                    {
                    }

                    // #amas mas g coff
                    /* ! новый ключ - подстрока существующего - не добавляем в словарь */
                    /* abcd */
                    /* abc */
                    /* d */

                    /* новый ключ - подстрока существующего, его длина больше - замена в словаре по индексу */
                    /* abcd */
                    /* abcdef */

                    /* новый ключ частично внутри существующего - добавляем (или заменяем значение ключа) в словарь выступающую часть*/
                    /* abcd */
                    /*   cdaas */
                    if (keywordPosition != -1)
                    {
                        // # #teatime - crush
                        positionOfNextKeyword = keywordPosition + keyword.Length;

                        positionsAndKeyLengths.Add(new KeyValuePair<int, string>(keywordPosition, keyword));
                    }
                }
                while (keywordPosition != -1);
            }

            return positionsAndKeyLengths;
        }

        private FormattedString GetKeywordsMergedWithSimpleText(List<KeyValuePair<int, string>> positionKeywordPairs)
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

                formattedString.Spans.Add(GetKeywordSpan(this.Text.Substring(pairs.Key, pairs.Value.Length)));

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

            bool isHashtag = Regex.IsMatch(
                keyword,
                Constants.RegexPatterns.HASHTAG_PATTERN,
                RegexOptions.IgnoreCase);

            if (isHashtag)
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
