using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using InterTwitter.Helpers.ProcessHelpers;

namespace InterTwitter.Services.Autorization
{
    public interface IAutorizationService
    {
        int UserId { get; set; }

        Task<AOResult<int>> CheckUserAsync(string login, string password);
    }
}
