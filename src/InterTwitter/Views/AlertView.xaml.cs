using System;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace InterTwitter.Views
{
    public partial class AlertView : Frame
    {
        public AlertView()
        {
            InitializeComponent();
            alertView.WidthRequest = Prism.PrismApplicationBase.Current.MainPage.Width - 84;
            cancelButton.Clicked += OnCancelButtonClicked;
            okButton.Clicked += OnOkButtonClicked;
        }

        private void OnOkButtonClicked(object sender, EventArgs e)
        {
            okButton.IsEnabled = false;
        }

        private void OnCancelButtonClicked(object sender, EventArgs e)
        {
            cancelButton.IsEnabled = false;
        }
    }
}