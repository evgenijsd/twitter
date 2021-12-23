using InterTwitter.Enums;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Windows.Input;

namespace InterTwitter.ViewModels.TweetViewModel
{
    public class BaseTweetViewModel : BindableBase
    {
        public BaseTweetViewModel()
        {
            Mode = EStateMode.WithoutTweet;
        }
        #region -- Public properties --

        private EStateMode _mode;
        public EStateMode Mode
        {
            get => _mode;
            set => SetProperty(ref _mode, value);
        }

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

        private bool _isTweetLiked;
        public bool IsTweetLiked
        {
            get => _isTweetLiked;
            set => SetProperty(ref _isTweetLiked, value);
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

        private DateTime _creationTime;
        public DateTime CreationTime
        {
            get => _creationTime;
            set => SetProperty(ref _creationTime, value);
        }

        private ICommand _likeCommand;
        public ICommand LikeCommand
        {
            get => _likeCommand;
            set => SetProperty(ref _likeCommand, value);
        }

        private ICommand _bookmarkCommand;
        public ICommand BookmarkCommand
        {
            get => _bookmarkCommand;
            set => SetProperty(ref _bookmarkCommand, value);
        }

        private ICommand _moveToImagesGalleryCommand;
        public ICommand MoveToImagesGalleryCommand
        {
            get => _moveToImagesGalleryCommand;
            set => SetProperty(ref _moveToImagesGalleryCommand, value);
        }

        private ICommand _moveToVideoGalleryCommand;
        public ICommand MoveToVideoGalleryCommand
        {
            get => _moveToVideoGalleryCommand;
            set => SetProperty(ref _moveToVideoGalleryCommand, value);
        }

        private ICommand _moveToAuthorCommand;
        public ICommand MoveToAuthorCommand
        {
            get => _moveToAuthorCommand;
            set => SetProperty(ref _moveToAuthorCommand, value);
        }

        #endregion

    }
}
