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
                    Email = "test@gmail.com",
                    Password = "1111",
                    AvatarPath = "https://cdn.allfamous.org/people/avatars/bill-gates-zdrr-allfamous.org.jpg",
                    BackgroundUserImagePath = "https://yapx.ru/viral/PMYaG",
                },
                new UserModel
                {
                    Id = 2,
                    Name = "Kate White",
                    Email = "test2@gmail.com",
                    Password = "2222",
                    AvatarPath = "https://www.iso.org/files/live/sites/isoorg/files/news/News_archive/2021/03/Ref2639/Ref2639.jpg/thumbnails/300x300",
                    BackgroundUserImagePath = "https://yapx.ru/viral/PMYaG",
                },
                new UserModel
                {
                    Id = 3,
                    Name = "Sam Smith",
                    Email = "test3@gmail.com",
                    Password = "3333",
                    AvatarPath = "https://i.ebayimg.com/images/g/6EIAAOSwJHlfnm3a/s-l300.jpg",
                    BackgroundUserImagePath = "https://yapx.ru/viral/PMYaG",
                },
                new UserModel
                {
                    Id = 4,
                    Name = "Steve Jobs",
                    Email = "test4@gmail.com",
                    Password = "4444",
                    AvatarPath = "https://www.acumarketing.com/wp-content/uploads/2011/08/steve-jobs.jpg",
                    BackgroundUserImagePath = "https://yapx.ru/viral/PMYaG",
                },
                new UserModel
                {
                    Id = 5,
                    Name = "Elon musk",
                    Email = "test5@gmail.com",
                    Password = "4444",
                    AvatarPath = "https://file.liga.net/images/general/2021/09/20/thumbnail-20210920123323-9397.jpg?v=1632132620",
                    BackgroundUserImagePath = "https://yapx.ru/viral/PMYaG",
                },
                new UserModel
                {
                    Id = 6,
                    Name = "Keano Reaves",
                    Email = "test6@gmail.com",
                    Password = "4444",
                    AvatarPath = "https://www.biography.com/.image/ar_1:1%2Cc_fill%2Ccs_srgb%2Cg_face%2Cq_auto:good%2Cw_300/MTE5NTU2MzE2MzU1NzI0ODEx/keanu-reeves-9454211-1-402.jpg",
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

        public async Task<AOResult<UserModel>> GetByIdAsync(int id)
        {
            var result = new AOResult<UserModel>();
            try
            {
                var user = _users?.FirstOrDefault(x => x.Id == id);
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
                result.SetError($"Exception: {nameof(GetByIdAsync)}", Resources.Resource.AlertDatabase, ex);
            }

            return result;
        }

        public async Task<AOResult<int>> AddAsync(UserModel user)
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
                result.SetError($"Exception: {nameof(AddAsync)}", Resources.Resource.AlertDatabase, ex);
            }

            return result;
        }

        #endregion
    }
}
