using InterTwitter.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace InterTwitter.Services.UserService
{
    public interface IUserService
    {
        Task<UserModel> GetUser(int id);
        Task<List<UserModel>> GetAllUsers();
        Task InsertUser(UserModel user);
        Task DeleteUser(int id);
        Task UpdateUser(UserModel user);
    }
}
