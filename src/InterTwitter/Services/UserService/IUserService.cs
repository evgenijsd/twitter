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
    }
}
