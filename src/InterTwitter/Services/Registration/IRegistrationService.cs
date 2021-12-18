using InterTwitter.Helpers.ProcessHelpers;
using InterTwitter.Models;
using System.Threading.Tasks;

namespace InterTwitter.Services.Registration
{
    public interface IRegistrationService
    {
        Task<AOResult<int>> AddAsync(UserModel user);
        Task<AOResult> CheckTheCorrectEmailAsync(string email);
        Task<AOResult<UserModel>> GetByIdAsync(int id);
    }
}
