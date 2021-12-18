using InterTwitter.Helpers;
using InterTwitter.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InterTwitter.Services
{
    public class TweetService : ITweetService
    {
        private readonly IMockService _mockService;

        public TweetService(
            IMockService mockService)
        {
            _mockService = mockService;
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

        public async Task<AOResult<IEnumerable<TweetModel>>> GetAllTweetsByHashtagsOrKeysAsync(IEnumerable<string> keys)
        {
            var result = new AOResult<IEnumerable<TweetModel>>();

            try
            {
                var allTweets = _mockService.Tweets;

                var foundTweets = allTweets.Where(tweet => keys
                     .Any(key => tweet.Text?
                    .IndexOf(key, StringComparison.OrdinalIgnoreCase) > -1));

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

        public Task<AOResult<UserModel>> GetAuthorAsync(int authorId)
        {
            var result = new AOResult<UserModel>();

            try
            {
                var author = _mockService.Users?.Where(x => x.Id == authorId)?.FirstOrDefault();
                if (author != null)
                {
                    result.SetSuccess(author);
                }
                else
                {
                    result.SetFailure("not found any user");
                }
            }
            catch (Exception ex)
            {
                result.SetError($"{nameof(GetAuthorAsync)}: exception", "Some issues", ex);
            }

            return Task.FromResult(result);
        }

        #endregion

    }
}
