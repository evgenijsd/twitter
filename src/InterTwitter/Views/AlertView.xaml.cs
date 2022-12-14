using InterTwitter.ViewModels;
using Prism.Services.Dialogs;
using Rg.Plugins.Popup.Animations;
using Rg.Plugins.Popup.Enums;
using Rg.Plugins.Popup.Pages;
using System;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace InterTwitter.Views
{
    public partial class AlertView : PopupPage
    {
        public AlertView(DialogParameters param, Action<IDialogParameters> requestClose)
        {
            InitializeComponent();
            frame.WidthRequest = Prism.PrismApplicationBase.Current.MainPage.Width - 84;
            cancelButton.Clicked += OnCancelButtonClicked;
            okButton.Clicked += OnOkButtonClicked;
            BindingContext = new AlertViewModel(param, requestClose);
            Animation = new MoveAnimation(MoveAnimationOptions.Center, MoveAnimationOptions.Center);
           // Animation = new ScaleAnimation();
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