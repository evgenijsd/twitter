using InterTwitter.Views;
using MapNotepad.Helpers;
using Prism.Navigation;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace InterTwitter.ViewModels
{
    public class BookmarksPageViewModel : BaseTabViewModel
    {
        public BookmarksPageViewModel(INavigationService navigationService)
                                                  : base(navigationService)
        {
            IconPath = "ic_bookmarks_gray";
        }
        #region --- Public Properties ---

        public ICommand GotoFlyoutCommand => SingleExecutionCommand.FromFunc(OnGotoFlyoutCommand);

        #endregion

        #region --- Overrides ---

        public override void OnAppearing()
        {
            IconPath = "ic_bookmarks_blue.png";
        }

        public override void OnDisappearing()
        {
            IconPath = "ic_bookmarks_gray.png";
        }

        #endregion

        #region --- Private Helpers ---

        private Task OnGotoFlyoutCommand()
        {
            MessagingCenter.Send(this, "OpenSidebar", true);
            MessagingCenter.Send(this, "TabChange", typeof(BookmarksPage));
            return Task.FromResult(true);
        }

        #endregion
    }
}
