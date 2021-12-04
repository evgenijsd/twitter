using MapNotepad.Helpers;
using Prism.Navigation;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace InterTwitter.ViewModels
{
    public class HomePageViewModel : BaseTabViewModel
    {
        public HomePageViewModel(INavigationService navigationService)
                                                  : base(navigationService)
        {
            IconPath = "ic_home_gray.png";
        }

        #region --- Public Properties ---

        public ICommand GotoFlyoutCommand => SingleExecutionCommand.FromFunc(OnGotoFlyoutCommand);

        #endregion

        #region --- Overrides ---

        public override void OnAppearing()
        {
            IconPath = "ic_home_blue.png";
        }

        public override void OnDisappearing()
        {
            IconPath = "ic_home_gray.png";
        }

        #endregion

        #region --- Private Helpers ---

        private Task OnGotoFlyoutCommand()
        {
            MessagingCenter.Send(this, "OpenSidebar", true);
            return Task.FromResult(true);
        }

        #endregion
    }
}
