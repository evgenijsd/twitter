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
                var oldUser = _mockService.Users.FirstOrDefault(x => x.Id == user.Id);
                _mockService.Users.Remove(oldUser);
                _mockService.Users.Add(user);
                _mockService.Users.Sort((x1, x2) => x1.Id.CompareTo(x2.Id));
                result.SetSuccess(user.Id);
            }
            catch (Exception e)
            {
                result.SetError($"Method:{nameof(UserService)}/{nameof(UpdateUserAsync)} exception:", "Error", e);
            }

            return Task<AOResult<int>>.Factory.StartNew(() => result);
        }

        //public Task DeleteUser(int id)
        //{
        //    _mockService.Users.Remove(_mockService.Users.FirstOrDefault(x => x.Id == id));
        //    return Task.CompletedTask;
        //}

        //public Task<List<UserModel>> GetAllUsers()
        //{
        //    return Task<List<UserModel>>.Factory.StartNew(() => _mockService.Users);
        //}

        //public Task<UserModel> GetUser(int id)
        //{
        //    return Task<UserModel>.Factory.StartNew(() => _mockService.Users.FirstOrDefault(x => x.Id == id));
        //}

        //public Task InsertUser(UserModel user)
        //{
        //    _mockService.Users.Add(user);
        //    return Task.CompletedTask;
        //}

        //public Task UpdateUser(UserModel user)
        //{
        //   var v = _mockService.Users.FirstOrDefault(x => x.Id == user.Id);
        //   _mockService.Users.Remove(v);
        //   _mockService.Users.Add(user);

        //   return Task.CompletedTask;
        //}
        #endregion

    }
}
