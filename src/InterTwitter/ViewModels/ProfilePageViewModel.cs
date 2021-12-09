using InterTwitter.Views;
using MapNotepad.Helpers;
using Prism.Navigation;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace InterTwitter.ViewModels
{
    public class ProfilePageViewModel : BaseViewModel
    {
        public ProfilePageViewModel(INavigationService navigationService)
            : base(navigationService)
        {
            MenuItems = new ObservableCollection<MenuItemViewModel>(new[]
                {
                    new MenuItemViewModel
                    {
                        Id = 0, Title = "Posts",
                        TargetType = typeof(HomePage),
                        ImageSource = "ic_home_gray",
                        TextColor = (Xamarin.Forms.Color)Prism.PrismApplicationBase.Current.Resources["appcolor_i4"],
                    },

                    new MenuItemViewModel
                    {
                        Id = 1,
                        Title = "Likes",
                        TargetType = typeof(SearchPage),
                        ImageSource = "ic_search_gray",
                        TextColor = (Xamarin.Forms.Color)Prism.PrismApplicationBase.Current.Resources["appcolor_i4"],
                    },
                });
        }
        #region --- Public Properties ---

        private ObservableCollection<MenuItemViewModel> _menuItems;
        public ObservableCollection<MenuItemViewModel> MenuItems
        {
            get => _menuItems;
            set => SetProperty(ref _menuItems, value);
        }

        private string _userMail = "JKenter@gmail.com";
        public string UserMail
        {
            get => _userMail;
            set => SetProperty(ref _userMail, value);
        }

        private string _userName = "Jaylon Kenter";
        public string UserName
        {
            get => _userName;
            set => SetProperty(ref _userName, value);
        }

        private string _userBackgroundImage = "https://picsum.photos/500/500?image=122";
        public string UserBackgroundImage
        {
            get => _userBackgroundImage;
            set => SetProperty(ref _userBackgroundImage, value);
        }

        private string _userImagePath = "https://picsum.photos/500/500?image=290";
        public string UserImagePath
        {
            get => _userImagePath;
            set => SetProperty(ref _userImagePath, value);
        }

        public ICommand NavgationCommandAsync => SingleExecutionCommand.FromFunc(NavigationService.GoBackAsync);
        public ICommand NavigationToEditCommandAsync => SingleExecutionCommand.FromFunc(() => NavigationService.NavigateAsync(nameof(EditProfilePage)));

        #endregion

    }
}
