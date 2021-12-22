using Xamarin.Forms;

namespace InterTwitter.Views
{
    public partial class AlertView : Frame
    {
        public AlertView()
        {
            InitializeComponent();
            aframe.WidthRequest = Prism.PrismApplicationBase.Current.MainPage.Width - 84;
        }
    }
}