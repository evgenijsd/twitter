using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace InterTwitter.Models.TweetViewModel
{
    public class BaseTweetViewModel : BindableBase
    {
        private int _id;
        public int Id
        {
            get => _id;
            set => SetProperty(ref _id, value);
        }

        private int _likes;
        public int Likes
        {
            get => _likes;
            set => SetProperty(ref _likes, value);
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

        private List<string> _images;
        public List<string> Images
        {
            get => _images;
            set => SetProperty(ref _images, value);
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

        private bool _IsTweetMarked;
        public bool IsTweetMarked
        {
            get => _IsTweetMarked;
            set => SetProperty(ref _IsTweetMarked, value);
        }

        private string _media;
        public string Media
        {
            get => _media;
            set => SetProperty(ref _media, value);
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

        private DateTime _CreationTime;
        public DateTime CreationTime
        {
            get => _CreationTime;
            set => SetProperty(ref _CreationTime, value);
        }
    }
}
