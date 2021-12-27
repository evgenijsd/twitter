using InterTwitter.Helpers;
using InterTwitter.Helpers.ProcessHelpers;
using InterTwitter.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace InterTwitter.Services.Hashtag
{
    public interface IHashtagService
    {
        Task<AOResult> IncrementHashtagPopularity(string hashtag);

        Task<AOResult> DecrementHashtagPopularity(string hashtag);

        Task<AOResult<IEnumerable<HashtagModel>>> GetPopularHashtags(int hashtagsNumber);
    }
}
