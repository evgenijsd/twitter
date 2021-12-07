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
        #region -- Public properties --

        private int _id;
        public int Id
        {
            get => _id;
            set => SetProperty(ref _id, value);
        }

        private int _likesNumber = 123;
        public int LikesNumber
        {
            get => _likesNumber;
            set => SetProperty(ref _likesNumber, value);
        }

        private int _userId;
        public int UserId
        {
            get => _userId;
            set => SetProperty(ref _userId, value);
        }

        private string _userName;
        public string UserName
        {
            get => _userName;
            set => SetProperty(ref _userName, value);
        }

        private string _userAvatar;
        public string UserAvatar
        {
            get => _userAvatar;
            set => SetProperty(ref _userAvatar, value);
        }

        private string _text;
        public string Text
        {
            get => _text;
            set => SetProperty(ref _text, value);
        }

        private string _hashtag;
        public string Hashtag
        {
            get => _hashtag;
            set => SetProperty(ref _hashtag, value);
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
        public ICommand LikeTweetCommand => _likeTweetCommand ?? (_likeTweetCommand = SingleExecutionCommand.FromFunc<ImagesTweetViewModel>(OnLikeCommandAsync));

        private ICommand _openTweetCommand;
        public ICommand OpenTweetCommand => _openTweetCommand ?? (_openTweetCommand = SingleExecutionCommand.FromFunc<ImagesTweetViewModel>(OnOpenTweetCommandAsync));

        private ICommand _markTweetCommand;
        public ICommand MarkTweetCommand => _markTweetCommand ?? (_markTweetCommand = SingleExecutionCommand.FromFunc<BaseTweetViewModel>(OnMarkCommandAsync));

        private DateTime _CreationTime;
        public DateTime CreationTime
        {
            get => _CreationTime;
            set => SetProperty(ref _CreationTime, value);
        }
        #endregion

        #region -- Private helpers --

        private Task OnLikeCommandAsync(ImagesTweetViewModel tweet)
        {
            IsTweekLiked = !IsTweekLiked;
            return Task.CompletedTask;
        }

        private Task OnOpenTweetCommandAsync(ImagesTweetViewModel arg)
        {
            throw new NotImplementedException();
        }

        private Task OnMarkCommandAsync(BaseTweetViewModel tweet)
        {
            IsBookmarked = !IsBookmarked;
            return Task.CompletedTask;
        }

        #endregion

    }
}
