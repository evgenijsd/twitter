using InterTwitter.Enums;
using InterTwitter.Helpers;
using InterTwitter.Models;
using InterTwitter.Services.HashtagManager;
using Prism.Navigation;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Windows.Input;

namespace InterTwitter.ViewModels
{
    public class TweetSearchPageViewModel : BaseViewModel
    {
        private INavigationService _navigationService;
        private IHashtagManager _hashtagManager;

        public TweetSearchPageViewModel(
            INavigationService navigationService,
            IHashtagManager hashtagManager)
            : base(navigationService)
        {
            _navigationService = navigationService;
            _hashtagManager = hashtagManager;
        }

        #region --- Public properties ---

        private string _avatarIcon;
        public string AvatarIcon
        {
            get => _avatarIcon;
            set => SetProperty(ref _avatarIcon, value);
        }

        private string _queryString;
        public string QueryString
        {
            get => _queryString;
            set => SetProperty(ref _queryString, value);
        }

        private string _queryStringWithNoResults;
        public string QueryStringWithNoResults
        {
            get => _queryStringWithNoResults;
            set => SetProperty(ref _queryStringWithNoResults, value);
        }

        private HashtagModel _selectedHashtag;
        public HashtagModel SelectedHashtag
        {
            get => _selectedHashtag;
            set => SetProperty(ref _selectedHashtag, value);
        }

        private ObservableCollection<HashtagModel> _hashtagModels;
        public ObservableCollection<HashtagModel> HashtagModels
        {
            get => _hashtagModels;
            set => SetProperty(ref _hashtagModels, value);
        }

        private ESearchState _tweetsSearchState;
        public ESearchState TweetsSearchState
        {
            get => _tweetsSearchState;
            set => SetProperty(ref _tweetsSearchState, value);
        }

        private ESearchResult _tweetSearchResult;
        public ESearchResult TweetSearchResult
        {
            get => _tweetSearchResult;
            set => SetProperty(ref _tweetSearchResult, value);
        }

        private ICommand _avatarIconTapCommand;
        public ICommand AvatarIconTapCommand => _avatarIconTapCommand ??= SingleExecutionCommand.FromFunc(OnAvatarIconTapCommandTapAsync);

        private ICommand _startTweetsSearchTapCommand;
        public ICommand StartTweetsSearchTapCommand => _startTweetsSearchTapCommand ??= SingleExecutionCommand.FromFunc(OnStartTweetsSearchCommandTapAsync);

        private ICommand _backToHashtagsTapCommand;
        public ICommand BackToHashtagsTapCommand => _backToHashtagsTapCommand ??= SingleExecutionCommand.FromFunc(OnBackToHashTagsCommandTapAsync);

        private ICommand _hashtagTapCommand;
        public ICommand HashtagTapCommand => _hashtagTapCommand ??= SingleExecutionCommand.FromFunc(OnHashtagTapCommandAsync);

        #endregion

        #region --- Overrides ---

        public override async Task InitializeAsync(INavigationParameters parameters)
        {
            var result = await _hashtagManager.GetPopularHashtags(5);

            if (result.IsSuccess)
            {
                HashtagModels = new ObservableCollection<HashtagModel>(result.Result);
            }
        }

        private void TestInit()
        {
            HashtagModels = new ObservableCollection<HashtagModel>()
            {
                new HashtagModel()
                {
                    Text = "#AMAs",
                    TweetsCount = 135,
                },
                new HashtagModel()
                {
                    Text = "#blockchain",
                    TweetsCount = 55,
                },
                new HashtagModel()
                {
                    Text = "#NoNuanceNovember",
                    TweetsCount = 25,
                },
                new HashtagModel()
                {
                    Text = "#NoNuanceNovember_2",
                    TweetsCount = 25,
                },
                new HashtagModel()
                {
                    Text = "#NoNuanceNovember_3",
                    TweetsCount = 25,
                },
                new HashtagModel()
                {
                    Text = "#NoNuanceNovember_4",
                    TweetsCount = 25,
                },
                new HashtagModel()
                {
                    Text = "#NoNuanceNovember_5",
                    TweetsCount = 25,
                },
                new HashtagModel()
                {
                    Text = "#NoNuanceNovember_6",
                    TweetsCount = 25,
                },
                new HashtagModel()
                {
                    Text = "#NoNuanceNovember_7",
                    TweetsCount = 25,
                },
                new HashtagModel()
                {
                    Text = "#NoNuanceNovember_8",
                    TweetsCount = 25,
                },
                new HashtagModel()
                {
                    Text = "#NoNuanceNovember_9",
                    TweetsCount = 25,
                },
                new HashtagModel()
                {
                    Text = "#NoNuanceNovember_10",
                    TweetsCount = 25,
                },
            };
        }

        protected override void OnPropertyChanged(PropertyChangedEventArgs args)
        {
            base.OnPropertyChanged(args);

            switch (args.PropertyName)
            {
                case nameof(TweetsSearchState):
                    if (TweetsSearchState == ESearchState.NotActive)
                    {
                        ResetSearchData();
                    }

                    break;
            }
        }

        public override void OnNavigatedFrom(INavigationParameters parameters)
        {
            base.OnNavigatedFrom(parameters);

            TweetsSearchState = ESearchState.NotActive;

            HashtagModels.Clear();
            ResetSearchData();
        }

        public override void OnNavigatedTo(INavigationParameters parameters)
        {
            base.OnNavigatedTo(parameters);

            AvatarIcon = "pic_profile_small";

            /* TO DO: load hashtags */
        }

        #endregion

        #region --- Private helpers ---

        private Task OnAvatarIconTapCommandTapAsync()
        {
            return Task.CompletedTask;
        }

        private Task OnStartTweetsSearchCommandTapAsync()
        {
            if (QueryString.Length > 1)
            {
                TweetsSearch();
            }

            return Task.CompletedTask;
        }

        private Task OnHashtagTapCommandAsync()
        {
            QueryString = SelectedHashtag.Text;
            TweetsSearch();

            return Task.CompletedTask;
        }

        private Task OnBackToHashTagsCommandTapAsync()
        {
            TweetsSearchState = ESearchState.NotActive;

            ResetSearchData();

            return Task.CompletedTask;
        }

        private void TweetsSearch()
        {
            TweetsSearchState = ESearchState.Active;

            /* TO DO: calling of the tweets search */

            switch (TweetSearchResult)
            {
                case ESearchResult.NoResults:
                    QueryStringWithNoResults = QueryString;
                    break;
                case ESearchResult.Success:
                    QueryStringWithNoResults = string.Empty;
                    break;
            }
        }

        private void ResetSearchData()
        {
            QueryString = string.Empty;
            QueryStringWithNoResults = string.Empty;

            /* TO DO: clear found tweets */
        }

        #endregion
    }
}
