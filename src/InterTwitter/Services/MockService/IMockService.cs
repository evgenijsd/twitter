using InterTwitter.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace InterTwitter.Services
{
    public interface IMockService
    {
        Task<IEnumerable<TweetModel>> GetAllTweetsAsync();

        Task<UserModel> GetTweetAuthorAsync(int id);

        Task AddTweetAsync(TweetModel tm);
    }
}
