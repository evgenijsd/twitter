using InterTwitter.Helpers;
using InterTwitter.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace InterTwitter.Services.UserService
{
    public interface IUserService
    {
        Task<AOResult<UserModel>> GetUserAsync(int id);
        Task<AOResult<List<UserModel>>> GetAllUsersAsync();
        Task<AOResult<int>> InsertUserAsync(UserModel user);
        Task<AOResult<int>> DeleteUserAsync(UserModel user);
        Task<AOResult<int>> UpdateUserAsync(UserModel user);

        Task<AOResult<int>> AddToBlacklistAsync(int currentUserId, int userId);
        Task<AOResult<int>> AddToMuteListAsync(int currentUserId, int userId);
        Task<AOResult<int>> RemoveFromBlacklistAsync(int currentUserId, int userId);
        Task<AOResult<int>> RemoveFromMutelistAsync(int currentUserId, int userId);
        Task<AOResult<bool>> IsUserMuted(int currentUserId, int userId);
        Task<AOResult<bool>> IsUserBlocked(int currentUserId, int userId);
        Task<AOResult<List<UserModel>>> GetAllMutedUsersAsync(int currentUserId);
        Task<AOResult<List<UserModel>>> GetAllBlockedUsersAsync(int currentUserId);
    }
}
