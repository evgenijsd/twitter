﻿using InterTwitter.Helpers.ProcessHelpers;
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
                var tweets = await _mockService.GetAllAsync<TweetModel>();

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
                var tweets = await _mockService.GetAsync<TweetModel>(x => x.UserId == userid);

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

        public async Task<AOResult<IEnumerable<TweetModel>>> FindTweetsByKeywordsAsync(IEnumerable<string> keys)
        {
            var result = new AOResult<IEnumerable<TweetModel>>();

            try
            {
                var allTweets = await _mockService.GetAllAsync<TweetModel>();

                if (allTweets != null)
                {
                    var foundTweets = allTweets.Where(tweet => keys
                        .Any(key => tweet.Text?
                        .IndexOf(key, StringComparison.OrdinalIgnoreCase) > -1));

                    if (foundTweets?.FirstOrDefault() != null)
                    {
                        result.SetSuccess(foundTweets.OrderByDescending(x => x.CreationTime));
                    }
                }
            }
            catch (Exception ex)
            {
                result.SetError($"{nameof(FindTweetsByKeywordsAsync)}: exception", "Some issues", ex);
            }

            return result;
        }

        public async Task<AOResult<UserModel>> GetAuthorAsync(int authorId)
        {
            var result = new AOResult<UserModel>();

            try
            {
                var author = await _mockService.FindAsync<UserModel>(x => x.Id == authorId);
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
