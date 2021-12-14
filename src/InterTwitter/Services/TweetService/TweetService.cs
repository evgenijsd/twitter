using InterTwitter.Helpers;
using InterTwitter.Models;
using InterTwitter.Services.MockService;
using InterTwitter.Services.SettingsManager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InterTwitter.Services.TweetService
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

        public async Task<AOResult<IEnumerable<TweetModel>>> GetAllTweetsAsync()
        {
            await Task.Delay(50);
            var result = new AOResult<IEnumerable<TweetModel>>();
            try
            {
                var tweets = _mockService.Tweets;

                if (tweets != null)
                {
                    result.SetSuccess(tweets.OrderByDescending(x => x.CreationTime));
                }
                else
                {
                    result.SetFailure("No tweets found");
                }
            }
            catch (Exception ex)
            {
                result.SetError($"{nameof(GetAllTweetsAsync)}: exception", "Some issues", ex);
            }

            return result;
        }

        public async Task<AOResult<IEnumerable<TweetModel>>> GetAllTweetsByHashtagsOrKeysAsync(string searchQuery)
        {
            var result = new AOResult<IEnumerable<TweetModel>>();
            var keys = searchQuery.ToLower().Split(' ', '\t').Distinct();

            try
            {
                var allTweets = _mockService.Tweets;
                /* var test = allTweets.Where(tweet => keys
                    .Any(key => tweet.Text != null && tweet.Text.ToLower()
                    .Contains(key))); */

                var foundTweets = allTweets.Where(tweet => keys
                    .Any(key => tweet.Text
                    ?.IndexOf(key, StringComparison.OrdinalIgnoreCase) > -1));

                if (foundTweets?.FirstOrDefault() != null)
                {
                    result.SetSuccess(foundTweets.OrderByDescending(x => x.CreationTime));
                }
            }
            catch (Exception ex)
            {
                result.SetError($"{nameof(GetAllTweetsByHashtagsOrKeysAsync)}: exception", "Some issues", ex);
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