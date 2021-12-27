using InterTwitter.Helpers.ProcessHelpers;
using InterTwitter.Models;
using System;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.Essentials;

namespace InterTwitter.Services
{
    public class AuthorizationService : IAuthorizationService
    {
        private readonly IMockService _mockService;

        public AuthorizationService(IMockService mockService)
        {
            _mockService = mockService;
        }

        #region -- Public helpers --

        public async Task<AOResult<UserModel>> CheckUserAsync(string email, string password)
        {
            var result = new AOResult<UserModel>();

            try
            {
                var user = await _mockService.FindAsync<UserModel>(x => x.Email.ToLower() == email.ToLower() && x.Password == password);
                if (user != null)
                {
                    result.SetSuccess(user);
                }
                else
                {
                    result.SetFailure();
                }
            }
            catch (Exception ex)
            {
                result.SetError($"Exception: {nameof(CheckUserAsync)}", "Wrong result", ex);
            }

            return result;
        }
        #endregion
    }
}
