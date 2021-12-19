using InterTwitter.Helpers;
using InterTwitter.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace InterTwitter.Services
{
    public interface ILikeService
    {
        Task<AOResult> DeleteLikeAsync(int tweetId, int userId);
        Task<AOResult<List<LikeModel>>> GetLikesAsync(int userId);
        Task<AOResult<int>> AddLikeAsync(int tweetId, int userId);
        Task<AOResult> AnyAsync(int tweetId, int userId);
        Task<AOResult<int>> CountAsync(int tweetId);
    }
}
