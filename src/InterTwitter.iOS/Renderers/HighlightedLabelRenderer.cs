using CoreGraphics;
using CoreText;
using Foundation;
using InterTwitter.Controls.HighlightedLabel;
using InterTwitter.Droid.Renderers;
using InterTwitter.iOS.Renderers;
using System;
using System.ComponentModel;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(HighlightedLabel), typeof(HighlightedLabelRenderer))]
namespace InterTwitter.Droid.Renderers
{
    public class HighlightedLabelRenderer : LineSpacingLabelRenderer
    {
        private HighlightedLabel _highlightedLabel;
        private UIColor _defaultBackgroundColor;
        private UIColor _defaultTextColor;
        private UIColor _keywordBackgroundColor;
        private UIColor _hashtagTextColor;

        #region -- Overrides --

        protected override void OnElementChanged(ElementChangedEventArgs<Label> e)
        {
            base.OnElementChanged(e);

            if (e.OldElement == null)
            {
                _highlightedLabel = Element as HighlightedLabel;

                _defaultBackgroundColor = _highlightedLabel.BackgroundColor.ToUIColor();
                _defaultTextColor = _highlightedLabel.TextColor.ToUIColor();
                _keywordBackgroundColor = _highlightedLabel.KeywordBackgroundColor.ToUIColor();
                _hashtagTextColor = _highlightedLabel.HashtagTextColor.ToUIColor();
            }

            HighlightWordsInText();
        }

        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            base.OnElementPropertyChanged(sender, e);

            SetLineSpacing();
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

                UIStringAttributes stringAttributes = new UIStringAttributes()
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