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
            var result = new AOResult<IEnumerable<TweetModel>>();
            try
            {
                var tweets = await _mockService.GetAllTweetsAsync();

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

        public async Task<AOResult<UserModel>> GetAuthorAsync(int authorId)
        {
            var result = new AOResult<UserModel>();

            try
            {
                var author = await _mockService?.GetTweetAuthorAsync(authorId);
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

            return result;
        }

        #endregion

    }
}
