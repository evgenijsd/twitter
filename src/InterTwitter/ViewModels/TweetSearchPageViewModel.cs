using InterTwitter.Enums;
using InterTwitter.Helpers;
using InterTwitter.Models;
using Prism.Navigation;
using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;

namespace InterTwitter.ViewModels
{
    public class TweetSearchPageViewModel : BaseViewModel
    {
        public TweetSearchPageViewModel()
            : base()
        {
            ProfileIcon = "pic_profile_small";
        }

        #region --- Public properties ---

        private string _profileIcon;
        public string ProfileIcon
        {
            get => _profileIcon;
            set => SetProperty(ref _profileIcon, value);
        }

        private string _searchQuery;
        public string SearchQuery
        {
            get => _searchQuery;
            set => SetProperty(ref _searchQuery, value);
        }

        private ObservableCollection<ThemeModel> _themeModels;
        public ObservableCollection<ThemeModel> ThemeModels
        {
            get => _themeModels;
            set => SetProperty(ref _themeModels, value);
        }

        private ESearchState _tweetSearchState;
        public ESearchState TweetSearchState
        {
            get => _tweetSearchState;
            set => SetProperty(ref _tweetSearchState, value);
        }

        private ETweetSearchResult _tweetSearchResult;
        public ETweetSearchResult TweetSearchResult
        {
            get => _tweetSearchResult;
            set => SetProperty(ref _tweetSearchResult, value);
        }

        private ICommand _profileIconTapCommand;
        public ICommand ProfileIconTapCommand => _profileIconTapCommand ??= SingleExecutionCommand.FromFunc(ProfileTapCommandTapAsync);

        private ICommand _startSearchTapCommand;
        public ICommand StartSearchTapCommand => _startSearchTapCommand ??= SingleExecutionCommand.FromFunc(StartSearchCommandTapAsync);

        private ICommand _stopSearchTapCommand;
        public ICommand StopSearchTapCommand => _stopSearchTapCommand ??= SingleExecutionCommand.FromFunc(StopSearchCommandTapAsync);

        #region --- testing ---

        private ICommand _tweetFoundCommand;
        public ICommand TweetFoundCommand => _tweetFoundCommand ??= SingleExecutionCommand.FromFunc(TweetFoundTapCommand);

        private ICommand _notTweetFoundCommand;
        public ICommand NoTweetFoundCommand => _notTweetFoundCommand ??= SingleExecutionCommand.FromFunc(NoTweetFoundTapCommand);

        private Task NoTweetFoundTapCommand()
        {
            TweetSearchResult = ETweetSearchResult.NoTweetsFound;
            return Task.CompletedTask;
        }

        private Task TweetFoundTapCommand()
        {
            TweetSearchResult = ETweetSearchResult.TweetsFound;
            return Task.CompletedTask;
        }

        #endregion

        #endregion

        #region --- Overrides ---

        public override Task InitializeAsync(INavigationParameters parameters)
        {
            ThemeModels = new ObservableCollection<ThemeModel>()
            {
                new ThemeModel()
                {
                    Title = "Title 1",
                    TweetsCount = 5,
                },
                new ThemeModel()
                {
                    Title = "Title 2",
                    TweetsCount = 15,
                },
                new ThemeModel()
                {
                    Title = "Title 3",
                    TweetsCount = 25,
                },
            };

            return base.InitializeAsync(parameters);
        }

        #endregion

        #region --- Private helpers ---

        private Task ProfileTapCommandTapAsync()
        {
            return Task.CompletedTask;
        }

        private Task StartSearchCommandTapAsync()
        {
            TweetSearchState = ESearchState.Active;
            return Task.CompletedTask;
        }

        private Task StopSearchCommandTapAsync()
        {
            TweetSearchState = ESearchState.NotActive;
            return Task.CompletedTask;
        }

        #endregion
    }
}
