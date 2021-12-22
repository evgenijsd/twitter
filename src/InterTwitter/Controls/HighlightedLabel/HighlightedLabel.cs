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

                        var foundKeysInfo = GetInfoAboutKeyFound(new List<string>(this.WordsToHighlight));

                        var debug_foundKeysInfo = foundKeysInfo.Select(x => $"{x.Position} {x.Text}").ToArray();

                        foundKeysInfo = foundKeysInfo
                            .OrderBy(x => x.Position)
                            .ThenByDescending(x => x.Text.Length)
                            .ToList();

                        debug_foundKeysInfo = foundKeysInfo.Select(x =>
                            $" {(x.IsHashtag ? "tag" : "word")} " +
                            $"{x.Position} {x.Length} {x.Text}").ToArray();

                        string str = string.Empty;

                        try
                        {
                            for (int i = 0; i < foundKeysInfo.Count - 1; i++)
                            {
                                var foundKey = foundKeysInfo[i];
                                int endOfFoundKey = foundKey.Position + foundKey.Length - 1;

                                for (int k = i + 1; k < foundKeysInfo.Count; k++)
                                {
                                    var comparedKey = foundKeysInfo[k];
                                    int endOfComparedKey = comparedKey.Position + comparedKey.Length - 1;

                                    if (endOfFoundKey >= comparedKey.Position)
                                    {
                                        if (endOfFoundKey < endOfComparedKey)
                                        {
                                            int offset = Math.Abs((comparedKey.Position + comparedKey.Length) - (foundKey.Position + foundKey.Length));
                                            string cuttedKeyText = comparedKey.Text.Substring(comparedKey.Length - offset);

                                            var cuttedKey = new HighlightedWordInfo()
                                            {
                                                Position = endOfFoundKey + 1,
                                                Text = cuttedKeyText,
                                                Length = cuttedKeyText.Length,
                                            };

                                            foundKeysInfo.RemoveAt(k);
                                            foundKeysInfo.Insert(k, cuttedKey);
                                        }
                                    }
                                    else
                                    {
                                        break;
                                    }
                                }
                            }
                        }
                        catch (Exception e)
                        {
                            string msg = e.Message;
                        }

                        str = str;
                        debug_foundKeysInfo = foundKeysInfo.Select(x =>
                           $" {(x.IsHashtag ? "tag" : "word")} " +
                           $"{x.Position} {x.Length} {x.Text}").ToArray();

                        str = string.Empty;

                        try
                        {
                            for (int i = 0; i < foundKeysInfo.Count - 1; i++)
                            {
                                var foundKey = foundKeysInfo[i];
                                int endOfFoundKey = foundKey.Position + foundKey.Length - 1;

                                for (int k = i + 1; k < foundKeysInfo.Count;)
                                {
                                    var comparedKey = foundKeysInfo[k];
                                    int endOfComparedKey = comparedKey.Position + comparedKey.Length - 1;

                                    if (endOfFoundKey >= comparedKey.Position)
                                    {
                                        if (endOfFoundKey >= endOfComparedKey)
                                        {
                                            str += comparedKey.Text + "|";
                                            foundKeysInfo.RemoveAt(k);
                                        }
                                    }
                                    else
                                    {
                                        break;
                                    }
                                }
                            }
                        }
                        catch (Exception e)
                        {
                            string msg = e.Message;
                        }

                        str = str;
                         debug_foundKeysInfo = foundKeysInfo.Select(x =>
                            $" {(x.IsHashtag ? "tag" : "word")} " +
                            $"{x.Position} {x.Length} {x.Text}").ToArray();

                        FormattedString formattedString = GetKeуsMergedWithSimpleText(foundKeysInfo);

                        MergeWithRestOfSimpleText(foundKeysInfo.Last(), formattedString);

                        // добавляем команду
                        //if (MoreCommand != null)
                        //{
                        //    formattedString.Spans.Add(GetCommandSpan("...more"));
                        //}
                        this.FormattedText = formattedString;
                    }

                    break;
            }
        }

        #region -- Private helpers --

        private List<HighlightedWordInfo> GetInfoAboutKeyFound(List<string> keys)
        {
            // получаем все слова текста и их длину
            var words = Text
                .Split(' ')
                .Select(x => new HighlightedWordInfo()
                {
                    //IsHashtag = Regex.IsMatch(x, Constants.RegexPatterns.HASHTAG_PATTERN),
                    Text = x,
                    Length = x.Length,
                }).ToArray();

            // узнаем позиции всех слов в тексте
            int nextWordPosition = 0;
            for (int i = 0; i < words.Count(); i++)
            {
                words[i].Position = Text.IndexOf(words[i].Text, nextWordPosition);
                nextWordPosition = words[i].Position + words[i].Length;
            }

            var debugWords = words.Select(x => $"{x.Text} {x.Position}").ToArray();
            var debugKeys = keys.Select(x => $"{x}").ToArray();

            // инфа о найденных ключах
            var foundKeysInfo = new List<HighlightedWordInfo>();

            // ищем ключ во всех словах
            for (int keyIndex = 0; keyIndex < keys.Count(); keyIndex++)
            {
                for (int wordIndex = 0; wordIndex < words.Length; wordIndex++)
                {
                    bool isKeyAHashtag = Regex.IsMatch(keys[keyIndex], Constants.RegexPatterns.HASHTAG_PATTERN);

                    // если слово - тег, то проверяем на полное совпадение
                    if (isKeyAHashtag && words[wordIndex].Text.Equals(keys[keyIndex], StringComparison.OrdinalIgnoreCase))
                    {
                        foundKeysInfo.Add(new HighlightedWordInfo()
                        {
                            Text = keys[keyIndex],
                            Length = keys[keyIndex].Length,
                            Position = words[wordIndex].Position,
                            IsHashtag = true,
                        });
                    }
                    else if (!isKeyAHashtag)
                    {
                        int keywPosition = -1;
                        int nextKeyPosition = 0;

                        do
                        {
                            keywPosition = words[wordIndex].Text.IndexOf(keys[keyIndex], nextKeyPosition, StringComparison.OrdinalIgnoreCase);

                            if (keywPosition != -1)
                            {
                                nextKeyPosition = keywPosition + keys[keyIndex].Length;

                                foundKeysInfo.Add(
                                    new HighlightedWordInfo()
                                    {
                                        Text = keys[keyIndex],
                                        Length = keys[keyIndex].Length,
                                        Position = words[wordIndex].Position + keywPosition,
                                    });
                            }
                        }
                        while (keywPosition != -1);
                    }
                }
            }

            //foreach (var keyword in keywords)
            //{
            //    int keywordPosition = -1;
            //    int positionOfNextKeyword = 0;

            //    do
            //    {
            //        // сделать корректный распознаватель тегов
            //        bool isHashtag = Regex.IsMatch(keyword, Constants.RegexPatterns.HASHTAG_PATTERN);

            //        keywordPosition = this.Text.IndexOf(keyword, positionOfNextKeyword, StringComparison.OrdinalIgnoreCase);

            //        // если ключевое слово - хештег, то ищем по-другому
            //        if (isHashtag)
            //        {
            //        }

            //        if (keywordPosition != -1)
            //        {
            //            positionOfNextKeyword = keywordPosition + keyword.Length;

            //            positionsAndKeyLengths.Add(new KeyValuePair<int, string>(keywordPosition, keyword));
            //        }
            //    }
            //    while (keywordPosition != -1);
            //}
            return foundKeysInfo;
        }

        private FormattedString GetKeуsMergedWithSimpleText(List<HighlightedWordInfo> foundKeysInfo)
        {
            FormattedString formattedString = new FormattedString();
            int previousKeywordPosition = 0;

            foreach (var key in foundKeysInfo)
            {
                // вставка спана с простым текстом между текущим и предыдущим ключем
                if (key.Position - previousKeywordPosition > 0)
                {
                    string textBetweetnKeys = Text.Substring(previousKeywordPosition, key.Position - previousKeywordPosition);
                    formattedString.Spans.Add(new Span
                    {
                        Text = textBetweetnKeys,
                    });
                }

                formattedString.Spans.Add(GetKeywordSpan(key));

                previousKeywordPosition = key.Position + key.Text.Length;
            }

            return formattedString;
        }

        private void MergeWithRestOfSimpleText(HighlightedWordInfo lastKey, FormattedString formattedString)
        {
            if (lastKey.Position < this.Text.Length)
            {
                Span lastSpan = new Span
                {
                    Text = this.Text.Substring(lastKey.Position + lastKey.Text.Length),
                };

                formattedString.Spans.Add(lastSpan);
            }
        }

        private Span GetKeywordSpan(HighlightedWordInfo keyword)
        {
            Span keySpan = new Span()
            {
                Text = keyword.Text,
            };

            if (keyword.IsHashtag)
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
