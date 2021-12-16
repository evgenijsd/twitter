using InterTwitter.Enums;
using InterTwitter.Extensions;
using InterTwitter.Helpers;
using InterTwitter.Models;
using InterTwitter.Services;
using InterTwitter.ViewModels.TweetViewModel;
using InterTwitter.Views;
using InterTwitter.Views.TweetFullPage;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using static InterTwitter.Constants.Navigation;

namespace InterTwitter.ViewModels
{
    public class HomePageViewModel : BaseTabViewModel
    {
        private readonly ITweetService _tweetService;

        private bool _isFirstStart = true;

        public HomePageViewModel(
            ITweetService tweetService,
            INavigationService navigationService)
            : base(navigationService)
        {
            IconPath = Prism.PrismApplicationBase.Current.Resources["ic_home_gray"] as ImageSource;
            _tweetService = tweetService;
        }

        #region -- Public properties --

        private ICommand _openFlyoutCommandAsync;
        public ICommand OpenFlyoutCommandAsync => _openFlyoutCommandAsync ?? (_openFlyoutCommandAsync = SingleExecutionCommand.FromFunc(OnOpenFlyoutCommandAsync));

        private ICommand _addTweetCommandAsync;
        public ICommand AddTweetCommandAsync => _addTweetCommandAsync ?? (_addTweetCommandAsync = SingleExecutionCommand.FromFunc(OnAddTweetPageAsync));

        private ObservableCollection<BaseTweetViewModel> _tweets;
        public ObservableCollection<BaseTweetViewModel> Tweets
        {
            get => _tweets;
            set => SetProperty(ref _tweets, value);
        }

        #endregion

        #region -- Overrides --

        public override void OnNavigatedTo(INavigationParameters parameters)
        {
            base.OnNavigatedTo(parameters);
        }

        public override async void OnAppearing()
        {
            if (_isFirstStart)
            {
                await InitAsync();
            }

            _isFirstStart = true;

            IconPath = Prism.PrismApplicationBase.Current.Resources["ic_home_blue"] as ImageSource;
        }

        public override void OnDisappearing()
        {
            IconPath = Prism.PrismApplicationBase.Current.Resources["ic_home_gray"] as ImageSource;
        }

        #endregion

        #region -- Private helpers --

        private async Task InitAsync()
        {
            var getTweetResult = await _tweetService.GetAllTweetsAsync();

            if (getTweetResult.IsSuccess)
            {
                var tweetViewModels = new List<BaseTweetViewModel>(getTweetResult.Result.Select(x => x.Media == EAttachedMediaType.Photos || x.Media == EAttachedMediaType.Gif ? x.ToImagesTweetViewModel() : x.ToBaseTweetViewModel()));

                var likeTweetCommand = SingleExecutionCommand.FromFunc<BaseTweetViewModel>(OnLikeTweetCommandAsync);
                var openTweetCommand = SingleExecutionCommand.FromFunc<BaseTweetViewModel>(OnOpenTweetCommandAsync);
                var bookmarkTweetCommand = SingleExecutionCommand.FromFunc<BaseTweetViewModel>(OnBookmarkTweetCommandAsync);
                var moveToAuthorCommand = SingleExecutionCommand.FromFunc<BaseTweetViewModel>(OnMoveToAuthorCommandAsync);
                var moveToImagesGalleryCommand = SingleExecutionCommand.FromFunc(OnMoveToImagesGallaryCommandAsync);
                var moveToVideoGalleryCommand = SingleExecutionCommand.FromFunc<BaseTweetViewModel>(OnMoveToVideoGallaryCommandAsync);

                foreach (var tweetVM in tweetViewModels)
                {
                    tweetVM.LikeCommand = likeTweetCommand;
                    tweetVM.BookmarkCommand = bookmarkTweetCommand;
                    tweetVM.MoveToImagesGalleryCommand = moveToImagesGalleryCommand;
                    tweetVM.MoveToVideoGalleryCommand = moveToVideoGalleryCommand;
                    tweetVM.MoveToAuthorCommand = moveToAuthorCommand;

                    var tweetAuthor = await _tweetService.GetAuthorAsync(tweetVM.UserId);

                    if (tweetAuthor.IsSuccess)
                    {
                        tweetVM.UserAvatar = tweetAuthor.Result.AvatarPath;
                        tweetVM.UserBackgroundImage = tweetAuthor.Result.BackgroundUserImagePath;
                        tweetVM.UserName = tweetAuthor.Result.Name;
                    }
                }

                Tweets = new ObservableCollection<BaseTweetViewModel>(tweetViewModels);
            }
        }

        private Task OnBookmarkTweetCommandAsync(BaseTweetViewModel vm)
        {
            vm.IsBookmarked = !vm.IsBookmarked;

            return Task.CompletedTask;
        }

        private Task OnOpenTweetCommandAsync(BaseTweetViewModel vm)
        {
            var navParams = new NavigationParameters();

            navParams.Add(TWEET, vm.ToTweetModel());

            return NavigationService.NavigateAsync(nameof(ImagesFullPage), navParams);
        }

        private Task OnMoveToImagesGallaryCommandAsync(object vm)
        {
            var r = vm as BaseTweetViewModel;
            //var navParams = new NavigationParameters();

            //navParams.Add(Images_Gallery, vm.ToTweetModel());

            //return NavigationService.NavigateAsync(nameof(ImagesGalleryPage), navParams);
            return Task.CompletedTask;
        }

        private Task OnMoveToVideoGallaryCommandAsync(BaseTweetViewModel arg)
        {
            //var navParams = new NavigationParameters();

            //navParams.Add(Video_Gallery, vm.ToTweetModel());

            //return NavigationService.NavigateAsync(nameof(VideoGalleryPage), navParams);
            return Task.CompletedTask;
        }

        private Task OnMoveToAuthorCommandAsync(BaseTweetViewModel vm)
        {
            return Task.CompletedTask;
        }

        private Task OnLikeTweetCommandAsync(BaseTweetViewModel vm)
        {
            vm.IsTweetLiked = !vm.IsTweetLiked;

            return Task.CompletedTask;
        }

        private Task OnAddTweetPageAsync()
        {
            return Task.CompletedTask;
        }

        private Task OnOpenFlyoutCommandAsync()
        {
            MessagingCenter.Send(this, Constants.Messages.OPEN_SIDEBAR, true);
            MessagingCenter.Send(this, Constants.Messages.TAB_CHANGE, typeof(HomePage));
            return Task.CompletedTask;
        }

        #endregion
    }
}
