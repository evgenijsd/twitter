using InterTwitter.Helpers;
using InterTwitter.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace InterTwitter.Services.HashtagManager
{
    public interface IHashtagManager
    {
        Task<AOResult> IncreaseHashtagPopularityByOne(HashtagModel hashtag);

        Task<AOResult> DecreaseHashtagPopularityByOne(HashtagModel hashtag);

        Task<AOResult<IEnumerable<HashtagModel>>> GetPopularHashtags(int hashtagsNumber);
    }
}
