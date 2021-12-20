using InterTwitter.Helpers;
using InterTwitter.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace InterTwitter.Services
{
    public interface ITweetService
    {
        Task<AOResult<List<TweetModel>>> GetAllTweetsAsync();

        Task<AOResult<UserModel>> GetAuthorAsync(int authorId);

        Task<AOResult<List<TweetModel>>> GetByUserTweetsAsync(int userid);
    }
}
