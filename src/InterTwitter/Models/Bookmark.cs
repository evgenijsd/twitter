using System;
using System.Collections.Generic;
using System.Text;

namespace InterTwitter.Models
{
    public class Bookmark : IEntityBase
    {
        public int Id { get; set; }

        public int UserId { get; set; }

        public int TweetId { get; set; }

        public bool Notification { get; set; }

        public DateTime CreationTime { get; set; }
    }
}
