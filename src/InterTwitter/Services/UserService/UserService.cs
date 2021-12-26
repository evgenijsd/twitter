using InterTwitter.Helpers;
using InterTwitter.Models;
using InterTwitter.Services.Settings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InterTwitter.Services.UserService
{
    public class UserService : IUserService
    {
        private readonly IMockService _mockService;
        private readonly ISettingsManager _settingsManager;
        private readonly int _currentUserId;

        public UserService(IMockService mockService, ISettingsManager settingsManager)
        {
            _mockService = mockService;
            _settingsManager = settingsManager;
            _currentUserId = _settingsManager.UserId;
        }

        #region -- IUserService Implementation --

        public Task<AOResult> DeleteUserAsync(UserModel user)
        {
            var result = new AOResult();
            try
            {
                _mockService.Users.Remove(user);
                result.SetSuccess();
            }
            catch (Exception ex)
            {
                result.SetError($"{nameof(DeleteUserAsync)} : exception", "Something went wrong", ex);
            }

            return Task.FromResult(result);
        }

        public Task<AOResult<IEnumerable<UserModel>>> GetAllUsersAsync()
        {
            var result = new AOResult<IEnumerable<UserModel>>();
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
            catch (Exception ex)
            {
                result.SetError($"{nameof(GetAllUsersAsync)} : exception", "Something went wrong", ex);
            }

            return Task.FromResult(result);
        }

        public Task<AOResult<UserModel>> GetUserAsync(int id)
        {
            var result = new AOResult<UserModel>();
            try
            {
                var user = _mockService.Users?.FirstOrDefault(x => x.Id == id);
                if (user != null)
                {
                    result.SetSuccess(user);
                }
                else
                {
                    result.SetFailure("User not found!");
                }
            }
            catch (Exception ex)
            {
                result.SetError($"{nameof(GetUserAsync)} : exception", "Something went wrong", ex);
            }

            return Task<AOResult<UserModel>>.FromResult(result);
        }

        public Task<AOResult> InsertUserAsync(UserModel user)
        {
            var result = new AOResult();
            try
            {
                var sameUser = _mockService.Users?.FirstOrDefault(x => x.Email == user.Email);
                if (sameUser == null)
                {
                    var lastId = _mockService.Users.OrderBy(x => x.Id).LastOrDefault()?.Id;
                    user.Id = (int)++lastId;
                    _mockService.Users.Add(user);
                    result.SetSuccess();
                }
                else
                {
                    result.SetFailure("Such user already exists");
                }
            }
            catch (Exception ex)
            {
                result.SetError($"{nameof(InsertUserAsync)} : exception", "Something went wrong", ex);
            }

            return Task.FromResult(result);
        }

        public Task<AOResult> UpdateUserAsync(UserModel user)
        {
            var result = new AOResult();
            try
            {
                var oldUser = _mockService.Users?.FirstOrDefault(x => x.Id == user.Id);
                _mockService.Users?.Remove(oldUser);
                _mockService.Users?.Add(user);
                _mockService.Users = _mockService.Users.OrderBy(x => x.Id).ToList();
                result.SetSuccess();
            }
            catch (Exception ex)
            {
                result.SetError($"{nameof(UpdateUserAsync)} : exception", "Something went wrong", ex);
            }

            return Task.FromResult(result);
        }

        public Task<AOResult> AddToBlacklistAsync(int userId)
        {
            var result = new AOResult();
            try
            {
                var id = 1;
                if (_mockService.BlackList.Count() > 0)
                {
                    id = (int)_mockService.BlackList.Max(x => x.Id);
                    id++;
                }

                _mockService.BlackList.Add(new BlockModel
                {
                    UserId = _currentUserId,
                    BlockedUserId = userId,
                    Id = id,
                });
                result.SetSuccess();
            }
            catch (Exception ex)
            {
                result.SetError($"{nameof(AddToBlacklistAsync)} : exception", "Something went wrong", ex);
            }

            return Task.FromResult(result);
        }

        public Task<AOResult> AddToMuteListAsync(int userId)
        {
            var result = new AOResult();
            try
            {
                var id = 1;
                if (_mockService.MuteList.Count() > 0)
                {
                    id = (int)_mockService.MuteList.Max(x => x.Id);
                }

                _mockService.MuteList.Add(new MuteModel
                {
                    UserId = _currentUserId,
                    MutedUserId = userId,
                    Id = id,
                });
                result.SetSuccess();
            }
            catch (Exception ex)
            {
                result.SetError($"{nameof(AddToMuteListAsync)} : exception", "Something went wrong", ex);
            }

            return Task.FromResult(result);
        }

        public Task<AOResult<bool>> CheckIfUserIsBlockedAsync(int userId)
        {
            var result = new AOResult<bool>();
            try
            {
                if (_mockService.BlackList.Any(x => x.UserId == _currentUserId && x.BlockedUserId == userId))
                {
                    result.SetSuccess(true);
                }
                else
                {
                    result.SetSuccess(false);
                }
            }
            catch (Exception ex)
            {
                result.SetError($"{nameof(CheckIfUserIsBlockedAsync)} : exception", "Something went wrong", ex);
            }

            return Task.FromResult(result);
        }

        public Task<AOResult<bool>> CheckIfUserIsMutedAsync(int userId)
        {
            var result = new AOResult<bool>();
            try
            {
                if (_mockService.MuteList.Any(x => x.UserId == _currentUserId && x.MutedUserId == userId))
                {
                    result.SetSuccess(true);
                }
                else
                {
                    result.SetSuccess(false);
                }
            }
            catch (Exception ex)
            {
                result.SetError($"{nameof(CheckIfUserIsMutedAsync)} : exception", "Something went wrong", ex);
            }

            return Task.FromResult(result);
        }

        public Task<AOResult> RemoveFromBlacklistAsync(int userId)
        {
            var result = new AOResult();
            try
            {
                var userToRemove = _mockService.BlackList.FirstOrDefault(x => x.UserId == _currentUserId && x.BlockedUserId == userId);
                if (userToRemove != null)
                {
                    _mockService.BlackList.Remove(userToRemove);
                    result.SetSuccess();
                }
                else
                {
                    result.SetFailure("No such user in blacklist");
                }
            }
            catch (Exception ex)
            {
                result.SetError($"{nameof(RemoveFromBlacklistAsync)} : exception", "Something went wrong", ex);
            }

            return Task.FromResult(result);
        }

        public Task<AOResult> RemoveFromMutelistAsync(int userId)
        {
            var result = new AOResult();
            try
            {
                var userToRemove = _mockService.MuteList.FirstOrDefault(x => x.UserId == _currentUserId && x.MutedUserId == userId);
                if (userToRemove != null)
                {
                    _mockService.MuteList.Remove(userToRemove);
                    result.SetSuccess();
                }
                else
                {
                    result.SetFailure("No such user in mutelist");
                }
            }
            catch (Exception ex)
            {
                result.SetError($"{nameof(RemoveFromMutelistAsync)} : exception", "Something went wrong", ex);
            }

            return Task.FromResult(result);
        }

        public Task<AOResult<IEnumerable<UserModel>>> GetAllMutedUsersAsync()
        {
            var result = new AOResult<IEnumerable<UserModel>>();
            try
            {
                var muteList = _mockService.MuteList.Where(x => x.UserId == _currentUserId);

                var resultList = _mockService.Users.Where(x => muteList.Any(u => u.MutedUserId == x.Id));

                if (muteList.Count() > 0)
                {
                    result.SetSuccess(resultList);
                }
                else
                {
                    result.SetFailure("Mutelist is empty");
                }
            }
            catch (Exception ex)
            {
                result.SetError($"{nameof(GetAllMutedUsersAsync)} : exception", "Something went wrong", ex);
            }

            return Task.FromResult(result);
        }

        public Task<AOResult<IEnumerable<UserModel>>> GetAllBlockedUsersAsync()
        {
            var result = new AOResult<IEnumerable<UserModel>>();
            try
            {
                var blacklist = _mockService.BlackList.Where(x => x.UserId == _currentUserId);

                var resultList = _mockService.Users.Where(x => blacklist.Any(u => x.Id == u.BlockedUserId));

                if (blacklist.Count() > 0)
                {
                    result.SetSuccess(resultList);
                }
                else
                {
                    result.SetFailure("Blacklis is empty");
                }
            }
            catch (Exception ex)
            {
                result.SetError($"{nameof(GetAllBlockedUsersAsync)} : exception", "Something went wrong", ex);
            }

            return Task.FromResult(result);
        }

        #endregion

    }
}
