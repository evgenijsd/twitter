using CoreGraphics;
using CoreText;
using Foundation;
using InterTwitter.Controls.HighlightedLabel;
using InterTwitter.Droid.Renderers;
using System;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(HighlightedLabel), typeof(HighlightedLabelRenderer))]
namespace InterTwitter.Droid.Renderers
{
    public class HighlightedLabelRenderer : LabelRenderer
    {
        private HighlightedLabel _highlightedLabel;
        private CGColor _defaultBackgroundColor;
        private CGColor _defaultTextColor;
        private CGColor _keywordBackgroundColor;
        private CGColor _hashtagTextColor;

        #region -- Overrides --

        protected override void OnElementChanged(ElementChangedEventArgs<Label> e)
        {
            base.OnElementChanged(e);

            if (e.OldElement == null)
            {
                _highlightedLabel = Element as HighlightedLabel;

                _defaultBackgroundColor = _highlightedLabel.BackgroundColor.ToCGColor();
                _defaultTextColor = _highlightedLabel.TextColor.ToCGColor();
                _keywordBackgroundColor = _highlightedLabel.KeywordBackgroundColor.ToCGColor();
                _hashtagTextColor = _highlightedLabel.HashtagTextColor.ToCGColor();
            }

            HighlightWordsInText();
        }

        #endregion

        #region -- Private helpers --

        private void HighlightWordsInText()
        {
            _highlightedLabel = Element as HighlightedLabel;

            if (_highlightedLabel != null
                && !string.IsNullOrEmpty(_highlightedLabel.Text)
                && _highlightedLabel.WordsToHighlight != null)
            {
                var highlightedWords = _highlightedLabel.GetHighlightedWords();

                var attributedString = new NSMutableAttributedString(_highlightedLabel.Text);

                CTStringAttributes stringAttributes = new CTStringAttributes()
                {
                    ForegroundColor = _defaultTextColor,
                    BackgroundColor = _keywordBackgroundColor
                };

                bool isHashtagStyleSet = false;
                
                foreach (var item in highlightedWords)
                {
                    var range = new NSRange(item.Position, item.Length);

                    if (!isHashtagStyleSet && item.IsHashtag)
                    {
                        stringAttributes.ForegroundColor = _hashtagTextColor;
                        stringAttributes.BackgroundColor = _defaultBackgroundColor;
                        isHashtagStyleSet = true;
                    }

                    attributedString.AddAttributes(stringAttributes, range);
                }

                (Control as UILabel).AttributedText = attributedString;
            }
        }

        #endregion
    }
}