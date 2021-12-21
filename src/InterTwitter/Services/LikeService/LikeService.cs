using InterTwitter.Helpers;
using InterTwitter.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InterTwitter.Services.LikeService
{
    public class LikeService : ILikeService
    {
        private readonly IMockService _mockService;

        public LikeService(IMockService mockService)
        {
            _mockService = mockService;
        }

        #region -- Public helpers --

        public async Task<AOResult<List<LikeModel>>> GetLikesAsync(int userId)
        {
            var result = new AOResult<List<LikeModel>>();

            try
            {
                var likes = _mockService.Likes.Where(x => x.UserId == userId).ToList();

                if (likes != null)
                {
                    result.SetSuccess(likes);
                }
                else
                {
                    result.SetFailure(Resources.Resource.NotFoundBookmark);
                }
            }
            catch (Exception ex)
            {
                result.SetError($"{nameof(GetLikesAsync)}: exception", Resources.Resource.SomeIssues, ex);
            }

            return result;
        }

        public async Task<AOResult> DeleteLikeAsync(int tweetId, int userId)
        {
            var result = new AOResult();
            try
            {
                var like = _mockService.Likes.FirstOrDefault(x => x.UserId == userId && x.TweetId == tweetId);

                if (like != null)
                {
                    result.SetSuccess();
                    _mockService.Likes.Remove(like);
                }
                else
                {
                    result.SetFailure(Resources.Resource.NotFoundBookmark);
                }
            }
            catch (Exception ex)
            {
                result.SetError($"{nameof(DeleteLikeAsync)}: exception", Resources.Resource.SomeIssues, ex);
            }

            return result;
        }

        public async Task<AOResult<int>> AddLikeAsync(int tweetId, int userId)
        {
            var result = new AOResult<int>();
            try
            {
                if (!_mockService.Likes.Any(x => x.TweetId == tweetId && x.UserId == userId))
                {
                    var like = new LikeModel
                    {
                        Id = _mockService.Likes.Count() + 1,
                        TweetId = tweetId,
                        UserId = userId,
                        Notification = true,
                    };

                    _mockService.Likes.Add(like);
                    int id = _mockService.Likes.Last().Id;
                    if (id > 0)
                    {
                        result.SetSuccess(id);
                    }
                }
                else
                {
                    result.SetFailure();
                }
            }
            catch (Exception ex)
            {
                result.SetError($"Exception: {nameof(AddLikeAsync)}", Resources.Resource.WrongResult, ex);
            }

            return result;
        }

        public async Task<AOResult> AnyAsync(int tweetId, int userId)
        {
            var result = new AOResult();
            try
            {
                var any = _mockService.Likes.Any(x => x.TweetId == tweetId && x.UserId == userId);
                if (any)
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
                result.SetError($"Exception: {nameof(AnyAsync)}", Resources.Resource.WrongResult, ex);
            }

            return result;
        }

        public async Task<AOResult<int>> CountAsync(int tweetId)
        {
            var result = new AOResult<int>();
            try
            {
                var count = _mockService.Likes.Count(x => x.TweetId == tweetId);
                if (count >= 0)
                {
                    result.SetSuccess(count);
                }
                else
                {
                    result.SetFailure();
                }
            }
            catch (Exception ex)
            {
                result.SetError($"Exception: {nameof(CountAsync)}", Resources.Resource.WrongResult, ex);
            }

            return result;
        }

        #endregion
    }
}