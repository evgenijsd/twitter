using InterTwitter.Enums;
using InterTwitter.Extensions;
using InterTwitter.Helpers;
using InterTwitter.Models;
using InterTwitter.Models.TweetViewModel;
using InterTwitter.Services.HashtagManager;
using InterTwitter.Services.TweetService;
using InterTwitter.Views;
using Prism.Navigation;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.CommunityToolkit.Helpers;
using Xamarin.Forms;
using static InterTwitter.Constants;

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
            AvatarIcon = "pic_profile_small";

            /*TweetSearchResult = ESearchResult.Success;*/

            IconPath = Prism.PrismApplicationBase.Current.Resources["ic_search_gray"] as ImageSource;
        }

        #region -- Public Properties --

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

        private ObservableCollection<HashtagModel> _hashtagModels;
        public ObservableCollection<HashtagModel> HashtagModels
        {
            get => _hashtagModels;
            set => SetProperty(ref _hashtagModels, value);
        }

        private ObservableCollection<BaseTweetViewModel> _foundTweets;
        public ObservableCollection<BaseTweetViewModel> FoundTweets
        {
            get => _foundTweets;
            set => SetProperty(ref _foundTweets, value);
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

        public override async void OnAppearing()
        {
            await LoadAsync();

            var result = await _hashtagManager.GetPopularHashtags(5);

            if (result.IsSuccess)
            {
                HashtagModels = new ObservableCollection<HashtagModel>(result.Result);
            }

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

        #region --- Private Helpers ---

        private async Task LoadAsync()
        {
            var result = await _tweetService.GetAllTweetsAsync();

            if (result.IsSuccess)
            {
                var tweetViewModels = new List<BaseTweetViewModel>(
                    result.Result.Select(x => x.Media == ETypeAttachedMedia.Photos
                        || x.Media == ETypeAttachedMedia.Gif
                        ? x.ToImagesTweetViewModel()
                        : x.ToBaseTweetViewModel()));

                foreach (var tweet in tweetViewModels)
                {
                    var tweetAuthor = await _tweetService.GetUserAsync(tweet.UserId);

                    if (tweetAuthor.IsSuccess)
                    {
                        tweet.UserAvatar = tweetAuthor.Result.AvatarPath;
                        tweet.UserBackgroundImage = tweetAuthor.Result.BackgroundUserImagePath;
                        tweet.UserName = tweetAuthor.Result.Name;
                    }
                }

                FoundTweets = new ObservableCollection<BaseTweetViewModel>(tweetViewModels);
            }
        }

        private Task OnOpenFlyoutCommandAsync()
        {
            MessagingCenter.Send(this, Constants.Messages.OPEN_SIDEBAR, true);
            MessagingCenter.Send(this, Constants.Messages.TAB_CHANGE, typeof(SearchPage));
            return Task.CompletedTask;
        }

        private Task OnStartTweetsSearchCommandTapAsync()
        {
            TweetsSearch();

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

        private async void TweetsSearch()
        {
            TweetsSearchState = ESearchState.Active;

            if (QueryString.Length <= 2)
            {
                NoResultsMessage = LocalizationResourceManager.Current[SearchRequestMessages.INACCURATE_REQUEST];
            }
            else
            {
                var result = await _tweetService.GetAllTweetsByHashtagsOrKeysAsync(QueryString);

                if (result.IsSuccess)
                {
                    //FoundTweets = (ObservableCollection<BaseTweetViewModel>)result.Result;
                }

                /* TO DO: calling of the tweets search */

                switch (TweetSearchResult)
                {
                    case ESearchResult.NoResults:
                        NoResultsMessage = $"{LocalizationResourceManager.Current[SearchRequestMessages.NO_RESULTS_FOR]}\n\"{QueryString}\"";
                        break;
                    case ESearchResult.Success:
                        NoResultsMessage = string.Empty;
                        break;
                }
            }
        }

        private void ResetSearchData()
        {
            FoundTweets.Clear();
            QueryString = string.Empty;
            NoResultsMessage = string.Empty;
        }

        #endregion

    }
}
