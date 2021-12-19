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

        public LineSpacingLabelRenderer(Context context)
            : base(context)
        {
        }

        #region -- Overrides --

        protected override void OnElementChanged(ElementChangedEventArgs<Label> e)
        {
            base.OnElementChanged(e);

            if (e.OldElement == null)
            {
                _lineSpacingLabel = Element as LineSpacingLabel;
            }

            var lineSpacing = _lineSpacingLabel.LineSpacing;

            Control.SetLineSpacing(1f, lineSpacing);

            UpdateLayout();
        } 

        #endregion
    }
}