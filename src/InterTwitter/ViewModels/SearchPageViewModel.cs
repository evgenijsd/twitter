using InterTwitter.Enums;
using InterTwitter.Extensions;
using InterTwitter.Helpers;
using InterTwitter.Models;
using InterTwitter.Models.TweetViewModel;
using InterTwitter.Services;
using InterTwitter.Services.HashtagManager;
using Prism.Navigation;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.CommunityToolkit.Helpers;
using Xamarin.Forms;

namespace InterTwitter.ViewModels
{
    public class SearchPageViewModel : BaseTabViewModel
    {
        private readonly ITweetService _tweetService;
        private IHashtagManager _hashtagManager;

        public SearchPageViewModel(
            INavigationService navigationService,
            ITweetService tweetService,
            IHashtagManager hashtagManager)
            : base(navigationService)
        {
            _tweetService = tweetService;
            _hashtagManager = hashtagManager;

            Tweets = new ObservableCollection<BaseTweetViewModel>();
            Hashtags = new ObservableCollection<HashtagModel>();

            IconPath = Prism.PrismApplicationBase.Current.Resources["ic_search_gray"] as ImageSource;
            AvatarIcon = "pic_profile_small";

            TestText = "laborum #morning #AMAs onsequuntur #coffeeTime laborum numquam #teaTime";

            Keywords = new List<string>()
            {
                "#AMAs",
                "#NoNuanceNovember",
                "#blockchain",
                "#coffeeTime",
                "#teaTime",
                "laborum",
                "#morning",
                "nemo",
                "sed",
                "odit",
                "eaque",
                "um",
            };

            QueryString = "#am # amas #amas masd ma";
        }

        #region -- Public Properties --

        private string _testText;
        public string TestText
        {
            get => _testText;
            set => SetProperty(ref _testText, value);
        }

        private List<string> _keywords;
        public List<string> Keywords
        {
            get => _keywords;
            set => SetProperty(ref _keywords, value);
        }

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
        public string NoResultsMessage
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

        private ObservableCollection<HashtagModel> _hashtags;
        public ObservableCollection<HashtagModel> Hashtags
        {
            get => _hashtags;
            set => SetProperty(ref _hashtags, value);
        }

        private ObservableCollection<BaseTweetViewModel> _tweets;
        public ObservableCollection<BaseTweetViewModel> Tweets
        {
            get => _tweets;
            set => SetProperty(ref _tweets, value);
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

        private ICommand _startTweetsSearchTapCommand;
        public ICommand StartTweetsSearchTapCommand => _startTweetsSearchTapCommand ??= SingleExecutionCommand.FromFunc(OnStartTweetsSearchCommandTapAsync);

        private ICommand _backToHashtagsTapCommand;
        public ICommand BackToHashtagsTapCommand => _backToHashtagsTapCommand ??= SingleExecutionCommand.FromFunc(OnBackToHashTagsCommandTapAsync);

        private ICommand _hashtagTapCommand;
        public ICommand HashtagTapCommand => _hashtagTapCommand ??= SingleExecutionCommand.FromFunc(OnHashtagTapCommandAsync);

        private ICommand _openFlyoutCommand;
        public ICommand OpenFlyoutCommandAsync => _openFlyoutCommand ??= SingleExecutionCommand.FromFunc(OnOpenFlyoutCommandAsync);

        #endregion

        #region -- Overrides --

        public override async void OnNavigatedTo(INavigationParameters parameters)
        {
            await LoadHashtagsAsync();

            base.OnNavigatedTo(parameters);
        }

        public override void OnNavigatedFrom(INavigationParameters parameters)
        {
            ResetSearchData();

            base.OnNavigatedFrom(parameters);
        }

        public override void OnAppearing()
        {
            IconPath = Prism.PrismApplicationBase.Current.Resources["ic_search_blue"] as ImageSource;
        }

        public override void OnDisappearing()
        {
            IconPath = Prism.PrismApplicationBase.Current.Resources["ic_search_gray"] as ImageSource;
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

        #endregion

        #region -- Pulic helpers --

        public void ResetSearchData()
        {
            Tweets.Clear();
            TweetsSearchState = ESearchState.NotActive;
            QueryString = string.Empty;
            NoResultsMessage = string.Empty;
        }

        #endregion

        #region -- Private helpers --

        private async Task LoadHashtagsAsync()
        {
            var result = await _hashtagManager.GetPopularHashtags(5);

            if (result.IsSuccess)
            {
                Hashtags = new ObservableCollection<HashtagModel>(result.Result);
            }
        }

        private async Task InitTweetsForDisplayingAsync(IEnumerable<TweetModel> tweets)
        {
            var tweetViewModels = new List<BaseTweetViewModel>(
                tweets.Select(x => x.Media == EAttachedMediaType.Photos
                    || x.Media == EAttachedMediaType.Gif
                    ? x.ToImagesTweetViewModel()
                    : x.ToBaseTweetViewModel()));

            foreach (var tweet in tweetViewModels)
            {
                var tweetAuthor = await _tweetService.GetAuthorAsync(tweet.UserId);

                if (tweetAuthor.IsSuccess)
                {
                    tweet.UserAvatar = tweetAuthor.Result.AvatarPath;
                    tweet.UserBackgroundImage = tweetAuthor.Result.BackgroundUserImagePath;
                    tweet.UserName = tweetAuthor.Result.Name;
                    tweet.Keywords = Keywords;
                }
            }

            Tweets = new ObservableCollection<BaseTweetViewModel>(tweetViewModels);
        }

        private Task OnOpenFlyoutCommandAsync()
        {
            MessagingCenter.Send(this, Constants.Messages.OPEN_SIDEBAR, true);

            return Task.CompletedTask;
        }

        private Task OnStartTweetsSearchCommandTapAsync()
        {
            TweetsSearch(QueryString);

            return Task.CompletedTask;
        }

        private Task OnHashtagTapCommandAsync()
        {
            QueryString = SelectedHashtag.Text;
            TweetsSearch(QueryString);

            return Task.CompletedTask;
        }

        private Task OnBackToHashTagsCommandTapAsync()
        {
            TweetsSearchState = ESearchState.NotActive;

            ResetSearchData();

            return Task.CompletedTask;
        }

        private async void TweetsSearch(string queryString)
        {
            TweetsSearchState = ESearchState.Active;

            if (string.IsNullOrWhiteSpace(queryString)
                || (!string.IsNullOrWhiteSpace(queryString)
                && queryString.FirstOrDefault() == '#'
                    ? queryString.Length < 3
                    : queryString.Length < 2))
            {
                TweetSearchResult = ESearchResult.NoResults;
                NoResultsMessage = LocalizationResourceManager.Current[Constants.TweetsSearch.INACCURATE_REQUEST];
            }
            else
            {
                Keywords = Constants.TweetsSearch.GetParsedKeysFromQuery(queryString);
                var result = await _tweetService.GetAllTweetsByHashtagsOrKeysAsync(Keywords);

                if (result.IsSuccess)
                {
                    TweetSearchResult = ESearchResult.Success;

                    await InitTweetsForDisplayingAsync(result.Result);
                }
                else
                {
                    TweetSearchResult = ESearchResult.NoResults;
                    NoResultsMessage = $"{LocalizationResourceManager.Current[Constants.TweetsSearch.NO_RESULTS_FOR]}\n\"{queryString}\"";
                }
            }
        }

        #endregion
    }
}
