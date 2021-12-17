using System.Threading.Tasks;
using InterTwitter.Helpers.ProcessHelpers;
using InterTwitter.Models;

namespace InterTwitter.Services.Authorization
{
    public interface IAuthorizationService
    {
        int UserId { get; set; }

        Task<AOResult<UserModel>> CheckUserAsync(string login, string password);
    }
}
