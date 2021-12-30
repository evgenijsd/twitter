using InterTwitter.Helpers;
using InterTwitter.Helpers.ProcessHelpers;
using InterTwitter.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace InterTwitter.Services
{
    public interface IBookmarkService
    {
        Task<AOResult> DeleteBoormarkAsync(int tweetId, int userId);
        Task<AOResult> DeleteAllBookmarksAsync(int userId);
        Task<AOResult<List<Bookmark>>> GetBookmarksAsync(int userId);
        Task<AOResult<List<Bookmark>>> GetNotificationsAsync(int userId);
        Task<AOResult<int>> AddBookmarkAsync(int tweetId, int userId);
        Task<AOResult> AnyAsync(int tweetId, int userId);
    }
}
