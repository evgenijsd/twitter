using InterTwitter.Helpers;
using InterTwitter.Helpers.ProcessHelpers;
using InterTwitter.Models;
using InterTwitter.Resources.Strings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InterTwitter.Services
{
    public class BookmarkService : IBookmarkService
    {
        private readonly IMockService _mockService;

        public BookmarkService(IMockService mockService)
        {
            _mockService = mockService;
        }

        #region -- Interface implementation --

        public async Task<AOResult<List<Bookmark>>> GetBookmarksAsync(int userId)
        {
            var result = new AOResult<List<Bookmark>>();

            try
            {
                var bookmarks = _mockService.Bookmarks.Where(x => x.UserId == userId).ToList();

                if (bookmarks != null)
                {
                    result.SetSuccess(bookmarks);
                }
                else
                {
                    result.SetFailure(Strings.NotFoundBookmark);
                }
            }
            catch (Exception ex)
            {
                result.SetError($"{nameof(GetBookmarksAsync)}: exception", Strings.SomeIssues, ex);
            }

            return result;
        }

        public async Task<AOResult<List<Bookmark>>> GetNotificationsAsync(int userId)
        {
            var result = new AOResult<List<Bookmark>>();

            try
            {
                var bookmarks = _mockService.Bookmarks.Where(x => x.UserId != userId && x.Notification).ToList();

                if (bookmarks != null)
                {
                    result.SetSuccess(bookmarks);
                }
                else
                {
                    result.SetFailure(Strings.NotFoundBookmark);
                }
            }
            catch (Exception ex)
            {
                result.SetError($"{nameof(GetNotificationsAsync)}: exception", Strings.SomeIssues, ex);
            }

            return result;
        }

        public async Task<AOResult> DeleteAllBookmarksAsync(int userId)
        {
            var result = new AOResult();
            try
            {
                var bookmark = _mockService.Bookmarks.FirstOrDefault(x => x.UserId == userId);

                if (bookmark != null)
                {
                    var bookmarksList = _mockService.Bookmarks.ToList();

                    bookmarksList.RemoveAll(x => x.UserId == userId);

                    _mockService.Bookmarks = bookmarksList;

                    result.SetSuccess();
                }
                else
                {
                    result.SetFailure(Strings.NotFoundBookmark);
                }
            }
            catch (Exception ex)
            {
                result.SetError($"{nameof(DeleteAllBookmarksAsync)}: exception", Strings.SomeIssues, ex);
            }

            return result;
        }

        public async Task<AOResult> DeleteBoormarkAsync(int tweetId, int userId)
        {
            var result = new AOResult();
            try
            {
                var bookmark = _mockService.Bookmarks.FirstOrDefault(x => x.UserId == userId && x.TweetId == tweetId);

                if (bookmark != null)
                {
                    result.SetSuccess();
                    _mockService.Bookmarks.Remove(bookmark);
                }
                else
                {
                    result.SetFailure(Strings.NotFoundBookmark);
                }
            }
            catch (Exception ex)
            {
                result.SetError($"{nameof(DeleteBoormarkAsync)}: exception", Strings.SomeIssues, ex);
            }

            return result;
        }

        public async Task<AOResult<int>> AddBookmarkAsync(int tweetId, int userId)
        {
            var result = new AOResult<int>();
            try
            {
                if (!_mockService.Bookmarks.Any(x => x.TweetId == tweetId && x.UserId == userId))
                {
                    var bookmark = new Bookmark
                    {
                        Id = _mockService.Bookmarks.Count() + 1,
                        TweetId = tweetId,
                        UserId = userId,
                        Notification = true,
                    };
                    _mockService.Bookmarks.Add(bookmark);
                    int id = _mockService.Bookmarks.Last().Id;
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
                result.SetError($"Exception: {nameof(AddBookmarkAsync)}", Strings.WrongResult, ex);
            }

            return result;
        }

        public async Task<AOResult> AnyAsync(int tweetId, int userId)
        {
            var result = new AOResult();
            try
            {
                var any = _mockService.Bookmarks.Any(x => x.TweetId == tweetId && x.UserId == userId);
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
                result.SetError($"Exception: {nameof(AddBookmarkAsync)}", Strings.WrongResult, ex);
            }

            return result;
        }

        #endregion
    }
}
