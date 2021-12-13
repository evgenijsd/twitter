using InterTwitter.Helpers;
using InterTwitter.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace InterTwitter.Services.TweetService
{
    public interface ITweetService
    {
        Task<AOResult<IEnumerable<TweetModel>>> GetAllTweetsAsync();

        Task<AOResult<UserModel>> GetUserAsync(int userId);

        Task<AOResult<IEnumerable<TweetModel>>> GetAllTweetsByHashtagsOrKeysAsync(string searchQuery);
    }
}