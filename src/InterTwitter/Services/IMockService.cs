using InterTwitter.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace InterTwitter.Services
{
    public interface IMockService
    {
        IEnumerable<UserModel> Users { get; set; }

        IEnumerable<TweetModel> Tweets { get; set; }
    }
}
