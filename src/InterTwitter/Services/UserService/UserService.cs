using InterTwitter.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InterTwitter.Services.UserService
{
    public class UserService : IUserService
    {
        private readonly IMockService _mockService;
        public UserService(IMockService mockService)
        {
            _mockService = mockService;
        }

        #region -- IUserService Implementation --

        public Task DeleteUser(int id)
        {
            _mockService.Users.Remove(_mockService.Users.FirstOrDefault(x => x.Id == id));
            return Task.CompletedTask;
        }

        public Task<List<UserModel>> GetAllUsers()
        {
            return Task<List<UserModel>>.Factory.StartNew(() => _mockService.Users);
        }

        public Task<UserModel> GetUser(int id)
        {
            return Task<UserModel>.Factory.StartNew(() => _mockService.Users.FirstOrDefault(x => x.Id == id));
        }

        public Task InsertUser(UserModel user)
        {
            _mockService.Users.Add(user);
            return Task.CompletedTask;
        }

        public Task UpdateUser(UserModel user)
        {
           var v = _mockService.Users.FirstOrDefault(x => x.Id == user.Id);
           _mockService.Users.Remove(v);
           _mockService.Users.Add(user);

           return Task.CompletedTask;
        }

        #endregion

    }
}
