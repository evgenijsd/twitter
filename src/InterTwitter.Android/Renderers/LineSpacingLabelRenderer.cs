using Android.Content;
using InterTwitter.Controls;
using InterTwitter.Droid.Renderers;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(LineSpacingLabel), typeof(LineSpacingLabelRenderer))]
namespace InterTwitter.Droid.Renderers
{
    public class LineSpacingLabelRenderer : LabelRenderer
    {
        protected LineSpacingLabel _lineSpacingLabel;
        protected LineSpacingLabel LineSpacingLabel => _lineSpacingLabel ??= (Element as LineSpacingLabel);

        public LineSpacingLabelRenderer(Context context)
            : base(context)
        {
        }

        #region -- Overrides --

        protected override void OnElementChanged(ElementChangedEventArgs<Label> e)
        {
            base.OnElementChanged(e);

            SetLineSpacing();
        }

        #endregion

        #region -- Private hepers

        protected void SetLineSpacing()
        {
            if (LineSpacingLabel != null)
            {
                var lineSpacing = LineSpacingLabel.LineSpacing;

                Control.SetLineSpacing(1f, lineSpacing);

                UpdateLayout();
            }
        }

        #endregion
    }
}