using System;
using System.Threading.Tasks;
using Xamarin.Forms.Xaml;

namespace InterTwitter.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class EditProfilePage : BaseContentPage
    {
        public EditProfilePage()
        {
            InitializeComponent();
        }

        public async void OnLeftArrowTapHandler(object sender, EventArgs e)
        {
            leftArrowImage.Source = (Xamarin.Forms.ImageSource)Prism.PrismApplicationBase.Current.Resources["ic_left_gray"];
            await Task.Delay(200);
            leftArrowImage.Source = (Xamarin.Forms.ImageSource)Prism.PrismApplicationBase.Current.Resources["ic_left_blue"];
        }

        public async void OnCheckTapHandler(object sender, EventArgs e)
        {
            checkImage.Source = (Xamarin.Forms.ImageSource)Prism.PrismApplicationBase.Current.Resources["ic_check_gray"];
            await Task.Delay(200);
            checkImage.Source = (Xamarin.Forms.ImageSource)Prism.PrismApplicationBase.Current.Resources["ic_check_blue"];
        }
    }
}