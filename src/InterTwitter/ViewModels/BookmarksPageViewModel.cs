using InterTwitter.Enums;
using InterTwitter.Extensions;
using InterTwitter.Helpers;
using InterTwitter.Models.TweetViewModel;
using InterTwitter.Services;
using InterTwitter.Services.Settings;
using Prism.Navigation;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;

namespace InterTwitter.ViewModels
{
    public class BookmarksPageViewModel : BasePageViewModel
    {
        private readonly ITweetService _tweetService;

        public BookmarksPageViewModel(
            ISettingsManager settingManager,
            ITweetService tweetService)
        {
            _tweetService = tweetService;
        }

        #region -- Public properties --

        private bool _IsTweetLiked;

        public bool IsTweetLiked
        {
            get => _IsTweetLiked;
            set => SetProperty(ref _IsTweetLiked, value);
        }

        private ObservableCollection<BaseTweetViewModel> _tweets;

        public ObservableCollection<BaseTweetViewModel> Tweets
        {
            get => _tweets;
            set => SetProperty(ref _tweets, value);
        }

        private ICommand _DeleteCommand;
        public ICommand DeleteCommand => _DeleteCommand ??= SingleExecutionCommand.FromFunc<object>(OnDeleteCommandAsync);

        #endregion

        #region -- Overrides --

        public override async void OnNavigatedTo(INavigationParameters parameters)
        {
            await InitAsync();
        }

        #endregion

        #region -- Private helpers --

        private async Task InitAsync()
        {
            var getTweetResult = await _tweetService.GetAllTweetsAsync();

            if (getTweetResult.IsSuccess)
            {
                var tweetViewModels = new List<BaseTweetViewModel>(getTweetResult.Result.Select(x => x.Media == ETypeAttachedMedia.Photos || x.Media == ETypeAttachedMedia.Gif ? x.ToImagesTweetViewModel() : x.ToBaseTweetViewModel()));

                foreach (var tweet in tweetViewModels)
                {
                    var tweetAuthor = await _tweetService.GetUserAsync(tweet.UserId);

                    if (tweetAuthor.IsSuccess)
                    {
                        tweet.UserAvatar = tweetAuthor.Result.AvatarPath;
                        tweet.UserBackgroundImage = tweetAuthor.Result.BackgroundUserImagePath;
                        tweet.UserName = tweetAuthor.Result.Name;
                    }

                    tweet.DeleteBookmarkCommand = DeleteCommand;
                }

                Tweets = new ObservableCollection<BaseTweetViewModel>(tweetViewModels/*.Where(x => x.UserId == 1)*/);
            }
        }

        private Task OnDeleteCommandAsync(object args)
        {
            if (args != null)
            {
                _IsTweetLiked = !_IsTweetLiked;
            }

            /*{
   var confirmConfig = new ConfirmConfig()
   {
       Message = "Delete pin",
       OkText = "Delete",
       CancelText = "Cancel"
   };
   var confirm = await UserDialogs.Instance.ConfirmAsync(confirmConfig);
   if (confirm)
   {
       await _mapService.DeletePinAsync(PinSearch, args);
   }
}
PinView pinv = args as PinView;
PinModel pindel = pinv.ToPinModel();*/
            return Task.CompletedTask;
        }

        #endregion
    }
}
