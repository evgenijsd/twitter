using InterTwitter.Enums;
using InterTwitter.Helpers;
using InterTwitter.Helpers.ProcessHelpers;
using InterTwitter.Models;
using InterTwitter.Models.NotificationViewModel;
using InterTwitter.Resources.Strings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InterTwitter.Services
{
    public class NotificationService : INotificationService
    {
        private readonly ILikeService _likeService;

        private readonly IBookmarkService _bookmarkService;

        private readonly ITweetService _tweetService;

        public NotificationService(
            ILikeService likeService,
            IBookmarkService bookmarkService,
            ITweetService tweetService)
        {
            _likeService = likeService;
            _bookmarkService = bookmarkService;
            _tweetService = tweetService;
        }

        #region -- Interface implementation --

        public async Task<AOResult<List<BaseNotificationViewModel>>> GetNotificationsAsync(int userId)
        {
            var result = new AOResult<List<BaseNotificationViewModel>>();

            try
            {
                var resultTweets = await _tweetService.GetByUserTweetsAsync(userId);
                if (resultTweets.IsSuccess)
                {
                    var notificationViewModels = new List<BaseNotificationViewModel>();

                    var resultBookmarks = await GetBookmarkNotificationsAsync(resultTweets.Result, userId);
                    var resultLikes = await GetLikeNotificationsAsync(resultTweets.Result, userId);
                    if (resultBookmarks.IsSuccess && resultLikes.IsSuccess)
                    {
                        notificationViewModels = resultBookmarks.Result.Concat(resultLikes.Result).ToList();
                    }
                    else
                    {
                        notificationViewModels = resultBookmarks.Result ?? resultLikes.Result;
                    }

                    if (notificationViewModels != null)
                    {
                        result.SetSuccess(notificationViewModels);
                    }
                    else
                    {
                        result.SetFailure(Strings.NotFoundNotification);
                    }
                }
                else
                {
                    result.SetFailure(Strings.NoTweets);
                }
            }
            catch (Exception ex)
            {
                result.SetError($"{nameof(GetNotificationsAsync)}: exception", Strings.SomeIssues, ex);
            }

            return result;
        }

        #endregion

        #region -- Private helpers --

        private async Task<AOResult<List<BaseNotificationViewModel>>> GetBookmarkNotificationsAsync(List<TweetModel> tweets, int userId)
        {
            var result = new AOResult<List<BaseNotificationViewModel>>();
            try
            {
                var resultBookmarks = await _bookmarkService.GetNotificationsAsync(userId);
                if (resultBookmarks.IsSuccess)
                {
                    var notificationViewModels = new List<BaseNotificationViewModel>();
                    var bookmarks = resultBookmarks.Result.Where(x => tweets.Any(y => y.Id == x.TweetId)).ToList();
                    foreach (var b in bookmarks)
                    {
                        var tweet = tweets.FirstOrDefault(x => x.Id == b.TweetId);
                        var user = await _tweetService.GetAuthorAsync(b.UserId);
                        var notification = new BaseNotificationViewModel
                        {
                            TweetId = b.TweetId,
                            UserId = b.UserId,
                            CreationTime = b.CreationTime,
                            UserAvatar = user.Result?.AvatarPath,
                            UserName = user.Result?.Name,
                            Text = tweet.Text,
                            MediaPaths = tweet.MediaPaths,
                            Media = tweet.Media,
                            NotificationIcon = "ic_bookmarks_blue",
                            NotificationText = "saved your post",
                        };

                        notificationViewModels.Add(notification);
                    }

                    if (notificationViewModels != null)
                    {
                        result.SetSuccess(notificationViewModels);
                    }
                    else
                    {
                        result.SetFailure(Strings.NotFoundNotification);
                    }
                }
            }
            catch (Exception ex)
            {
                result.SetError($"{nameof(GetNotificationsAsync)}: exception", Strings.SomeIssues, ex);
            }

            return result;
        }

        private async Task<AOResult<List<BaseNotificationViewModel>>> GetLikeNotificationsAsync(List<TweetModel> tweets, int userId)
        {
            var result = new AOResult<List<BaseNotificationViewModel>>();
            try
            {
                var resultLikes = await _likeService.GetNotificationsAsync(userId);
                var notificationViewModels = new List<BaseNotificationViewModel>();
                if (resultLikes.IsSuccess)
                {
                    var likes = resultLikes.Result.Where(x => tweets.Any(y => y.Id == x.TweetId)).ToList();
                    foreach (var l in likes)
                    {
                        var tweet = tweets.FirstOrDefault(x => x.Id == l.TweetId);
                        var user = await _tweetService.GetAuthorAsync(l.UserId);
                        var notification = new BaseNotificationViewModel
                        {
                            TweetId = l.TweetId,
                            UserId = l.UserId,
                            CreationTime = l.CreationTime,
                            UserAvatar = user.Result?.AvatarPath,
                            UserName = user.Result?.Name,
                            Text = tweet.Text,
                            MediaPaths = tweet.MediaPaths,
                            Media = tweet.Media,
                            NotificationIcon = "ic_like_blue",
                            NotificationText = "liked your post",
                        };

                        notificationViewModels.Add(notification);
                    }

                    if (notificationViewModels != null)
                    {
                        result.SetSuccess(notificationViewModels);
                    }
                    else
                    {
                        result.SetFailure(Strings.NotFoundNotification);
                    }
                }
            }
            catch (Exception ex)
            {
                result.SetError($"{nameof(GetNotificationsAsync)}: exception", Strings.SomeIssues, ex);
            }

            return result;
        }

        #endregion

    }
}
