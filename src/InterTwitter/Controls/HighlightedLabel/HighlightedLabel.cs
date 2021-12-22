using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;
using System.Windows.Input;
using Xamarin.Forms;

namespace InterTwitter.Controls.HighlightedLabel
{
    public class HighlightedLabel : LineSpacingLabel
    {
        #region -- Public helpers --

        public static readonly BindableProperty WordsToHighlightProperty = BindableProperty.Create(
            propertyName: nameof(WordsToHighlight),
            returnType: typeof(IEnumerable<string>),
            declaringType: typeof(HighlightedLabel),
            defaultBindingMode: BindingMode.TwoWay);

        public IEnumerable<string> WordsToHighlight
        {
            get => (IEnumerable<string>)GetValue(WordsToHighlightProperty);
            set => SetValue(WordsToHighlightProperty, value);
        }

        public static readonly BindableProperty MoreCommandProperty = BindableProperty.Create(
            propertyName: nameof(MoreCommand),
            returnType: typeof(ICommand),
            declaringType: typeof(HighlightedLabel),
            defaultBindingMode: BindingMode.TwoWay);

        public ICommand MoreCommand
        {
            get => (ICommand)GetValue(MoreCommandProperty);
            set => SetValue(MoreCommandProperty, value);
        }

        public static readonly BindableProperty KeywordBackgroundColorProperty = BindableProperty.Create(
            propertyName: nameof(KeywordBackgroundColor),
            returnType: typeof(Color),
            declaringType: typeof(HighlightedLabel),
            defaultBindingMode: BindingMode.TwoWay);

        public Color KeywordBackgroundColor
        {
            get => (Color)GetValue(KeywordBackgroundColorProperty);
            set => SetValue(KeywordBackgroundColorProperty, value);
        }

        public static readonly BindableProperty HashtagTextColorProperty = BindableProperty.Create(
            propertyName: nameof(HashtagTextColor),
            returnType: typeof(Color),
            declaringType: typeof(HighlightedLabel),
            defaultBindingMode: BindingMode.TwoWay);

        public Color HashtagTextColor
        {
            get => (Color)GetValue(HashtagTextColorProperty);
            set => SetValue(HashtagTextColorProperty, value);
        }

        #endregion

        protected override void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            base.OnPropertyChanged(propertyName);
            switch (propertyName)
            {
                case nameof(this.WordsToHighlight):
                    if (!string.IsNullOrEmpty(this.Text) && this.WordsToHighlight?.Count() > 0)
                    {
                        DateTime start = DateTime.Now;
                        var tokens = Constants.Methods.GetUniqueWords(this.Text);

                        var positionsAndKeyLenghths = GetPositionsAndKeyLengthsPairs(new List<string>(this.WordsToHighlight));

                        positionsAndKeyLenghths = positionsAndKeyLenghths
                            .OrderBy(x => x.Key)
                            .ThenByDescending(x => x.Value.Length)
                            .ToList();

                        var strings = positionsAndKeyLenghths.Select(x => $"{x.Value} {x.Key}").ToArray();

                        FormattedString formattedString = GetKeywordsMergedWithSimpleText(positionsAndKeyLenghths);

                        MergeWithRestOfSimpleText(positionsAndKeyLenghths.Last(), formattedString);

                        if (MoreCommand != null)
                        {
                            formattedString.Spans.Add(GetCommandSpan("...more"));
                        }

                        this.FormattedText = formattedString;

                    }

                    break;
            }
        }

        #region -- Private helpers --

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

        private FormattedString GetKeywordsMergedWithSimpleText(List<KeyValuePair<int, string>> positionsAndKeys)
        {
            FormattedString formattedString = new FormattedString();
            int previousKeywordPosition = 0;

            foreach (var posAndKey in positionsAndKeys)
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

                formattedString.Spans.Add(GetKeywordSpan(this.Text.Substring(posAndKey.Key, posAndKey.Value.Length)));

                previousKeywordPosition = posAndKey.Key + posAndKey.Value.Length;
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
                keySpan.ForegroundColor = HashtagTextColor;
            }
            else
            {
                keySpan.ForegroundColor = this.TextColor;
                keySpan.BackgroundColor = KeywordBackgroundColor;
            }

            return keySpan;
        }

        private Span GetCommandSpan(string text)
        {
            Span commandSpan = new Span()
            {
                Text = text,
                TextColor = HashtagTextColor,
            };
            commandSpan.GestureRecognizers.Add(new TapGestureRecognizer()
            {
                Command = MoreCommand,
                NumberOfTapsRequired = 1,
            });

            return commandSpan;
        }

        #endregion

        #region -- Public helpers --

        public List<HighlightedWordInfo> GetHighlightedWords()
        {
            var keywords = new List<HighlightedWordInfo>();
            var hashtags = new List<HighlightedWordInfo>();

            string[] wordsToHighlight = WordsToHighlight.ToArray();

            var uniqueHashtagsInText = Text
                .Split(' ')
                .Where(x => Regex.IsMatch(x, Constants.RegexPatterns.HASHTAG_PATTERN))
                .Distinct();

            foreach (var word in wordsToHighlight)
            {
                int wordPosition = 0;
                int positionOfNextWord = 0;

                do
                {
                    wordPosition = Text.IndexOf(word, positionOfNextWord, StringComparison.OrdinalIgnoreCase);

                    if (wordPosition != -1)
                    {
                        positionOfNextWord = wordPosition + word.Length;

                        bool isHashtag = uniqueHashtagsInText.Any(x => x.Equals(word, StringComparison.OrdinalIgnoreCase));

                        HighlightedWordInfo highlightedWord = new HighlightedWordInfo()
                        {
                            Text = word,
                            Position = wordPosition,
                            Length = word.Length,
                            IsHashtag = isHashtag,
                        };

                        if (isHashtag)
                        {
                            hashtags.Add(highlightedWord);
                        }
                        else
                        {
                            keywords.Add(highlightedWord);
                        }
                    }
                }
                while (wordPosition != -1);
            }

            return keywords.Union(hashtags).ToList();
        }

        #endregion
    }
}
