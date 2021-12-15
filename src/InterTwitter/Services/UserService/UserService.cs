using InterTwitter.Helpers;
using InterTwitter.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InterTwitter.Services.UserService
{
    public class UserService : IUserService
    {
        private readonly IMockService _mockService;

        #region -- IUserService Implementation --

        public UserService(IMockService mockService)
        {
            _mockService = mockService;
        }

        public Task<AOResult<int>> DeleteUserAsync(UserModel user)
        {
            var result = new AOResult<int>();
            try
            {
                _mockService.Users.Remove(user);
                result.SetSuccess(user.Id);
            }
            catch (Exception e)
            {
                result.SetError($"Method:{nameof(UserService)}/{nameof(DeleteUserAsync)} exception:", "Error", e);
            }

            return Task<AOResult<int>>.Factory.StartNew(() => result);
        }

        public Task<AOResult<List<UserModel>>> GetAllUsersAsync()
        {
            var result = new AOResult<List<UserModel>>();
            try
            {
                var users = _mockService.Users;
                if (users != null)
                {
                    result.SetSuccess(users);
                }
                else
                {
                    result.SetFailure("Users not found!");
                }
            }
            catch (Exception e)
            {
                result.SetError($"Method:{nameof(UserService)}/{nameof(GetAllUsersAsync)} exception:", "Error", e);
            }

            return Task<AOResult<List<UserModel>>>.Factory.StartNew(() => result);
        }

        public Task<AOResult<UserModel>> GetUserAsync(int id)
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
                    result.SetFailure("User not found!");
                }
            }
            catch (Exception e)
            {
                result.SetError($"Method:{nameof(UserService)}/{nameof(GetUserAsync)} exception:", "Error", e);
            }

            return Task<AOResult<UserModel>>.Factory.StartNew(() => result);
        }

        public Task<AOResult<int>> InsertUserAsync(UserModel user)
        {
            var result = new AOResult<int>();
            try
            {
                var sameUser = _mockService.Users.FirstOrDefault(x => x.Email == user.Email);
                if (sameUser == null)
                {
                    var lastId = _mockService.Users.Last().Id;
                    user.Id = ++lastId;
                    _mockService.Users.Add(user);
                    result.SetSuccess(user.Id);
                }
                else
                {
                    result.SetFailure("Such user already exists");
                }
            }
            catch (Exception e)
            {
                result.SetError($"Method:{nameof(UserService)}/{nameof(InsertUserAsync)} exception:", "Error", e);
            }

            return Task<AOResult<int>>.Factory.StartNew(() => result);
        }

        public Task<AOResult<int>> UpdateUserAsync(UserModel user)
        {
            var result = new AOResult<int>();
            try
            {
                var oldUser = _mockService.Users?.FirstOrDefault(x => x.Id == user.Id);
                _mockService.Users?.Remove(oldUser);
                _mockService.Users?.Add(user);
                _mockService.Users.Sort((x1, x2) => x1.Id.CompareTo(x2.Id));
                result.SetSuccess(user.Id);
            }
            catch (Exception e)
            {
                result.SetError($"Method:{nameof(UserService)}/{nameof(UpdateUserAsync)} exception:", "Error", e);
            }

            return Task<AOResult<int>>.Factory.StartNew(() => result);
        }

        //7777777777777777777777777777777
        public Task<AOResult<int>> AddToBlacklistAsync(int currentUserId, int userId)
        {
            var result = new AOResult<int>();
            try
            {
                var id = _mockService.BlackList.Count();
                _mockService.BlackList.Add(new BlockModel
                {
                    UserId = currentUserId,
                    BlockedUserId = userId,
                    Id = id,
                });
                result.SetSuccess(id);
            }
            catch (Exception e)
            {
                result.SetError($"Method:{nameof(UserService)}/{nameof(AddToBlacklistAsync)} exception:", "Error", e);
            }

            return Task<AOResult<int>>.Factory.StartNew(() => result);
        }

        public Task<AOResult<int>> AddToMuteListAsync(int currentUserId, int userId)
        {
            var result = new AOResult<int>();
            try
            {
                var id = _mockService.MuteList.Count();
                _mockService.MuteList.Add(new MuteModel
                {
                    UserId = currentUserId,
                    MutedUserId = userId,
                    Id = id,
                });
                result.SetSuccess(id);
            }
            catch (Exception e)
            {
                result.SetError($"Method:{nameof(UserService)}/{nameof(AddToMuteListAsync)} exception:", "Error", e);
            }

            return Task<AOResult<int>>.Factory.StartNew(() => result);
        }

        public Task<AOResult<bool>> IsUserBlocked(int currentUserId, int userId)
        {
            var result = new AOResult<bool>();
            try
            {
                if (_mockService.BlackList.FirstOrDefault(x => x.UserId == currentUserId && x.BlockedUserId == userId) != null)
                {
                    result.SetSuccess(true);
                }
                else
                {
                    result.SetSuccess(false);
                }
            }
            catch (Exception e)
            {
                result.SetError($"Method:{nameof(UserService)}/{nameof(IsUserBlocked)} exception:", "Error", e);
            }

            return Task<AOResult<bool>>.Factory.StartNew(() => result);
        }

        public Task<AOResult<bool>> IsUserMuted(int currentUserId, int userId)
        {
            var result = new AOResult<bool>();
            try
            {
                if (_mockService.MuteList.FirstOrDefault(x => x.UserId == currentUserId && x.MutedUserId == userId) != null)
                {
                    result.SetSuccess(true);
                }
                else
                {
                    result.SetSuccess(false);
                }
            }
            catch (Exception e)
            {
                result.SetError($"Method:{nameof(UserService)}/{nameof(IsUserMuted)} exception:", "Error", e);
            }

            return Task<AOResult<bool>>.Factory.StartNew(() => result);
        }

        public Task<AOResult<int>> RemoveFromBlacklistAsync(int currentUserId, int userId)
        {
            var result = new AOResult<int>();
            try
            {
                var removing = _mockService.BlackList.FirstOrDefault(x => x.UserId == currentUserId && x.BlockedUserId == userId);
                if (removing != null)
                {
                    _mockService.BlackList.Remove(removing);
                    result.SetSuccess(removing.Id);
                }
                else
                {
                    result.SetFailure("No such user in blacklist");
                }
            }
            catch (Exception e)
            {
                result.SetError($"Method:{nameof(UserService)}/{nameof(RemoveFromBlacklistAsync)} exception:", "Error", e);
            }

            return Task<AOResult<int>>.Factory.StartNew(() => result);
        }

        public Task<AOResult<int>> RemoveFromMutelistAsync(int currentUserId, int userId)
        {
            var result = new AOResult<int>();
            try
            {
                var removing = _mockService.MuteList.FirstOrDefault(x => x.UserId == currentUserId && x.MutedUserId == userId);
                if (removing != null)
                {
                    _mockService.MuteList.Remove(removing);
                    result.SetSuccess(removing.Id);
                }
                else
                {
                    result.SetFailure("No such user in mutelist");
                }
            }
            catch (Exception e)
            {
                result.SetError($"Method:{nameof(UserService)}/{nameof(RemoveFromMutelistAsync)} exception:", "Error", e);
            }

            return Task<AOResult<int>>.Factory.StartNew(() => result);
        }

        public Task<AOResult<List<MuteModel>>> GetAllMutedUsersAsync()
        {
            throw new NotImplementedException();
        }

        public Task<AOResult<List<UserModel>>> GetAllBlockedUsersAsync()
        {
            throw new NotImplementedException();
        }

        #endregion

    }
}
