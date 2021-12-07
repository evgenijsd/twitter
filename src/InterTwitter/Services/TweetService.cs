using InterTwitter.Helpers;
using InterTwitter.Models;
using InterTwitter.Models.TweetViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InterTwitter.Services
{
    public class TweetService : ITweetService
    {
        private readonly IMockService _mockService;
        public TweetService(IMockService mockService)
        {
            _mockService = mockService;
        }

        #region -- ITweetService implementation --
        public Task<AOResult<IEnumerable<BaseTweetViewModel>>> GetTweetAsync(int userId)
        {
            var result = new AOResult<IEnumerable<BaseTweetViewModel>>();
            try
            {
                var tweets = _mockService.Tweets.Where(x => x.UserId == userId);
                foreach (var item in tweets)
                {

                }
            }
            catch (Exception)
            {

                throw;
            }
        }
        #endregion

    }
}
