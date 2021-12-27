using InterTwitter.Helpers;
using InterTwitter.Models;
using InterTwitter.Services;
using InterTwitter.Services.Share;
using InterTwitter.Views;
using Prism.Navigation;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;

namespace InterTwitter.ViewModels
{
    public class ProfilePageViewModel : BaseViewModel
    {
        private readonly IRegistrationService _registrationService;
        private readonly IAuthorizationService _authorizationService;
        private readonly IShareService _shareService;

        public ProfilePageViewModel(
            INavigationService navigationService,
            IRegistrationService registrationService,
            IAuthorizationService authorizationService,
            IShareService shareService)
            : base(navigationService)
        {
            _registrationService = registrationService;
            _authorizationService = authorizationService;
            _shareService = shareService;

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

        #region -- Public Properties --

        private string _avatarIcon;
        public string ProfileIcon
        {
            get => _avatarIcon;
            set => SetProperty(ref _avatarIcon, value);
        }

        private string _profileName;
        public string ProfileName
        {
            get => _profileName;
            set => SetProperty(ref _profileName, value);
        }

        private string _profileEmail;
        public string ProfileEmail
        {
            get => _profileEmail;
            set => SetProperty(ref _profileEmail, value);
        }

        private ObservableCollection<MenuItemViewModel> _menuItems;
        public ObservableCollection<MenuItemViewModel> MenuItems
        {
            get => _menuItems;
            set => SetProperty(ref _menuItems, value);
        }

        public ICommand NavgationCommandAsync => SingleExecutionCommand.FromFunc(NavigationService.GoBackAsync);

        public ICommand _shareUserProfileTapCommand;
        public ICommand ShareUserProfileTapCommand => _shareUserProfileTapCommand ??= SingleExecutionCommand.FromFunc(OnShareUserProfileTapCommandAsync);

        #endregion

        #region -- Overrides --

        public override void OnNavigatedTo(INavigationParameters parameters)
        {
            base.OnNavigatedTo(parameters);

            if (parameters.TryGetValue(Constants.Navigation.USER, out UserModel user))
            {
                ProfileName = user.Name;
                ProfileEmail = user.Email;
                ProfileIcon = user.AvatarPath;
            }
        }

        #endregion

        #region -- Private helpers --

        private async Task OnShareUserProfileTapCommandAsync()
        {
            var aOResult = await _registrationService.GetByIdAsync(_authorizationService.UserId);

            if (aOResult.IsSuccess)
            {
                var user = aOResult.Result;
                string uri = $"{Constants.Values.APP_USER_LINK}{Constants.Values.APP_USER_LINK_ID}/{user.Id}";

                await _shareService.ShareTextRequest(user.Name, uri);
            }
        }

        #endregion
    }
}
