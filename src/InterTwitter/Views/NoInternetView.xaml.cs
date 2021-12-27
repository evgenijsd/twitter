using InterTwitter.ViewModels;
using Prism.Services.Dialogs;
using Rg.Plugins.Popup.Animations;
using Rg.Plugins.Popup.Enums;
using Rg.Plugins.Popup.Pages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace InterTwitter.Views
{
    public partial class NoInternetView : PopupPage
    {
        public NoInternetView(DialogParameters param, Action<IDialogParameters> requestClose)
        {
            InitializeComponent();
            frame.WidthRequest = Prism.PrismApplicationBase.Current.MainPage.Width - 30;
            BindingContext = new AlertViewModel(param, requestClose);
        }
    }
}