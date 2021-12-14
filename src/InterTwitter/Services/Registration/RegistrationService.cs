using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using InterTwitter.Helpers.ProcessHelpers;
using InterTwitter.Models;

namespace InterTwitter.Services.Registration
{
    public class RegistrationService : IRegistrationService
    {
        private List<UserModel> _users;

        public RegistrationService()
        {
            _users = new List<UserModel>
            {
                new UserModel
                {
                    Id = 1,
                    Name = "Bill Gates",
                    Email = "aaa@aaa.aaa",
                    Password = "1234567A",
                    AvatarPath = "https://inhabitat.com/wp-content/blogs.dir/1/files/2017/08/Bill-Gates-889x598.jpg",
                    BackgroundUserImagePath = "https://yapx.ru/viral/PMYaG",
                },
                new UserModel
                {
                    Id = 2,
                    Name = "Kate White",
                    Email = "bbb@bbb.bbb",
                    Password = "1234567A",
                    AvatarPath = "https://i.pinimg.com/236x/01/e1/10/01e11011168eb3e1c83d16747192d490.jpg",
                    BackgroundUserImagePath = "https://yapx.ru/viral/PMYaG",
                },
                new UserModel
                {
                    Id = 3,
                    Name = "Sam Smith",
                    Email = "ccc@ccc.ccc",
                    Password = "1234567A",
                    AvatarPath = "http://www.kinofilms.ua/images/person/big/738231.jpg",
                    BackgroundUserImagePath = "https://yapx.ru/viral/PMYaG",
                },
                new UserModel
                {
                    Id = 4,
                    Name = "Steve Jobs",
                    Email = "ddd@ddd.ddd",
                    Password = "1234567A",
                    AvatarPath = "https://upload.wikimedia.org/wikipedia/commons/b/b9/Steve_Jobs_Headshot_2010-CROP.jpg",
                    BackgroundUserImagePath = "https://yapx.ru/viral/PMYaG",
                },
                new UserModel
                {
                    Id = 5,
                    Name = "Elon Musk",
                    Email = "eee@eee.eee",
                    Password = "1234567A",
                    AvatarPath = "https://ichef.bbci.co.uk/news/640/cpsprodpb/81F4/production/_118486233_gettyimages-1229892674.jpg",
                    BackgroundUserImagePath = "https://yapx.ru/viral/PMYaG",
                },
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
                var user = _users?.FirstOrDefault(x => x.Email.ToLower() == email.ToLower());
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
