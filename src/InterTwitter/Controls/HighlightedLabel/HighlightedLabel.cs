using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using Xamarin.Forms;

namespace InterTwitter.Controls.HighlightedLabel
{
    public class HighlightedLabel : Label
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

        #region -- Public helpers --

        public List<HighlightedWord> GetHighlightedWords()
        {
            var keywords = new List<HighlightedWord>();
            var hashtags = new List<HighlightedWord>();

            string[] wordsToHighlight = WordsToHighlight.ToArray();

            var hashtagsInText = Text
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

                        bool isHashtag = hashtagsInText.Any(x => x.Equals(word, StringComparison.OrdinalIgnoreCase));

                        HighlightedWord highlightedWord = new HighlightedWord()
                        {
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
