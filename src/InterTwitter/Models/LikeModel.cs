﻿namespace InterTwitter.Models
{
    public class LikeModel : IEntityBase
    {
        public int Id { get; set; }

        public int UserId { get; set; }

        public int TweetId { get; set; }

        public bool Notification { get; set; }
    }
}
