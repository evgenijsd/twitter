﻿using InterTwitter.Models;
using System.Collections.Generic;

namespace InterTwitter.Services.MockService
{
    public interface IMockService
    {
        IEnumerable<UserModel> Users { get; set; }

        IEnumerable<TweetModel> Tweets { get; set; }

        IEnumerable<HashtagModel> Hashtags { get; set; }
    }
}