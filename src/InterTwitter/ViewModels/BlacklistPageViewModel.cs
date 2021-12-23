using InterTwitter.Helpers;
using InterTwitter.Extensions;
using InterTwitter.Models;
using InterTwitter.Services.Settings;
using InterTwitter.Services.UserService;
using Prism.Navigation;
using Prism.Services.Dialogs;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using InterTwitter.Views;

namespace InterTwitter.ViewModels
{
    public class BlacklistPageViewModel : BaseViewModel
    {
        private readonly ISettingsManager _settingsManager;
        private readonly IUserService _userService;
        private readonly IDialogService _dialogService;

        private bool _isBlacklistPage;
        private bool _isMutelistPage;
        private UserModel _currentUser;

        public BlacklistPageViewModel(INavigationService navigationService, ISettingsManager settingsManager, IUserService userService, IDialogService dialogService)
            : base(navigationService)
        {
            _settingsManager = settingsManager;
            _userService = userService;
            _dialogService = dialogService;
        }

        #region -- Public Properties --

        private ObservableCollection<UserViewModel> _usersList;
        public ObservableCollection<UserViewModel> UsersList
        {
            get => _usersList;
            set => SetProperty(ref _usersList, value);
        }

        private string _title;
        public string Title
        {
            get => _title;
            set => SetProperty(ref _title, value);
        }

        public ICommand NavigateBackCommandAsync => SingleExecutionCommand.FromFunc(() => NavigationService.GoBackAsync());

        #endregion

        #region -- Overrides --

        public override async Task InitializeAsync(INavigationParameters parameters)
        {
            if (_isBlacklistPage = parameters.ContainsKey(Constants.Navigation.BLACKLIST))
            {
                Title = Resources.Resource.Blacklist;
                _currentUser = parameters[Constants.Navigation.BLACKLIST] as UserModel;

                await InitBlacklistAsync();
            }
            else if (_isMutelistPage = parameters.ContainsKey(Constants.Navigation.MUTELIST))
            {
                Title = Resources.Resource.Mute;
                _currentUser = parameters[Constants.Navigation.MUTELIST] as UserModel;

                await InitMutelistAsync();
            }
        }

        #endregion

        #region -- Private Helpers --

        private async Task InitBlacklistAsync()
        {
            UsersList = new ObservableCollection<UserViewModel>();
            var blockedUsersResponse = await _userService.GetAllBlockedUsersAsync();
            if (blockedUsersResponse.IsSuccess)
            {
                foreach (var userModel in blockedUsersResponse.Result)
                {
                    var userViewModel = userModel.ToUserViewModel();
                    userViewModel.RemoveCommand = SingleExecutionCommand.FromFunc(OnRemoveCommand);
                    UsersList.Add(userViewModel);
                }
            }
        }

        private async Task InitMutelistAsync()
        {
            UsersList = new ObservableCollection<UserViewModel>();
            var mutedUsersResponse = await _userService.GetAllMutedUsersAsync();
            foreach (var userModel in mutedUsersResponse.Result)
            {
                var userViewModel = userModel.ToUserViewModel();
                userViewModel.RemoveCommand = SingleExecutionCommand.FromFunc(OnRemoveCommand);
                UsersList.Add(userViewModel);
            }
        }

        private Task OnRemoveCommand(object parameter)
        {
            if (parameter is UserViewModel userViewModel)
            {
                userViewModel = parameter as UserViewModel;

                string partOfMessage = string.Empty;
                if (_isBlacklistPage)
                {
                    partOfMessage = Resources.Resource.from_the_Blacklist;
                }
                else if (_isMutelistPage)
                {
                    partOfMessage = Resources.Resource.from_the_mute;
                }

                var param = new DialogParameters();
                param.Add(Constants.DialogParameterKeys.TITLE, $"{Resources.Resource.Remove} {userViewModel.Name} {partOfMessage}");
                param.Add(Constants.DialogParameterKeys.OK_BUTTON_TEXT, Resources.Resource.Ok);
                param.Add(Constants.DialogParameterKeys.CANCEL_BUTTON_TEXT, Resources.Resource.Cancel);

                _dialogService.ShowDialog(nameof(AlertView), param, CloseDialogCallback);

                async void CloseDialogCallback(IDialogResult dialogResult)
                {
                    bool result = (bool)dialogResult?.Parameters[Constants.DialogParameterKeys.ACCEPT];
                    if (result)
                    {
                        if (_isBlacklistPage)
                        {
                            await _userService.RemoveFromBlacklistAsync(userViewModel.Id);
                            UsersList.Remove(userViewModel);
                        }
                        else if (_isMutelistPage)
                        {
                            await _userService.RemoveFromMutelistAsync(userViewModel.Id);
                            UsersList.Remove(userViewModel);
                        }
                    }
                }
            }

            return Task.CompletedTask;
        }

        #endregion

    }
}
