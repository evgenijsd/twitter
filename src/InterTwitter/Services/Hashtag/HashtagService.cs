using InterTwitter.Helpers.ProcessHelpers;
using InterTwitter.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InterTwitter.Services.Hashtag
{
    public class HashtagService : IHashtagService
    {
        private readonly IMockService _mockService;

        public HashtagService(IMockService mockService)
        {
            _mockService = mockService;
        }

        #region -- IHashtagManager implementation --

        public async Task<AOResult> IncreaseHashtagPopularityByOne(HashtagModel hashtag)
        {
            var result = new AOResult();

            try
            {
                var allHashtags = new List<HashtagModel>(_mockService.Hashtags);

                if (allHashtags != null)
                {
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
            }
            catch (Exception ex)
            {
                result.SetError($"{nameof(IncreaseHashtagPopularityByOne)} : exception", "Something went wrong", ex);
            }

            return result;
        }

        public async Task<AOResult> DecreaseHashtagPopularityByOne(HashtagModel hashtag)
        {
            var result = new AOResult();

            try
            {
                var allHashtags = new List<HashtagModel>(_mockService.Hashtags);

                if (allHashtags != null)
                {
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
            }
            catch (Exception ex)
            {
                result.SetError($"{nameof(DecreaseHashtagPopularityByOne)} : exception", "Something went wrong", ex);
            }

            return result;
        }

        public async Task<AOResult<IEnumerable<HashtagModel>>> GetPopularHashtags(int numbersOfHashtags)
        {
            var result = new AOResult<IEnumerable<HashtagModel>>();

            try
            {
                var allHashtags = _mockService.Hashtags;

                var popularHashtags = allHashtags
                    ?.OrderByDescending(x => x.TweetsCount)
                    ?.Take(numbersOfHashtags);

                if (popularHashtags is not null)
                {
                    result.SetSuccess(popularHashtags);
                }
            }
            catch (Exception ex)
            {
                result.SetError($"{nameof(GetPopularHashtags)} : exception", "Something went wrong", ex);
            }

            return result;
        }

        #endregion
    }
}