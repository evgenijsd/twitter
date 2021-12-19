﻿using InterTwitter.Helpers;
using InterTwitter.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InterTwitter.Services.HashtagManager
{
    public class HashtagManager : IHashtagManager
    {
        private IMockService _mockService;

        public HashtagManager(IMockService mockService)
        {
            _mockService = mockService;
        }

        #region -- IHashtagManager implementation --

        public async Task<AOResult<bool>> IncreaseHashtagPopularityByOne(HashtagModel hashtag)
        {
            var result = new AOResult<bool>();

            try
            {
                var allHashtags = new List<HashtagModel>(_mockService.Hashtags);

                int indexOfHashtag = allHashtags.FindIndex(x => x.Text.Equals(hashtag.Text, StringComparison.OrdinalIgnoreCase));

                if (indexOfHashtag > 0)
                {
                    allHashtags[indexOfHashtag].TweetsCount++;
                }
                else
                {
                    hashtag.TweetsCount = 1;
                    allHashtags.Add(hashtag);
                }

                _mockService.Hashtags = allHashtags;
            }
            catch (Exception e)
            {
                result.SetError($"Caller: {nameof(HashtagManager)}.{nameof(IncreaseHashtagPopularityByOne)}", e.Message, e);
            }

            return result;
        }

        public async Task<AOResult<bool>> DecreaseHashtagPopularityByOne(HashtagModel hashtag)
        {
            var result = new AOResult<bool>();

            try
            {
                var allHashtags = new List<HashtagModel>(_mockService.Hashtags);

                int indexOfHashtag = allHashtags.FindIndex(x => x.Text.Equals(hashtag.Text, StringComparison.OrdinalIgnoreCase));

                if (indexOfHashtag > 0)
                {
                    if (allHashtags[indexOfHashtag].TweetsCount > 0)
                    {
                        allHashtags[indexOfHashtag].TweetsCount--;
                    }
                    else
                    {
                        allHashtags.RemoveAt(indexOfHashtag);
                    }
                }

                _mockService.Hashtags = allHashtags;
            }
            catch (Exception e)
            {
                result.SetError($"Caller: {nameof(HashtagManager)}.{nameof(DecreaseHashtagPopularityByOne)}", e.Message, e);
            }

            return result;
        }

        public async Task<AOResult<IEnumerable<HashtagModel>>> GetPopularHashtags(int numbersOfHashtags)
        {
            var result = new AOResult<IEnumerable<HashtagModel>>();

            try
            {
                var hashtags = _mockService.Hashtags;

                var popularHashtags = hashtags
                    ?.OrderByDescending(x => x.TweetsCount)
                    ?.Take(numbersOfHashtags);

                if (popularHashtags is not null)
                {
                    result.SetSuccess(popularHashtags);
                }
            }
            catch (Exception e)
            {
                result.SetError($"Caller: {nameof(HashtagManager)}.{nameof(GetPopularHashtags)}", e.Message, e);
            }

            return result;
        }

        #endregion
    }
}