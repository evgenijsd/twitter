using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
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

        //TestText = "#amas masd deveex max";

        //    Keywords = new List<string>()
        //    {
        //        "dev",
        //        "ex",
        //        "#am",
        //        "ma",
        //        "amas",
        //        "masd",
        //        "as",
        //    };
        protected override void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            base.OnPropertyChanged(propertyName);

            switch (propertyName)
            {
                case nameof(this.Keywords):
                    if (!string.IsNullOrEmpty(this.Text) && this.Keywords?.Count > 0)
                    {
                        DateTime start = DateTime.Now;
                        var tokens = Constants.TweetsSearch.GetUniqueWords(this.Text);

                        var positionsAndKeyLenghths = GetPositionsAndKeyLengthsPairs(this.Keywords);

                        positionsAndKeyLenghths = positionsAndKeyLenghths
                            .OrderBy(x => x.Key)
                            .ThenByDescending(x => x.Value.Length)
                            .ToList();

                        var strings = positionsAndKeyLenghths.Select(x => $"{x.Value} {x.Key}").ToArray();

                        FormattedString formattedString = GetKeywordsMergedWithSimpleText(positionsAndKeyLenghths);

                        MergeWithRestOfSimpleText(positionsAndKeyLenghths.Last(), formattedString);

                        this.FormattedText = formattedString;

                        DateTime end = DateTime.Now;
                        var time = end - start;
                        var r = 4;
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
                    keywordPosition = this.Text.IndexOf(keyword, positionOfNextKeyword, StringComparison.OrdinalIgnoreCase);

                    if (keywordPosition != -1)
                    {
                        positionOfNextKeyword = keywordPosition + keyword.Length;

                        positionsAndKeyLengths.Add(new KeyValuePair<int, string>(keywordPosition, keyword));
                    }
                }
                while (keywordPosition != -1);
            }

            return positionsAndKeyLengths;
        }

        private FormattedString GetKeywordsMergedWithSimpleText(List<KeyValuePair<int, string>> keys)
        {
            FormattedString formattedString = new FormattedString();
            int previousKeywordPosition = 0;
            KeyValuePair<int, string> lastAddedKey;

            for (int keyIndex = 0; keyIndex < keys.Count; keyIndex++)
            {
                // вставка спана с простым текстом между текущим и предыдущим ключем
                if (keys[keyIndex].Key - previousKeywordPosition > 0)
                {
                    string textBetweetnKeys = Text.Substring(previousKeywordPosition, keys[keyIndex].Key - previousKeywordPosition);

                    formattedString.Spans.Add(new Span
                    {
                        Text = textBetweetnKeys,
                    });
                }

                // самый простой случай - добавить спан первого ключа
                if (keyIndex == 0)
                {
                    lastAddedKey = keys[keyIndex];
                    formattedString.Spans.Add(GetKeywordSpan(keys[keyIndex].Value));
                }
                else
                {
                    // ключ не нарушает границы последнего добавленного ключа
                    if (previousKeywordPosition <= keys[keyIndex].Key)
                    {
                        lastAddedKey = keys[keyIndex];
                        formattedString.Spans.Add(GetKeywordSpan(keys[keyIndex].Value));
                    }
                    else
                    {
                        string subKey = lastAddedKey.Value + keys[keyIndex].Value.Substring(previousKeywordPosition);
                        if (true)
                        {
                        }
                    }
                }

                previousKeywordPosition = keys[keyIndex].Key + keys[keyIndex].Value.Length;
            }

           /* foreach (var posAndKey in positionKeywordPairs)
            {
                // вставка спана с простым текстом между текущим и предыдущим ключем
                if (posAndKey.Key - previousKeywordPosition > 0)
                {
                    string textBetweetnKeys = Text.Substring(previousKeywordPosition, posAndKey.Key - previousKeywordPosition);

                    formattedString.Spans.Add(new Span
                    {
                        Text = textBetweetnKeys,
                    });
                }
                else if (formattedString.Spans.Count > 0)
                {
                }

                // решаем, стоит ли обрезать текущий ключ перед вставкой, если он уже частично содержится в предыдущем ключе
                if (true)
                {
                    formattedString.Spans.Add(GetKeywordSpan(this.Text.Substring(posAndKey.Key, posAndKey.Value.Length)));
                }
                else
                {
                    formattedString.Spans.Add(GetKeywordSpan(this.Text.Substring(posAndKey.Key, posAndKey.Value.Length)));
                }

                previousKeywordPosition = posAndKey.Key + posAndKey.Value.Length;
            }*/

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
