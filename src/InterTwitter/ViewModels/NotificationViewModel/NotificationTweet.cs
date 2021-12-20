using InterTwitter.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace InterTwitter.ViewModels.NotificationViewModel
{
    public class NotificationTweet
    {
        public int TweetId { get; set; }

        public string UserAvatar { get; set; }

        public string UserName { get; set; }

        public string Text { get; set; }

        public IEnumerable<string> MediaPaths { get; set; }

        public EAttachedMediaType Media { get; set; }

        public string NotificationIcon { get; set; }

        public string NotificationText { get; set; }
    }
}
