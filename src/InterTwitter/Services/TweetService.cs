using InterTwitter.Extensions;
using InterTwitter.Helpers;
using InterTwitter.Models;
using InterTwitter.Models.TweetViewModel;
using InterTwitter.Services.Settings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InterTwitter.Services
{
    public class TweetService : ITweetService
    {
        private readonly IMockService _mockService;
        private readonly ISettingsManager _settingsManager;
        public TweetService(
            IMockService mockService,
            ISettingsManager settingsManager)
        {
            _mockService = mockService;
            _settingsManager = settingsManager;
        }

        #region -- ITweetService implementation --

        public async Task<AOResult<IEnumerable<BaseTweetViewModel>>> GetUserTweetsAsync(int userId)
        {
            var result = new AOResult<IEnumerable<BaseTweetViewModel>>();
            try
            {
                var tweets = _mockService.Tweets.Where(x => x.UserId == userId);

                var tweetViewModels = new List<BaseTweetViewModel>();
                foreach (var tweetModel in tweets)
                {
                    var user = await GetUserAsync(tweetModel.UserId);
                    if (user.IsSuccess)
                    {
                        tweetViewModels.Add(tweetModel.ToViewModel(user.Result));
                    }
                }

                if (tweetViewModels != null)
                {
                    result.SetSuccess(tweetViewModels.OrderByDescending(x => x.TweetModel.CreationTime));
                }
                else
                {
                    result.SetFailure("No tweets found");
                }
            }
            catch (Exception ex)
            {
                result.SetError($"{nameof(GetUserTweetsAsync)}: exception", "Some issues", ex);
            }

            return result;
        }

        public async Task<AOResult<IEnumerable<BaseTweetViewModel>>> GetAllTweetsAsync()
        {
            var result = new AOResult<IEnumerable<BaseTweetViewModel>>();
            try
            {
                var tweets = _mockService.Tweets;

                var tweetViewModels = new List<BaseTweetViewModel>();
                foreach (var tweetModel in tweets)
                {
                    var user = await GetUserAsync(tweetModel.UserId);
                    if (user.IsSuccess)
                    {
                        tweetViewModels.Add(tweetModel.ToViewModel(user.Result));
                    }
                }

                if (tweetViewModels != null)
                {
                    result.SetSuccess(tweetViewModels.OrderByDescending(x => x.TweetModel.CreationTime));
                }
                else
                {
                    result.SetFailure("No tweets found");
                }
            }
            catch (Exception ex)
            {
                result.SetError($"{nameof(GetUserTweetsAsync)}: exception", "Some issues", ex);
            }

            return result;
        }

        public async Task<AOResult<UserModel>> GetUserAsync(int userId)
        {
            var result = new AOResult<UserModel>();

            try
            {
                var user = _mockService.Users.Where(x => x.Id == userId).FirstOrDefault();
                if (user != null)
                {
                    result.SetSuccess(user);
                }
                else
                {
                    result.SetFailure("not found any user");
                }
            }
            catch (Exception ex)
            {
                result.SetError($"{nameof(GetUserAsync)}: exception", "Some issues", ex);
            }

            return await Task.FromResult(result);
        }

        #endregion

    }
}
