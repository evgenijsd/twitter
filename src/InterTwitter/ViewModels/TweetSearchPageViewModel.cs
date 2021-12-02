using InterTwitter.Enums;
using InterTwitter.Helpers;
using System.Threading.Tasks;
using System.Windows.Input;

namespace InterTwitter.ViewModels
{
    public class TweetSearchPageViewModel : BaseViewModel
    {
        public TweetSearchPageViewModel()
        {
        }

        #region --- Public properties ---

        private ETweetSearchState _tweetSearchState;
        public ETweetSearchState TweetSearchState
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

        private ICommand _startSearchCommand;
        public ICommand StartSearchCommand => _startSearchCommand ??= SingleExecutionCommand.FromFunc(StartSearchCommandTapAsync);

        private ICommand _stopSearchCommand;
        public ICommand StopSearchCommand => _stopSearchCommand ??= SingleExecutionCommand.FromFunc(StopSearchCommandTapAsync);

        #endregion

        #region --- Private helpers ---

        private Task StartSearchCommandTapAsync()
        {
            TweetSearchState = ETweetSearchState.Active;
            return Task.CompletedTask;
        }

        private Task StopSearchCommandTapAsync()
        {
            TweetSearchState = ETweetSearchState.NotActive;
            return Task.CompletedTask;
        }

        #endregion
    }
}
