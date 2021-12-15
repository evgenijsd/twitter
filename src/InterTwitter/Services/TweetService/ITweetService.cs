using InterTwitter.Helpers;
using InterTwitter.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace InterTwitter.Services
{
    public interface ITweetService
    {
        Task<AOResult<IEnumerable<TweetModel>>> GetAllTweetsAsync();

        Task<AOResult<UserModel>> GetAuthorAsync(int authorId);

        Task<AOResult<IEnumerable<TweetModel>>> GetAllTweetsByHashtagsOrKeysAsync(string searchQuery);
    }
}
