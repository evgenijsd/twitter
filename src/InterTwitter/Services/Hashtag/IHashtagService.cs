using InterTwitter.Helpers;
using InterTwitter.Helpers.ProcessHelpers;
using InterTwitter.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace InterTwitter.Services.Hashtag
{
    public interface IHashtagService
    {
        Task<AOResult> IncreaseHashtagPopularityByOne(HashtagModel hashtag);

        Task<AOResult> DecreaseHashtagPopularityByOne(HashtagModel hashtag);

        Task<AOResult<IEnumerable<HashtagModel>>> GetPopularHashtags(int hashtagsNumber);
    }
}
