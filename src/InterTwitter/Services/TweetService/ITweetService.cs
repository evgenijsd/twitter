using InterTwitter.Helpers;
using InterTwitter.Models;
using InterTwitter.Models.TweetViewModel;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace InterTwitter.Services
{
    public interface ITweetService
    {
        Task<AOResult<IEnumerable<TweetModel>>> GetAllTweetsAsync();

        Task<AOResult<UserModel>> GetUserAsync(int userId);

        void DeleteBoormark(int tweetId, int userId);
        Task<AOResult<IEnumerable<Bookmark>>> GetBookmarksAsync(int userId);
    }
}
