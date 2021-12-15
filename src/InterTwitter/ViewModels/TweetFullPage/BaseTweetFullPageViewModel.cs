using InterTwitter.Helpers;
using InterTwitter.ViewModels.TweetViewModel;
using Prism.Navigation;
using System.Threading.Tasks;
using System.Windows.Input;

namespace InterTwitter.ViewModels.TweetFullPage
{
    public class BaseTweetFullPageViewModel : BaseViewModel
    {
        public BaseTweetFullPageViewModel(
            INavigationService navigationService)
            : base(navigationService)
        {
        }

        #region -- Public properties --

        private ICommand _saveCommand;
        public ICommand SaveCommand => _saveCommand ?? (_saveCommand = SingleExecutionCommand.FromFunc(OnSaveAsync));

        private ICommand _shareCommand;
        public ICommand ShareCommand => _shareCommand ?? (_shareCommand = SingleExecutionCommand.FromFunc(OnShareAsync));

        private ICommand _goBackCommand;
        public ICommand GoBackCommand => _goBackCommand ?? (_goBackCommand = SingleExecutionCommand.FromFunc(OnGoBackAsync));

        private BaseTweetViewModel _tweetViewModel;

        public BaseTweetViewModel TweetViewModel
        {
            get => _tweetViewModel;
            set => SetProperty(ref _tweetViewModel, value);
        }

        #endregion

        #region -- Overrides --

        public override void OnNavigatedTo(INavigationParameters parameters)
        {
            base.OnNavigatedTo(parameters);

            if (parameters.ContainsKey(nameof(BaseTweetViewModel)))
            {
                parameters.TryGetValue("BaseTweetModel", out _tweetViewModel);
            }
        }
        #endregion

        #region -- Private helpers --
        private Task OnSaveAsync()
        {
            return Task.CompletedTask;
        }

        protected virtual Task OnShareAsync()
        {
            return Task.CompletedTask;
        }

        protected virtual Task OnGoBackAsync()
        {
            return NavigationService.GoBackAsync();
        }

        #endregion
    }
}
