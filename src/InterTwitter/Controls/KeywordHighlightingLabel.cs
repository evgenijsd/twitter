using System.Collections.Generic;
using Xamarin.Forms;

namespace InterTwitter.Controls
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
    }
}
