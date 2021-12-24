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

        public async Task<AOResult<List<TweetModel>>> GetAllTweetsAsync()
        {
            await Task.Delay(50);

            var result = new AOResult<List<TweetModel>>();
            try
            {
                var tweets = _mockService.Tweets;

                if (tweets != null)
                {
                    result.SetSuccess(tweets.OrderByDescending(x => x.CreationTime).ToList());
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

        public async Task<AOResult<List<TweetModel>>> GetByUserTweetsAsync(int userid)
        {
            var result = new AOResult<List<TweetModel>>();
            try
            {
                var tweets = _mockService.Tweets.Where(x => x.UserId == userid);

                if (tweets != null)
                {
                    result.SetSuccess(tweets.OrderByDescending(x => x.CreationTime).ToList());
                }
                else
                {
                    result.SetFailure("No tweets found");
                }
            }
            catch (Exception ex)
            {
                result.SetError($"{nameof(GetByUserTweetsAsync)}: exception", "Some issues", ex);
            }

            return result;
        }

        public async Task<AOResult<UserModel>> GetAuthorAsync(int authorId)
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

            return result;
        }

        #endregion

    }
}
