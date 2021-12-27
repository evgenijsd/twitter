using InterTwitter.Enums;
using InterTwitter.Helpers;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Input;

namespace InterTwitter.Models.NotificationViewModel
{
    public class BaseNotificationViewModel : BindableBase
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

        private string _notificationIcon;

        public string NotificationIcon
        {
            get => _notificationIcon;
            set => SetProperty(ref _notificationIcon, value);
        }

        private string _notificationText;

        public string NotificationText
        {
            get => _notificationText;
            set => SetProperty(ref _notificationText, value);
        }

        #endregion
    }
}
