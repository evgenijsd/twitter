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

        private ETweetSearchState _tweetSearchPageState;
        public ETweetSearchState TweetSearchPageState
        {
            get => _tweetSearchPageState;
            set => SetProperty(ref _tweetSearchPageState, value);
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
            TweetSearchPageState = ETweetSearchState.Active;
            return Task.CompletedTask;
        }

        private Task StopSearchCommandTapAsync()
        {
            TweetSearchPageState = ETweetSearchState.NotActive;
            return Task.CompletedTask;
        }

        #endregion
    }
}
