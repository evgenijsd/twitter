using InterTwitter.Helpers;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace InterTwitter.Models.TweetViewModel
{
    public class BaseTweetViewModel : BindableBase
    {
        public BaseTweetViewModel(UserModel userModel, TweetModel tweetModel)
        {
            _userModel = userModel;
            _tweetModel = tweetModel;
        }
        #region -- Public properties --

        private UserModel _userModel;
        public UserModel UserModel
        {
            get => _userModel;
            set => SetProperty(ref _userModel, value);
        }

        private TweetModel _tweetModel;
        public TweetModel TweetModel
        {
            get => _tweetModel;
            set => SetProperty(ref _tweetModel, value);
        }

        private int _likesNumber = 123;
        public int LikesNumber
        {
            get => _likesNumber;
            set => SetProperty(ref _likesNumber, value);
        }

        private bool _IsTweetLiked;
        public bool IsTweekLiked
        {
            get => _IsTweetLiked;
            set => SetProperty(ref _IsTweetLiked, value);
        }

        private bool _isBookmarked;
        public bool IsBookmarked
        {
            get => _isBookmarked;
            set => SetProperty(ref _isBookmarked, value, nameof(IsBookmarked));
        }

        private Enum _tweetType;
        public Enum TweetType
        {
            get => _tweetType;
            set => SetProperty(ref _tweetType, value);
        }

        private ICommand _likeTweetCommand;
        public ICommand LikeTweetCommand => _likeTweetCommand ?? (_likeTweetCommand = SingleExecutionCommand.FromFunc<ImagesTweetViewModel>(OnLikeAsync));

        private ICommand _openTweetCommand;
        public ICommand OpenTweetCommand => _openTweetCommand ?? (_openTweetCommand = SingleExecutionCommand.FromFunc<ImagesTweetViewModel>(OnOpenTweetAsync));

        private ICommand _markTweetCommand;
        public ICommand MarkTweetCommand => _markTweetCommand ?? (_markTweetCommand = SingleExecutionCommand.FromFunc<BaseTweetViewModel>(OnMarkAsync));

        private ICommand _moveToProfileCommand;
        public ICommand MoveToProfileCommand => _moveToProfileCommand ?? (_moveToProfileCommand = SingleExecutionCommand.FromFunc<BaseTweetViewModel>(OnGoToProfileAsync));

        private DateTime _CreationTime;
        public DateTime CreationTime
        {
            get => _CreationTime;
            set => SetProperty(ref _CreationTime, value);
        }
        #endregion

        #region -- Private helpers --

        private Task OnLikeAsync(BaseTweetViewModel tweet)
        {
            IsTweekLiked = !IsTweekLiked;
            return Task.CompletedTask;
        }

        private Task OnOpenTweetAsync(BaseTweetViewModel arg)
        {
            return Task.CompletedTask;
        }

        private Task OnMarkAsync(BaseTweetViewModel tweet)
        {
            IsBookmarked = !IsBookmarked;
            return Task.CompletedTask;
        }

        private Task OnGoToProfileAsync(BaseTweetViewModel arg)
        {
            return Task.CompletedTask;
        }

        #endregion

    }
}
