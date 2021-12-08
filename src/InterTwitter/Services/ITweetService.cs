﻿using InterTwitter.Helpers;
using InterTwitter.Models;
using InterTwitter.Models.TweetViewModel;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace InterTwitter.Services
{
    public interface ITweetService
    {
        Task<AOResult<IEnumerable<BaseTweetViewModel>>> GetUserTweetsAsync(int userId);

        Task<AOResult<IEnumerable<BaseTweetViewModel>>> GetAllTweetsAsync();
        Task<AOResult<UserModel>> GetUserAsync(int userId);
    }
}