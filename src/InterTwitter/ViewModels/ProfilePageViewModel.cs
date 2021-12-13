using InterTwitter.Services.Settings;
using InterTwitter.Services.UserService;
using InterTwitter.Views;
using MapNotepad.Helpers;
using Prism.Navigation;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace InterTwitter.ViewModels
{
    public class ProfilePageViewModel : BaseViewModel
    {
        private readonly ISettingsManager _settingsManager;
        private readonly IUserService _userService;
        public ProfilePageViewModel(INavigationService navigationService, ISettingsManager settingsManager, IUserService userService)
            : base(navigationService)
        {
            _settingsManager = settingsManager;
            _userService = userService;

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
            Subscribe();
        }
        #region --- Public Properties ---

        private ObservableCollection<MenuItemViewModel> _menuItems;
        public ObservableCollection<MenuItemViewModel> MenuItems
        {
            get => _menuItems;
            set => SetProperty(ref _menuItems, value);
        }

        private string _userMail;
        public string UserMail
        {
            get => _userMail;
            set => SetProperty(ref _userMail, value);
        }

        private string _userName;
        public string UserName
        {
            get => _userName;
            set => SetProperty(ref _userName, value);
        }

        private string _userBackgroundImage;
        public string UserBackgroundImage
        {
            get => _userBackgroundImage;
            set => SetProperty(ref _userBackgroundImage, value);
        }

        private string _userImagePath;
        public string UserImagePath
        {
            get => _userImagePath;
            set => SetProperty(ref _userImagePath, value);
        }

        public ICommand NavgationCommandAsync => SingleExecutionCommand.FromFunc(NavigationService.GoBackAsync);
        public ICommand NavigationToEditCommandAsync => SingleExecutionCommand.FromFunc(() => NavigationService.NavigateAsync(nameof(EditProfilePage)));

        #endregion

        #region -- Overrides --

        public override Task InitializeAsync(INavigationParameters parameters)
        {
            var user = _userService.GetUserAsync(_settingsManager.UserId).Result.Result;

            UserBackgroundImage = user.BackgroundUserImagePath;
            UserImagePath = user.AvatarPath;
            UserMail = user.Email;
            UserName = user.Name;
            return base.InitializeAsync(parameters);
        }

        #endregion

        #region -- Private Helpers --

        private void Subscribe()
        {
            MessagingCenter.Subscribe<EditProfilePageViewModel>(this, Constants.Messages.USER_PROFILE_CHANGED, UpdateAsync);
        }

        private async void UpdateAsync(object sender)
        {
            await Task.Delay(1);
            var user = _userService.GetUserAsync(_settingsManager.UserId).Result.Result;

            UserBackgroundImage = user.BackgroundUserImagePath;
            UserImagePath = user.AvatarPath;
            UserMail = user.Email;
            UserName = user.Name;
        }

        #endregion

    }
}
