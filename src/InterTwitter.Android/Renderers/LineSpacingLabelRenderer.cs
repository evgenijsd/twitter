using InterTwitter.Controls;
using InterTwitter.Droid.Renderers;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(LineSpacingLabel), typeof(LineSpacingLabelRenderer))]
namespace InterTwitter.Droid.Renderers
{
    public class LineSpacingLabelRenderer : LabelRenderer
    {
        protected LineSpacingLabel LineSpacingLabel { get; private set; }

        public LineSpacingLabelRenderer()
        {
        }

        protected override void OnElementChanged(ElementChangedEventArgs<Label> e)
        {
            base.OnElementChanged(e);

            if (e.OldElement == null)
            {
                this.LineSpacingLabel = (LineSpacingLabel)this.Element;
            }

            var lineSpacing = this.LineSpacingLabel.LineSpacing;

            this.Control.SetLineSpacing(1f, lineSpacing);

            this.UpdateLayout();
        }
    }
}