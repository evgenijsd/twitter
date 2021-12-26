using InterTwitter.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace InterTwitter.Controls.HighlightedLabel
{
    public class HighlightedLabel : LineSpacingLabel
    {
        public HighlightedLabel()
        {
            SetStyle();
        }

        #region -- Public helpers --

        public static readonly BindableProperty KeysToHighlightProperty = BindableProperty.Create(
            propertyName: nameof(KeysToHighlight),
            returnType: typeof(IEnumerable<string>),
            declaringType: typeof(HighlightedLabel),
            defaultBindingMode: BindingMode.TwoWay);

        public IEnumerable<string> KeysToHighlight
        {
            get => (IEnumerable<string>)GetValue(KeysToHighlightProperty);
            set => SetValue(KeysToHighlightProperty, value);
        }

        public static readonly BindableProperty KeyBackgroundColorProperty = BindableProperty.Create(
            propertyName: nameof(KeyBackgroundColor),
            returnType: typeof(Color),
            declaringType: typeof(HighlightedLabel),
            defaultBindingMode: BindingMode.TwoWay);

        public Color KeyBackgroundColor
        {
            get => (Color)GetValue(KeyBackgroundColorProperty);
            set => SetValue(KeyBackgroundColorProperty, value);
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

        public static readonly BindableProperty OriginalTextProperty = BindableProperty.Create(
            propertyName: nameof(OriginalText),
            returnType: typeof(string),
            declaringType: typeof(HighlightedLabel),
            defaultBindingMode: BindingMode.TwoWay);

        public string OriginalText
        {
            get => (string)GetValue(OriginalTextProperty);
            set => SetValue(OriginalTextProperty, value);
        }

        private ICommand _unfoldingText;
        public ICommand UnfoldingTextCommand => _unfoldingText ?? (_unfoldingText = SingleExecutionCommand.FromFunc(OnUnfoldingTextCommandAsync));

        #endregion

        #region -- Overrides --

        protected override void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            base.OnPropertyChanged(propertyName);

            if (propertyName == OriginalTextProperty.PropertyName && !string.IsNullOrEmpty(OriginalText))
            {
                SetText(OriginalText);
            }
        }

        #endregion

        #region -- Private helpers --

        private void SetText(string originalText)
        {
            (this.FormattedText, this.Text) = (null, null);

            if (originalText?.Length > 184)
            {
                string truncatedText = originalText.Replace('\n', ' ').Substring(0, 177);

                FormattedString formattedString = GetFormattedText(truncatedText);

                Span unfoldingTextSpan = new Span
                {
                    Text = "...more",
                };

                unfoldingTextSpan.SetDynamicResource(TextColorProperty, "appcolor_i1");

                TapGestureRecognizer tapGestureRecognizer = new TapGestureRecognizer();
                unfoldingTextSpan.GestureRecognizers.Add(tapGestureRecognizer);
                tapGestureRecognizer.Command = UnfoldingTextCommand;

                formattedString.Spans.Add(unfoldingTextSpan);

                FormattedText = formattedString;
            }
            else
            {
                FormattedText = GetFormattedText(originalText);
            }
        }

        private FormattedString GetFormattedText(string text)
        {
            FormattedString formattedString = null;

            if (KeysToHighlight?.Count() > 0)
            {
                formattedString = GetHighlightedFormattedString(text);
            }

            if (formattedString == null || formattedString.Spans.Count == 0)
            {
                formattedString = new FormattedString();

                Span textSpan = new Span
                {
                    Text = text,
                };

                formattedString.Spans.Add(textSpan);
            }

            return formattedString;
        }

        private Task OnUnfoldingTextCommandAsync()
        {
            if (Device.RuntimePlatform != Device.iOS)
            {
                this.FormattedText = GetFormattedText(OriginalText);
            }
            else
            {
                this.Text = OriginalText;
            }

            return Task.CompletedTask;
        }

        private void SetStyle()
        {
            var style = Device.RuntimePlatform == Device.Android
                ? "tstyle_i7"
                : "tstyle_i16";

            SetDynamicResource(StyleProperty, style);
        }

        private FormattedString GetHighlightedFormattedString(string text)
        {
            FormattedString formattedString = new FormattedString();

            var infoAboutKeysFound = GetInfoAboutKeysFound(text, KeysToHighlight.ToArray());

            if (infoAboutKeysFound?.Count > 0)
            {
                TrimIntersectingKeys(infoAboutKeysFound);

                DeleteAllSubKeys(infoAboutKeysFound);

                formattedString = GetKeуsMergedWithSimpleText(text, infoAboutKeysFound.ToArray());
            }

            return formattedString;
        }

        private List<KeyInfo> GetInfoAboutKeysFound(string text, string[] keys)
        {
            var words = text.Split(' ').Select(x => new KeyInfo()
            {
                Text = x,
                Length = x.Length,
            }).ToArray();

            for (int i = 0, nextWordPosition = 0; i < words.Length; i++)
            {
                words[i].Position = text.IndexOf(words[i].Text, nextWordPosition);
                nextWordPosition = words[i].Position + words[i].Length;
            }

            var foundKeysInfo = new List<KeyInfo>();

            for (int keyIndex = 0; keyIndex < keys.Length; keyIndex++)
            {
                for (int wordIndex = 0; wordIndex < words.Length; wordIndex++)
                {
                    int keywPosition = 0;
                    int nextKeyPosition = 0;

                    do
                    {
                        keywPosition = words[wordIndex].Text.IndexOf(keys[keyIndex], nextKeyPosition, StringComparison.OrdinalIgnoreCase);

                        if (keywPosition != -1)
                        {
                            nextKeyPosition = keywPosition + keys[keyIndex].Length;

                            bool isKeyAHashtag =
                                Regex.IsMatch(keys[keyIndex], Constants.RegexPatterns.HASHTAG_PATTERN) &&
                                words[wordIndex].Text.Equals(keys[keyIndex], StringComparison.OrdinalIgnoreCase);

                            foundKeysInfo.Add(
                                new KeyInfo()
                                {
                                    Text = keys[keyIndex],
                                    Length = keys[keyIndex].Length,
                                    Position = words[wordIndex].Position + keywPosition,
                                    IsHashtag = isKeyAHashtag,
                                });
                        }
                    }
                    while (keywPosition != -1);
                }
            }

            foundKeysInfo = foundKeysInfo
                .OrderBy(x => x.Position)
                .ThenByDescending(x => x.Text.Length)
                .ToList();

            return foundKeysInfo;
        }

        private void TrimIntersectingKeys(List<KeyInfo> foundKeysInfo)
        {
            for (int i = 0; i < foundKeysInfo.Count - 1; i++)
            {
                var foundKey = foundKeysInfo[i];
                int endOfFoundKey = foundKey.Position + foundKey.Length - 1;
                bool isKeysIntersect = true;

                for (int k = i + 1; isKeysIntersect && k < foundKeysInfo.Count; k++)
                {
                    var comparedKey = foundKeysInfo[k];
                    int endOfComparedKey = comparedKey.Position + comparedKey.Length - 1;
                    isKeysIntersect = endOfFoundKey >= comparedKey.Position;

                    if (isKeysIntersect && endOfFoundKey < endOfComparedKey)
                    {
                        int lengthOfCutPart = Math.Abs((comparedKey.Position + comparedKey.Length) - (foundKey.Position + foundKey.Length));

                        string cuttedKeyText = comparedKey.Text.Substring(comparedKey.Length - lengthOfCutPart);

                        var cuttedKey = new KeyInfo()
                        {
                            Text = cuttedKeyText,
                            Position = endOfFoundKey + 1,
                            Length = cuttedKeyText.Length,
                        };

                        foundKeysInfo.RemoveAt(k);
                        foundKeysInfo.Insert(k, cuttedKey);
                    }
                }
            }
        }

        private void DeleteAllSubKeys(List<KeyInfo> foundKeysInfo)
        {
            for (int i = 0; i < foundKeysInfo.Count - 1; i++)
            {
                var foundKey = foundKeysInfo[i];
                int endOfFoundKey = foundKey.Position + foundKey.Length - 1;
                bool isFoundKeyContainsComparedKey = true;

                for (int k = i + 1; isFoundKeyContainsComparedKey && k < foundKeysInfo.Count;)
                {
                    var comparedKey = foundKeysInfo[k];
                    int endOfComparedKey = comparedKey.Position + comparedKey.Length - 1;

                    isFoundKeyContainsComparedKey = endOfFoundKey >= comparedKey.Position;

                    if (isFoundKeyContainsComparedKey && endOfFoundKey >= endOfComparedKey)
                    {
                        foundKeysInfo.RemoveAt(k);
                    }
                }
            }
        }

        private FormattedString GetKeуsMergedWithSimpleText(string text, KeyInfo[] foundKeysInfo)
        {
            FormattedString formattedString = new FormattedString();
            int previousKeyPosition = 0;

            foreach (var key in foundKeysInfo)
            {
                if (key.Position - previousKeyPosition > 0)
                {
                    string textBetweetnKeys = text.Substring(previousKeyPosition, key.Position - previousKeyPosition);

                    formattedString.Spans.Add(new Span
                    {
                        Text = textBetweetnKeys,
                    });
                }

                formattedString.Spans.Add(GetKeySpan(text, key));

                previousKeyPosition = key.Position + key.Text.Length;
            }

            var lastKey = foundKeysInfo.Last();

            if (lastKey.Position < text.Length)
            {
                Span lastSpan = new Span
                {
                    Text = text.Substring(lastKey.Position + lastKey.Text.Length),
                };

                formattedString.Spans.Add(lastSpan);
            }

            return formattedString;
        }

        private Span GetKeySpan(string text, KeyInfo keyword)
        {
            Span keySpan = new Span()
            {
                Text = text.Substring(keyword.Position, keyword.Length),
            };

            if (keyword.IsHashtag)
            {
                keySpan.ForegroundColor = HashtagTextColor;
            }
            else
            {
                keySpan.ForegroundColor = this.TextColor;
                keySpan.BackgroundColor = KeyBackgroundColor;
            }

            return keySpan;
        }

        #endregion
    }
}
