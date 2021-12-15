using InterTwitter.iOS.Renderers;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(CollectionView), typeof(NoBounceRenderer))]
namespace InterTwitter.iOS.Renderers
{
    public class NoBounceRenderer : CollectionViewRenderer
    {
        public NoBounceRenderer()
        {

        }

        #region -- Overrides --

        protected override void OnElementChanged(ElementChangedEventArgs<GroupableItemsView> e)
        {
            base.OnElementChanged(e);

            if (e.NewElement != null)
            {
                Controller.CollectionView.Bounces = false;
            }
        }

        #endregion
    }
}