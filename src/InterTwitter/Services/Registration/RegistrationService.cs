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

        public async Task<AOResult<ECheckEnter>> CheckTheCorrectEmailAsync(string email)
        {
            var result = new AOResult<ECheckEnter>();
            try
            {
                var user = _users.FirstOrDefault(x => x.Email.ToLower() == email.ToLower());
                ECheckEnter check = ECheckEnter.ChecksArePassed;
                if (user != null)
                {
                    check = ECheckEnter.LoginExist;
                }
                else
                {
                    result.SetFailure();
                }

                result.SetSuccess(check);
            }
            catch (Exception ex)
            {
                result.SetError($"Exception: {nameof(CheckTheCorrectEmailAsync)}", "Wrong result", ex);
            }

            return result;
        }

        public ECheckEnter CheckCorrectName(string name)
        {
            const string validName = @"^[a-zA-Z]+$";
            ECheckEnter result = ECheckEnter.ChecksArePassed;

            if (!Regex.IsMatch(name, validName))
            {
                result = ECheckEnter.NameNotValid;
            }

            if (name.Length < MIN_LENGTH_NAME)
            {
                result = ECheckEnter.NameLengthNotValid;
            }

            return result;
        }

        public ECheckEnter CheckCorrectEmail(string email)
        {
            const string validEmail = @"\A[^@]+@([^@\.]+\.)+[^@\.]+\z";
            ECheckEnter result = ECheckEnter.ChecksArePassed;

            int s = email.IndexOf('@');
            if (s > MAX_LENGTH_EMAIL || (email.Length - s) > MAX_LENGTH_EMAIL)
            {
                result = ECheckEnter.EmailLengthNotValid;
            }

            if (!Regex.IsMatch(email, validEmail))
            {
                result = ECheckEnter.EmailNotValid;
            }

            if (s == -1)
            {
                result = ECheckEnter.EmailANotVaid;
            }

            return result;
        }

        public ECheckEnter CheckTheCorrectPassword(string password, string confirmPassword)
        {
            const string validPassword = @"^(?=.*[A-Z])(?=.*\d)[\d\D]+$";
            ECheckEnter check = ECheckEnter.ChecksArePassed;

            if (password != confirmPassword)
            {
                check = ECheckEnter.PasswordsNotEqual;
            }

            if (!Regex.IsMatch(password, validPassword))
            {
                check = ECheckEnter.PasswordBigLetterAndDigit;
            }

            if (password.Length < MIN_PASSWORD_LENGTH)
            {
                check = ECheckEnter.PasswordLengthNotValid;
            }

            return check;
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
                result.SetError($"Exception: {nameof(UserAddAsync)}", "Wrong result", ex);
            }

            return result;
        }

        #endregion
    }
}
