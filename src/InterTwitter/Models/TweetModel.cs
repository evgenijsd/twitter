﻿using InterTwitter.Enums;
using System;
using System.Collections.Generic;

namespace InterTwitter.Models
{
    public class TweetModel : IEntityBase
    {
        public int Id { get; set; }

        public int UserId { get; set; }

        public string Text { get; set; }

        public EAttachedMediaType Media { get; set; }

        public IEnumerable<string> MediaPaths { get; set; }

        public DateTime CreationTime { get; set; }
    }
}
