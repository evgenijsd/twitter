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

        public async Task<AOResult> IncrementHashtagPopularity(string hashtag)
        {
            var result = new AOResult();

            try
            {
                var allHashtags = new List<HashtagModel>(_mockService.Hashtags);

                if (allHashtags != null)
                {
                    int indexOfHashtag = allHashtags.FindIndex(x => x.Text.Equals(hashtag, StringComparison.OrdinalIgnoreCase));

                    if (indexOfHashtag > 0)
                    {
                        allHashtags[indexOfHashtag].TweetsCount++;
                        result.SetSuccess();
                    }
                    else
                    {
                        HashtagModel hashtagModel = new HashtagModel()
                        {
                            Text = hashtag,
                            TweetsCount = 1,
                        };

                        allHashtags.Add(hashtagModel);
                        result.SetSuccess();
                    }

                    _mockService.Hashtags = allHashtags;
                }
            }
            catch (Exception ex)
            {
                result.SetError($"{nameof(IncrementHashtagPopularity)} : exception", "Something went wrong", ex);
            }

            return result;
        }

        public async Task<AOResult> DecrementHashtagPopularity(string hashtag)
        {
            var result = new AOResult();

            try
            {
                var allHashtags = new List<HashtagModel>(_mockService.Hashtags);

                if (allHashtags != null)
                {
                    int indexOfHashtag = allHashtags.FindIndex(x => x.Text.Equals(hashtag, StringComparison.OrdinalIgnoreCase));

                    if (indexOfHashtag > 0)
                    {
                        if (allHashtags[indexOfHashtag].TweetsCount > 0)
                        {
                            allHashtags[indexOfHashtag].TweetsCount--;
                            result.SetSuccess();
                        }
                        else
                        {
                            allHashtags.RemoveAt(indexOfHashtag);
                            result.SetSuccess();
                        }
                    }

                    _mockService.Hashtags = allHashtags;
                }
            }
            catch (Exception ex)
            {
                result.SetError($"{nameof(DecrementHashtagPopularity)} : exception", "Something went wrong", ex);
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
                else
                {
                    result.SetFailure();
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