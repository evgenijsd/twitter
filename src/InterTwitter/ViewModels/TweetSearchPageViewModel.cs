using InterTwitter.Enums;
using InterTwitter.Helpers;
using InterTwitter.Models;
using Prism.Navigation;
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
            AvatarIcon = "pic_profile_small";
        }

        #region --- Public properties ---

        private string _avatarIcon;
        public string AvatarIcon
        {
            get => _avatarIcon;
            set => SetProperty(ref _avatarIcon, value);
        }

        private string _searchQuery;
        public string SearchQuery
        {
            get => _searchQuery;
            set => SetProperty(ref _searchQuery, value);
        }

        private string _searchQueryWithNoResults;
        public string SearchQueryWithNoResults
        {
            get => _searchQueryWithNoResults;
            set => SetProperty(ref _searchQueryWithNoResults, value);
        }

        private ThemeModel _selectedTweetsTheme;
        public ThemeModel SelectedTweetsTheme
        {
            get => _selectedTweetsTheme;
            set => SetProperty(ref _selectedTweetsTheme, value);
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

        private ESearchResult _tweetSearchResult;
        public ESearchResult TweetSearchResult
        {
            get => _tweetSearchResult;
            set => SetProperty(ref _tweetSearchResult, value);
        }

        private ICommand _avatarIconTapCommand;
        public ICommand AvatarIconTapCommand => _avatarIconTapCommand ??= SingleExecutionCommand.FromFunc(ProfileTapCommandTapAsync);

        private ICommand _startSearchTapCommand;
        public ICommand StartSearchTapCommand => _startSearchTapCommand ??= SingleExecutionCommand.FromFunc(StartSearchCommandTapAsync);

        private ICommand _stopSearchTapCommand;
        public ICommand StopSearchTapCommand => _stopSearchTapCommand ??= SingleExecutionCommand.FromFunc(StopSearchCommandTapAsync);

        private ICommand _tweetsThemeTapCommand;
        public ICommand TeetsThemeTapCommand => _tweetsThemeTapCommand ??= SingleExecutionCommand.FromFunc(TweetsThemeCommandTapAsync);

        #endregion

        #region --- Overrides ---

        public override Task InitializeAsync(INavigationParameters parameters)
        {
            ThemeModels = new ObservableCollection<ThemeModel>()
            {
                new ThemeModel()
                {
                    Title = "#AMAs",
                    TweetsCount = 135,
                },
                new ThemeModel()
                {
                    Title = "#blockchain",
                    TweetsCount = 55,
                },
                new ThemeModel()
                {
                    Title = "#NoNuanceNovember",
                    TweetsCount = 25,
                },
            };

            return base.InitializeAsync(parameters);
        }

        #endregion

        #region --- Private helpers ---

        private Task ProfileTapCommandTapAsync()
        {
            /* TEMP */
            switch (TweetSearchResult)
            {
                case ESearchResult.NoResults:
                    TweetSearchResult = ESearchResult.Success;
                    break;
                case ESearchResult.Success:
                    TweetSearchResult = ESearchResult.NoResults;
                    break;
                default:
                    break;
            }

            return Task.CompletedTask;
        }

        private Task StartSearchCommandTapAsync()
        {
            TweetSearchState = ESearchState.Active;

            switch (TweetSearchResult)
            {
                case ESearchResult.NoResults:
                    SearchQueryWithNoResults = SearchQuery;
                    break;

                case ESearchResult.Success:
                    SearchQueryWithNoResults = string.Empty;

                    /*TODO: filling tweets list*/
                    break;
            }

            return Task.CompletedTask;
        }

        private Task StopSearchCommandTapAsync()
        {
            SearchQueryWithNoResults = string.Empty;
            TweetSearchState = ESearchState.NotActive;

            return Task.CompletedTask;
        }

        private Task TweetsThemeCommandTapAsync(object obj)
        {
            SearchQuery = SelectedTweetsTheme.Title;
            TweetSearchState = ESearchState.Active;

            return Task.CompletedTask;
        }

        #endregion
    }
}
