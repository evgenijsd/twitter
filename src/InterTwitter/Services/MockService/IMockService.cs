using InterTwitter.Models;
using System.Collections.Generic;

namespace InterTwitter.Services
{
    public interface IMockService
    {
        IList<UserModel> Users { get; set; }

        IList<TweetModel> Tweets { get; set; }

        IList<HashtagModel> Hashtags { get; set; }

        IList<MuteModel> MuteList { get; set; }

        IList<BlockModel> BlackList { get; set; }

        IList<LikeModel> Likes { get; set; }
    }
}
