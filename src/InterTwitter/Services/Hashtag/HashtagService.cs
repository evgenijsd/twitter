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
                bool isSuccess = false;

                var allHashtags = await _mockService.GetAllAsync<HashtagModel>();

                if (allHashtags != null)
                {
                    var hashtagModel = allHashtags.FirstOrDefault(x => x.Text.Equals(hashtag, StringComparison.OrdinalIgnoreCase));

                    if (hashtagModel != null)
                    {
                        hashtagModel.TweetsCount++;

                        await _mockService.UpdateAsync(hashtagModel);

                        isSuccess = true;
                    }
                    else
                    {
                        hashtagModel = new HashtagModel()
                        {
                            Text = hashtag,

                            TweetsCount = 1,
                        };

                        await _mockService.AddAsync(hashtagModel);

                        isSuccess = true;
                    }
                }

                if (isSuccess)
                {
                    result.SetSuccess();
                }
                else
                {
                    result.SetFailure();
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
                var allHashtags = await _mockService.GetAllAsync<HashtagModel>();

                bool isSuccess = false;

                if (allHashtags != null)
                {
                    var hashtagModel = allHashtags.FirstOrDefault(x => x.Text.Equals(hashtag, StringComparison.OrdinalIgnoreCase));

                    if (hashtagModel != null)
                    {
                        if (hashtagModel.TweetsCount > 0)
                        {
                            hashtagModel.TweetsCount--;

                            await _mockService.UpdateAsync(hashtagModel);

                            isSuccess = true;
                        }
                        else
                        {
                            await _mockService.RemoveAsync(hashtagModel);

                            isSuccess = true;
                        }
                    }
                }

                if (isSuccess)
                {
                    result.SetSuccess();
                }
                else
                {
                    result.SetFailure();
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
                var allHashtags = await _mockService.GetAllAsync<HashtagModel>();

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