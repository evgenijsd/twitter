using Xamarin.Forms;

namespace InterTwitter.Views
{
    public partial class Alert2View : Frame
    {
        public Alert2View()
        {
            InitializeComponent();
            aframe.WidthRequest = Prism.PrismApplicationBase.Current.MainPage.Width - 84;
        }
    }
}