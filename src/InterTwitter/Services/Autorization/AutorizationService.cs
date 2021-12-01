using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InterTwitter.Helpers.ProcessHelpers;
using InterTwitter.Services.Registration;
using Xamarin.Essentials;

namespace InterTwitter.Services.Autorization
{
    public class AutorizationService : IAutorizationService
    {
        private IRegistrationService _registrationService;

        public AutorizationService(IRegistrationService registrationService)
        {
            _registrationService = registrationService;
        }

        #region -- Public properties --
        public int UserId
        {
            get => Preferences.Get(nameof(UserId), 0);
            set => Preferences.Set(nameof(UserId), value);
        }
        #endregion
        #region -- Public helpers --
        public async Task<AOResult<int>> CheckUserAsync(string email, string password)
        {
            var result = new AOResult<int>();

            try
            {
                var user = _registrationService.GetUsers().FirstOrDefault(x => x.Email == email);
                if (user != null && user.Email == email && user.Password == password)
                {
                    result.SetSuccess(user.Id);
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
