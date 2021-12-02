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
        private List<User> _users;

        public RegistrationService()
        {
            _users = new List<User>
            {
                new User { Id = 1, Name = "Gabriela Flores", Email = "aaa@aaa.aaa", Password = "1234567A", UserPhoto = "pic_profile_big" },
                new User { Id = 2, Name = "Yuki Sato", Email = "bbb@bbb.bbb", Password = "1234567A", UserPhoto = "pic_profile_big" },
                new User { Id = 3, Name = "John Dou", Email = "bbb@bbb.bbb", Password = "1234567A", UserPhoto = "pic_profile_big" },
            };
        }

        #region -- Public helpers --
        public List<User> GetUsers()
        {
            return _users;
        }

        public async Task<AOResult<int>> CheckTheCorrectEmailAsync(string name, string email)
        {
            var result = new AOResult<int>();
            try
            {
                var user = _users.FirstOrDefault(x => x.Email == email);
                ECheckEnter check = ECheckEnter.ChecksArePassed;
                check = CheckCorrectEmail(email);
                if (user != null)
                {
                    check = ECheckEnter.LoginExist;
                }

                result.SetSuccess((int)check);
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

            if (!Regex.IsMatch(email, validEmail))
            {
                result = ECheckEnter.EmailNotValid;
            }

            int s = email.IndexOf('@');
            if (s > MAX_LENGTH_EMAIL || (email.Length - s) > MAX_LENGTH_EMAIL
                || email.Length - 1 == s || s == 0)
            {
                result = ECheckEnter.EmailLengthNotValid;
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

        public async Task<int> UserAddAsync(User user)
        {
            int result = 0;
            try
            {
                user.Id = _users.Count();
                _users.Add(user);
                result = _users.Last().Id;
            }
            catch
            {
            }

            return result;
        }

        #endregion
    }
}
