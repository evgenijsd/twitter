using InterTwitter.Enums;
using InterTwitter.Helpers;
using InterTwitter.Views.TweetFullPage;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Input;

namespace InterTwitter.ViewModels.TweetViewModel
{
    public class BaseTweetViewModel : BindableBase
    {
        #region -- Public properties --

        private int _tweetId;
        public int TweetId
        {
            get => _tweetId;
            set => SetProperty(ref _tweetId, value);
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
            set
            {
                SetProperty(ref _text, value);
                RaisePropertyChanged(nameof(IsTextVisible));
            }
        }

        public bool IsTextVisible => !string.IsNullOrEmpty(Text);

        private IEnumerable<string> _mediaPaths;
        public IEnumerable<string> MediaPaths
        {
            get => _mediaPaths;
            set => SetProperty(ref _mediaPaths, value);
        }

        private int _likesNumber;
        public int LikesNumber
        {
            get => _likesNumber;
            set => SetProperty(ref _likesNumber, value);
        }

        private bool _IsTweetLiked;
        public bool IsTweetLiked
        {
            get => _IsTweetLiked;
            set => SetProperty(ref _IsTweetLiked, value);
        }

        private bool _isBookmarked;
        public bool IsBookmarked
        {
            get => _isBookmarked;
            set => SetProperty(ref _isBookmarked, value);
        }

        private string _userBackgroundImage;
        public string UserBackgroundImage
        {
            get => _userBackgroundImage;
            set => SetProperty(ref _userBackgroundImage, value);
        }

        private EAttachedMediaType _mediaType;
        public EAttachedMediaType Media
        {
            get => _mediaType;
            set => SetProperty(ref _mediaType, value);
        }

        private DateTime _CreationTime;
        public DateTime CreationTime
        {
            get => _CreationTime;
            set => SetProperty(ref _CreationTime, value);
        }

        private ICommand _likeTweetCommand;
        public ICommand LikeTweetCommand
        {
            get => _likeTweetCommand;
            set => SetProperty(ref _likeTweetCommand, value);
        }

        private ICommand _markTweetCommand;
        public ICommand MarkTweetCommand
        {
            get => _markTweetCommand;
            set => SetProperty(ref _markTweetCommand, value);
        }

        private ICommand _openTweetCommand;
        public ICommand OpenTweetCommand
        {
            get => _openTweetCommand;
            set => SetProperty(ref _openTweetCommand, value);
        }

        private ICommand _moveToProfileCommand;
        public ICommand MoveToProfileCommand
        {
            get => _moveToProfileCommand;
            set => SetProperty(ref _moveToProfileCommand, value);
        }

        #endregion

    }
}
