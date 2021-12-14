using System.Collections.Generic;
using System.Threading.Tasks;
using InterTwitter.Helpers.ProcessHelpers;
using InterTwitter.Models;

namespace InterTwitter.Services.Registration
{
    public interface IRegistrationService
    {
        Task<AOResult<int>> AddAsync(UserModel user);
        Task<AOResult<bool>> CheckTheCorrectEmailAsync(string email);
        Task<AOResult<UserModel>> GetByIdAsync(int id);
        List<UserModel> GetUsers();
    }
}
