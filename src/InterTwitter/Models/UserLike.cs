﻿namespace InterTwitter.Models
{
    public class UserLike : IEntityBase
    {
        public int Id { get; set; }

        public int UserId { get; set; }

        public int TweetId { get; set; }
    }
}
