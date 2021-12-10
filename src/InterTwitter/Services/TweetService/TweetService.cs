using InterTwitter.Helpers;
using InterTwitter.Models;
using InterTwitter.Models.TweetViewModel;
using InterTwitter.Services.Settings;
using Prism.Events;
using Prism.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

namespace InterTwitter.Services
{
    public class TweetService : ITweetService
    {
        private readonly IMockService _mockService;
        private IPageDialogService _dialogs { get; }
        private readonly ISettingsManager _settingsManager;
        private readonly IEventAggregator _event;

        public TweetService(
            IMockService mockService,
            ISettingsManager settingsManager,
            IEventAggregator aggregator,
            IPageDialogService dialogs)
        {
            _event = aggregator;
            _mockService = mockService;
            _settingsManager = settingsManager;
            _dialogs = dialogs;
        }

        #region -- ITweetService implementation --

        public async Task<AOResult<IEnumerable<TweetModel>>> GetAllTweetsAsync()
        {
            await Task.Delay(50);
            var result = new AOResult<IEnumerable<TweetModel>>();
            try
            {
                var tweets = _mockService.Tweets;

                if (tweets != null)
                {
                    result.SetSuccess(tweets.OrderByDescending(x => x.CreationTime));
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

        public async Task<AOResult<UserModel>> GetUserAsync(int userId)
        {
            var result = new AOResult<UserModel>();

            try
            {
                var user = _mockService.Users.Where(x => x.Id == userId).FirstOrDefault();
                if (user != null)
                {
                    result.SetSuccess(user);
                }
                else
                {
                    result.SetFailure("not found any user");
                }
            }
            catch (Exception ex)
            {
                result.SetError($"{nameof(GetUserAsync)}: exception", "Some issues", ex);
            }

            return await Task.FromResult(result);
        }

        public async Task<AOResult<IEnumerable<Bookmark>>> GetBookmarksAsync(int userId)
        {
            var result = new AOResult<IEnumerable<Bookmark>>();

            try
            {
                var bookmarks = _mockService.Bookmarks.Where(x => x.UserId == userId);
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
                result.SetError($"{nameof(GetUserAsync)}: exception", "Some issues", ex);
            }

            return await Task.FromResult(result);
        }

        public Task GetDeleteAllBookmarksAsync(int userId)
        {
            //var result = new AOResult<IEnumerable<Bookmark>>();
            try
            {
                /*var bookmarks = _mockService.Bookmarks.Where(x => x.UserId == userId);
                if (bookmarks != null)
                {
                    result.SetSuccess(bookmarks);
                }
                else
                {
                    result.SetFailure("not found any bookmark");
                }*/
                _mockService.Bookmarks = _mockService.Bookmarks.Where(x => x.UserId != userId);
            }
            catch (Exception ex)
            {
                //result.SetError($"{nameof(GetUserAsync)}: exception", "Some issues", ex);
            }

            return Task.CompletedTask;
        }

        public void DeleteBoormark(int tweetId, int userId)
        {
            _mockService.Bookmarks = _mockService.Bookmarks.Where(x => !(x.UserId == userId && x.TweetId == tweetId));
            _event.GetEvent<DeleteBookmarkEvent>().Publish(tweetId);
        }

        #endregion

    }

    public class DeleteBookmarkEvent : PubSubEvent<int>
    {
    }
}
