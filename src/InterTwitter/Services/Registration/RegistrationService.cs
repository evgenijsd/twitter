using InterTwitter.Helpers.ProcessHelpers;
using InterTwitter.Models;
using InterTwitter.Resources.Strings;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace InterTwitter.Services
{
    public class RegistrationService : IRegistrationService
    {
        private readonly IMockService _mockService;

        public RegistrationService(IMockService mockService)
        {
            _mockService = mockService;
        }

        #region -- Public helpers --

        public async Task<AOResult> CheckTheCorrectEmailAsync(string email)
        {
            var result = new AOResult();
            try
            {
                var user = _mockService.Users.FirstOrDefault(x => x.Email.ToLower() == email.ToLower());
                if (user != null)
                {
                    result.SetSuccess();
                }
                else
                {
                    result.SetFailure();
                }
            }
            catch (Exception ex)
            {
                result.SetError($"Exception: {nameof(CheckTheCorrectEmailAsync)}", Strings.AlertDatabase, ex);
            }

            return result;
        }

        public async Task<AOResult<UserModel>> GetByIdAsync(int id)
        {
            var result = new AOResult<UserModel>();
            try
            {
                var user = _mockService.Users.FirstOrDefault(x => x.Id == id);
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
                result.SetError($"Exception: {nameof(GetByIdAsync)}", Strings.AlertDatabase, ex);
            }

            return result;
        }

        public async Task<AOResult<int>> AddAsync(UserModel user)
        {
            var result = new AOResult<int>();
            try
            {
                user.Id = (int)_mockService.Users?.Count() + 1;
                _mockService.Users?.Add(user);
                int id = (int)_mockService.Users?.Last().Id;
                if (id > 0)
                {
                    result.SetSuccess(id);
                }
                else
                {
                    result.SetFailure();
                }
            }
            catch (Exception ex)
            {
                result.SetError($"Exception: {nameof(AddAsync)}", Strings.AlertDatabase, ex);
            }

            return result;
        }

        #endregion
    }
}
