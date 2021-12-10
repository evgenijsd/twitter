using InterTwitter.Enums;
using InterTwitter.Helpers;
using InterTwitter.Models;
using InterTwitter.Services.HashtagManager;
using InterTwitter.Views;
using Prism.Navigation;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace InterTwitter.ViewModels
{
    public class SearchPageViewModel : BaseTabViewModel
    {
        private IHashtagManager _hashtagManager;

        public SearchPageViewModel(
            INavigationService navigationService,
            IHashtagManager hashtagManager)
            : base(navigationService)
        {
            _hashtagManager = hashtagManager;
            AvatarIcon = "pic_profile_small";

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

        private ICommand _startTweetsSearchTapCommand;
        public ICommand StartTweetsSearchTapCommand => _startTweetsSearchTapCommand ??= SingleExecutionCommand.FromFunc(OnStartTweetsSearchCommandTapAsync);

        private ICommand _backToHashtagsTapCommand;
        public ICommand BackToHashtagsTapCommand => _backToHashtagsTapCommand ??= SingleExecutionCommand.FromFunc(OnBackToHashTagsCommandTapAsync);

        private ICommand _hashtagTapCommand;
        public ICommand HashtagTapCommand => _hashtagTapCommand ??= SingleExecutionCommand.FromFunc(OnHashtagTapCommandAsync);

        private ICommand _openFlyoutCommand;
        public ICommand OpenFlyoutCommandAsync => _openFlyoutCommand ??= SingleExecutionCommand.FromFunc(OnOpenFlyoutCommandAsync);

        #endregion

        #region --- Overrides ---

        public override async void OnAppearing()
        {
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

        private Task OnOpenFlyoutCommandAsync()
        {
            MessagingCenter.Send(this, Constants.Messages.OPEN_SIDEBAR, true);
            MessagingCenter.Send(this, Constants.Messages.TAB_CHANGE, typeof(SearchPage));
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
