using InterTwitter.Helpers.ProcessHelpers;
using InterTwitter.Models;
using System.Threading.Tasks;

namespace InterTwitter.Services
{
    public interface IAuthorizationService
    {
        int UserId { get; set; }

        Task<AOResult<UserModel>> CheckUserAsync(string login, string password);
    }
}
