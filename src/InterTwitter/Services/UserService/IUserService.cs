﻿using InterTwitter.Helpers.ProcessHelpers;
using InterTwitter.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace InterTwitter.Services.UserService
{
    public interface IUserService
    {
        Task<AOResult<UserModel>> GetUserAsync(int id);

        Task<AOResult<IEnumerable<UserModel>>> GetAllUsersAsync();

        Task<AOResult> InsertUserAsync(UserModel user);

        Task<AOResult> DeleteUserAsync(UserModel user);

        Task<AOResult> UpdateUserAsync(UserModel user);

        Task<AOResult> AddToBlacklistAsync(int userId);

        Task<AOResult> AddToMuteListAsync(int userId);

        Task<AOResult> RemoveFromBlacklistAsync(int userId);

        Task<AOResult> RemoveFromMutelistAsync(int userId);

        Task<AOResult<bool>> CheckIfUserIsMutedAsync(int userId);

        Task<AOResult<bool>> CheckIfUserIsBlockedAsync(int userId);

        Task<AOResult<IEnumerable<UserModel>>> GetAllMutedUsersAsync();

        Task<AOResult<IEnumerable<UserModel>>> GetAllBlockedUsersAsync();
    }
}
