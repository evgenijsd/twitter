using Xamarin.Forms;

namespace InterTwitter.Controls
{
    public class SearchEntry : Entry
    {
        public Color TintColor { get; set; } = (Color)Prism.PrismApplicationBase.Current.Resources["appcolor_i1"];
    }
}
