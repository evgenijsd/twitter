using InterTwitter.Helpers;
using InterTwitter.Extensions;
using InterTwitter.Models;
using InterTwitter.Services.Settings;
using InterTwitter.Services.UserService;
using Prism.Navigation;
using Prism.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace InterTwitter.ViewModels
{
    public class BlacklistPageViewModel : BaseViewModel
    {
        private readonly ISettingsManager _settingsManager;
        private readonly IUserService _userService;
        private readonly IPageDialogService _dialogService;

        private bool _isBlacklistPage;
        private bool _isMutelistPage;
        private UserModel _currentUser;

        public BlacklistPageViewModel(INavigationService navigationService, ISettingsManager settingsManager, IUserService userService, IPageDialogService dialogService)
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
           // UsersList = new ObservableCollection<UserViewModel>(_userService.GetAllUsersAsync().Result.Result.Select(x => x.ToUserViewModel()));
            if (_isBlacklistPage = parameters.ContainsKey(Constants.NavigationKeys.BLACKLIST))
            {
                Title = "Blacklist";
                _currentUser = parameters[Constants.NavigationKeys.BLACKLIST] as UserModel;

                await InitBlacklistAsync();
            }
            else if (_isMutelistPage = parameters.ContainsKey(Constants.NavigationKeys.MUTELIST))
            {
                Title = "Mute";
                _currentUser = parameters[Constants.NavigationKeys.MUTELIST] as UserModel;

                await InitMutelistAsync();
            }
        }

        #endregion

        #region -- Private Helpers --

        private async Task InitBlacklistAsync()
        {
            UsersList = new ObservableCollection<UserViewModel>();
            var list = _userService.GetAllBlockedUsersAsync(_currentUser.Id).Result.Result;
            foreach (var userModel in list)
            {
                var userViewModel = userModel.ToUserViewModel();
                userViewModel.RemoveCommand = SingleExecutionCommand.FromFunc(OnRemoveCommand);
                UsersList.Add(userViewModel);
            }
        }

        private async Task InitMutelistAsync()
        {
            UsersList = new ObservableCollection<UserViewModel>();
            var list = _userService.GetAllMutedUsersAsync(_currentUser.Id).Result.Result;
            foreach (var userModel in list)
            {
                var userViewModel = userModel.ToUserViewModel();
                userViewModel.RemoveCommand = SingleExecutionCommand.FromFunc(OnRemoveCommand);
                UsersList.Add(userViewModel);
            }
        }

        private async Task OnRemoveCommand(object parameter)
        {
            if (parameter != null)
            {
                var userViewModel = parameter as UserViewModel;

                if (_isBlacklistPage)
                {
                    bool result = await _dialogService.DisplayAlertAsync($"Remove {userViewModel.Name} from the blacklist?", string.Empty, "Ok", "Cancel");
                    if (result)
                    {
                       await _userService.RemoveFromBlacklistAsync(_currentUser.Id, userViewModel.Id);
                       UsersList.Remove(userViewModel);
                    }
            }
                else if (_isMutelistPage)
                {
                    bool result = await _dialogService.DisplayAlertAsync($"Remove {userViewModel.Name} from the mute?", string.Empty, "Ok", "Cancel");
                    if (result)
                    {
                        await _userService.RemoveFromMutelistAsync(_currentUser.Id, userViewModel.Id);
                        UsersList.Remove(userViewModel);
                    }
                }
            }
        }

        #endregion

    }
}
