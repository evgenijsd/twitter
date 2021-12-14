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
        private List<LikeModel> _likes;

        public LikeService()
        {
            _likes = new List<LikeModel>
            {
                new LikeModel
                {
                    Id = 1,
                    UserId = 1,
                    TweetId = 1,
                },
                new LikeModel
                {
                    Id = 2,
                    UserId = 1,
                    TweetId = 2,
                },
                new LikeModel
                {
                    Id = 3,
                    UserId = 1,
                    TweetId = 3,
                },
                new LikeModel
                {
                    Id = 4,
                    UserId = 1,
                    TweetId = 4,
                },
                new LikeModel
                {
                    Id = 5,
                    UserId = 1,
                    TweetId = 5,
                },
                new LikeModel
                {
                    Id = 6,
                    UserId = 2,
                    TweetId = 1,
                },
                new LikeModel
                {
                    Id = 7,
                    UserId = 2,
                    TweetId = 2,
                },
                new LikeModel
                {
                    Id = 8,
                    UserId = 2,
                    TweetId = 3,
                },
                new LikeModel
                {
                    Id = 9,
                    UserId = 2,
                    TweetId = 4,
                },
                new LikeModel
                {
                    Id = 10,
                    UserId = 2,
                    TweetId = 5,
                },
                new LikeModel
                {
                    Id = 11,
                    UserId = 3,
                    TweetId = 1,
                },
                new LikeModel
                {
                    Id = 12,
                    UserId = 3,
                    TweetId = 2,
                },
                new LikeModel
                {
                    Id = 13,
                    UserId = 3,
                    TweetId = 3,
                },
                new LikeModel
                {
                    Id = 14,
                    UserId = 3,
                    TweetId = 4,
                },
                new LikeModel
                {
                    Id = 15,
                    UserId = 3,
                    TweetId = 5,
                },
            };
        }

        #region -- Public helpers --

        public List<LikeModel> GetLikes()
        {
            return _likes;
        }

        public async Task<AOResult<List<LikeModel>>> GetLikesAsync(int userId)
        {
            var result = new AOResult<List<LikeModel>>();

            try
            {
                var bookmarks = _likes.Where(x => x.UserId == userId).ToList();

                if (bookmarks != null)
                {
                    result.SetSuccess(bookmarks);
                }
                else
                {
                    result.SetFailure("not found any bookmark");
                }
            }
            catch (Exception ex)
            {
                result.SetError($"{nameof(GetLikesAsync)}: exception", "Some issues", ex);
            }

            return result;
        }

        public async Task<AOResult> DeleteLikeAsync(int tweetId, int userId)
        {
            var result = new AOResult();
            try
            {
                var like = _likes.FirstOrDefault(x => x.UserId == userId && x.TweetId == tweetId);

                if (like != null)
                {
                    result.SetSuccess();
                    _likes.Remove(like);
                }
                else
                {
                    result.SetFailure("not found any bookmark");
                }
            }
            catch (Exception ex)
            {
                result.SetError($"{nameof(DeleteLikeAsync)}: exception", "Some issues", ex);
            }

            return result;
        }

        public async Task<AOResult<int>> AddLikeAsync(int tweetId, int userId)
        {
            var result = new AOResult<int>();
            try
            {
                var like = new LikeModel
                {
                    Id = _likes.Count() + 1,
                    TweetId = tweetId,
                    UserId = userId,
                    Notification = true,
                };

                _likes.Add(like);
                int id = _likes.Last().Id;
                if (id > 0)
                {
                    result.SetSuccess(id);
                }
                else
                {
                    result.SetFailure();
                }
            }
            catch (Exception ex)
            {
                result.SetError($"Exception: {nameof(AddLikeAsync)}", "Wrong result", ex);
            }

            return result;
        }

        public async Task<AOResult> AnyAsync(int tweetId, int userId)
        {
            var result = new AOResult();
            try
            {
                var any = _likes.Any(x => x.TweetId == tweetId && x.UserId == userId);
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
                result.SetError($"Exception: {nameof(AnyAsync)}", "Wrong result", ex);
            }

            return result;
        }

        public async Task<AOResult<int>> CountAsync(int tweetId)
        {
            var result = new AOResult<int>();
            try
            {
                var count = _likes.Count(x => x.TweetId == tweetId);
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
                result.SetError($"Exception: {nameof(CountAsync)}", "Wrong result", ex);
            }

            return result;
        }

        #endregion
    }
}