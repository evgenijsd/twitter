using InterTwitter.Helpers;
using InterTwitter.Models;
using InterTwitter.Services.Hashtag;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace InterTwitter.Services
{
    public class TweetService : ITweetService
    {
        private readonly IMockService _mockService;

        private readonly IHashtagService _hashtagService;

        public TweetService(
            IMockService mockService,
            IHashtagService hashtagService)
        {
            _mockService = mockService;
            _hashtagService = hashtagService;
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

        public Task<AOResult> AddTweetAsync(TweetModel tweet)
        {
            var result = new AOResult();

            try
            {
                ((List<TweetModel>)_mockService.Tweets).Add(tweet);

                var allHashtags = Constants.Methods.GetUniqueWords(tweet.Text).Where(x => Regex.IsMatch(x, Constants.RegexPatterns.HASHTAG_PATTERN));

                foreach (var tag in allHashtags)
                {
                    _hashtagService.IncreaseHashtagPopularityByOne(new HashtagModel()
                    {
                        Text = tag,
                    });
                }

                result.SetSuccess();
            }
            catch (Exception ex)
            {
                result.SetError($"{nameof(GetAllTweetsAsync)}: exception", "Some issues", ex);
            }

            return Task.FromResult(result);
        }

        #endregion

    }
}
