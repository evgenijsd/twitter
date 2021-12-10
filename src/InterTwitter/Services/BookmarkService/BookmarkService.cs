using InterTwitter.Helpers;
using InterTwitter.Models;
using Prism.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InterTwitter.Services.BookmarkService
{
    public class BookmarkService : IBookmarkService
    {
        private List<Bookmark> _bookmarks;
        private IEventAggregator _event;

        public BookmarkService(IEventAggregator aggregator)
        {
            _event = aggregator;

            _bookmarks = new List<Bookmark>
            {
                new Bookmark
                {
                    Id = 2,
                    UserId = 1,
                    TweetId = 2,
                },
                new Bookmark
                {
                    Id = 3,
                    UserId = 1,
                    TweetId = 3,
                },
                new Bookmark
                {
                    Id = 4,
                    UserId = 1,
                    TweetId = 4,
                },
                new Bookmark
                {
                    Id = 5,
                    UserId = 1,
                    TweetId = 5,
                },
                new Bookmark
                {
                    Id = 6,
                    UserId = 2,
                    TweetId = 1,
                },
                new Bookmark
                {
                    Id = 7,
                    UserId = 2,
                    TweetId = 2,
                },
                new Bookmark
                {
                    Id = 8,
                    UserId = 2,
                    TweetId = 3,
                },
                new Bookmark
                {
                    Id = 9,
                    UserId = 2,
                    TweetId = 4,
                },
                new Bookmark
                {
                    Id = 10,
                    UserId = 2,
                    TweetId = 5,
                },
                new Bookmark
                {
                    Id = 11,
                    UserId = 3,
                    TweetId = 1,
                },
                new Bookmark
                {
                    Id = 12,
                    UserId = 3,
                    TweetId = 2,
                },
                new Bookmark
                {
                    Id = 13,
                    UserId = 3,
                    TweetId = 3,
                },
                new Bookmark
                {
                    Id = 14,
                    UserId = 3,
                    TweetId = 4,
                },
                new Bookmark
                {
                    Id = 15,
                    UserId = 3,
                    TweetId = 5,
                },
            };
        }

        #region -- Public helpers --
        public List<Bookmark> GetBookmarks()
        {
            return _bookmarks;
        }

        public async Task<AOResult<List<Bookmark>>> GetBookmarksAsync(int userId)
        {
            var result = new AOResult<List<Bookmark>>();

            try
            {
                var bookmarks = _bookmarks.Where(x => x.UserId == userId).ToList();

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
                result.SetError($"{nameof(GetBookmarksAsync)}: exception", "Some issues", ex);
            }

            return result;
        }

        public async Task<AOResult> DeleteAllBookmarksAsync(int userId)
        {
            var result = new AOResult();
            try
            {
                var bookmarks = _bookmarks.Where(x => x.UserId == userId);

                if (bookmarks != null)
                {
                    result.SetSuccess();
                }
                else
                {
                    result.SetFailure("not found any bookmark");
                }

                foreach (var bookmark in bookmarks)
                {
                    _bookmarks.Remove(bookmark);
                }
            }
            catch (Exception ex)
            {
                result.SetError($"{nameof(DeleteAllBookmarksAsync)}: exception", "Some issues", ex);
            }

            return result;
        }

        public async Task<AOResult> DeleteBoormarkAsync(int tweetId, int userId)
        {
            var result = new AOResult();
            try
            {
                var bookmark = _bookmarks.FirstOrDefault(x => x.UserId == userId && x.TweetId == tweetId);

                if (bookmark != null)
                {
                    result.SetSuccess();
                }
                else
                {
                    result.SetFailure("not found any bookmark");
                }

                _bookmarks.Remove(bookmark);
                _event.GetEvent<DeleteBookmarkEvent>().Publish(tweetId);
            }
            catch (Exception ex)
            {
                result.SetError($"{nameof(DeleteBoormarkAsync)}: exception", "Some issues", ex);
            }

            return result;
        }

        #endregion
    }
}
