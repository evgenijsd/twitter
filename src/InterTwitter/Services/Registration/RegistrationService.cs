using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using InterTwitter.Enums;
using InterTwitter.Helpers.ProcessHelpers;
using InterTwitter.Models;

namespace InterTwitter.Services.Registration
{
    public class RegistrationService : IRegistrationService
    {
        private const int MAX_LENGTH_EMAIL = 64;
        private const int MIN_LENGTH_NAME = 2;
        private const int MIN_PASSWORD_LENGTH = 6;
        private List<UserModel> _users;

        public RegistrationService()
        {
            _users = new List<UserModel>
            {
                new UserModel { Id = 1, Name = "Gabriela", Email = "aaa@aaa.aaa", Password = "1234567A", UserPhoto = "pic_profile_big", BackgroundPhoto = "pic_profile_big" },
                new UserModel { Id = 2, Name = "Yuki", Email = "bbb@bbb.bbb", Password = "1234567A", UserPhoto = "pic_profile_big", BackgroundPhoto = "pic_profile_big" },
                new UserModel { Id = 3, Name = "John", Email = "ccc@ccc.ccc", Password = "1234567A", UserPhoto = "pic_profile_big", BackgroundPhoto = "pic_profile_big" },
            };
        }

        #region -- Public helpers --
        public List<UserModel> GetUsers()
        {
            return _users;
        }

        public async Task<AOResult<bool>> CheckTheCorrectEmailAsync(string email)
        {
            var result = new AOResult<bool>();
            try
            {
                var user = _users.FirstOrDefault(x => x.Email.ToLower() == email.ToLower());
                bool check = false;
                if (user != null)
                {
                    check = true;
                }
                else
                {
                    result.SetFailure();
                }

                result.SetSuccess(check);
            }
            catch (Exception ex)
            {
                result.SetError($"Exception: {nameof(CheckTheCorrectEmailAsync)}", Resources.Resource.AlertDatabase, ex);
            }

            return result;
        }

        public async Task<AOResult<int>> UserAddAsync(UserModel user)
        {
            var result = new AOResult<int>();
            try
            {
                user.Id = _users.Count() + 1;
                _users.Add(user);
                int id = _users.Last().Id;
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
                result.SetError($"Exception: {nameof(UserAddAsync)}", Resources.Resource.AlertDatabase, ex);
            }

            return result;
        }

        #endregion
    }
}
