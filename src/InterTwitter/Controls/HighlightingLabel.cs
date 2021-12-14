using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Xamarin.Forms;

namespace InterTwitter.Controls
{
    public class HighlighttingLabel : LineSpacingLabel
    {
        public HighlighttingLabel()
        {
            var formattedText = new FormattedString();
            formattedText.Spans.Add(new Span { Text = "text_1", ForegroundColor = Color.LightSkyBlue });
            formattedText.Spans.Add(new Span { Text = "text_2", ForegroundColor = Color.Black, BackgroundColor = Color.LightBlue });
            this.FormattedText = formattedText;
        }

        protected override void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            base.OnPropertyChanged(propertyName);
        }

        protected override void OnPropertyChanging([CallerMemberName] string propertyName = null)
        {
            base.OnPropertyChanging(propertyName);
        }
    }
}
