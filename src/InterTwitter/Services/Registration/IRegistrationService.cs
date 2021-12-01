using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using InterTwitter.Enums;
using InterTwitter.Helpers.ProcessHelpers;
using InterTwitter.Models;

namespace InterTwitter.Services.Registration
{
    public interface IRegistrationService
    {
        Task<int> UserAddAsync(User user);
        Task<AOResult<int>> CheckTheCorrectEmailAsync(string name, string email);
        ECheckEnter CheckTheCorrectPassword(string password, string confirmPassword);
        ECheckEnter CheckCorrectEmail(string email);
        List<User> GetUsers();
    }
}
