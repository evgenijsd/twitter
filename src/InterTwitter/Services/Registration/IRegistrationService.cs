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
        Task<AOResult<int>> UserAddAsync(UserModel user);
        Task<AOResult<bool>> CheckTheCorrectEmailAsync(string email);
        List<UserModel> GetUsers();
    }
}
