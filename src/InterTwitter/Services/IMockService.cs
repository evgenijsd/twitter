using InterTwitter.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace InterTwitter.Services
{
    public interface IMockService
    {
        List<UserModel> Users { get; set; }

        List<TweetModel> Tweets { get; set; }
    }
}
