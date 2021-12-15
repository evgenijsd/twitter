using InterTwitter.Models;
using System.Collections.Generic;

namespace InterTwitter.Services
{
    public interface IMockService
    {
        List<UserModel> Users { get; set; }
        List<LikeModel> Likes { get; set; }
        List<MuteModel> MuteList { get; set; }
        List<BlockModel> BlackList { get; set; }
        IEnumerable<TweetModel> Tweets { get; set; }
    }
}
