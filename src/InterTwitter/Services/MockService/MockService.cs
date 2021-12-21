using InterTwitter.Models;
using System;
using System.Collections.Generic;

namespace InterTwitter.Services
{
    public class MockService : IMockService
    {
        public MockService()
        {
            InitUsers();

            InitTweets();

            InitHashtags();
        }

        #region -- IMockService implementation --

        public IEnumerable<UserModel> Users { get; set; }

        public IEnumerable<TweetModel> Tweets { get; set; }

        public IEnumerable<HashtagModel> Hashtags { get; set; }

        #endregion

        #region -- Private helpers --

        private void InitUsers()
        {
            Users = new List<UserModel>
            {
                new UserModel
                {
                    Id = 1,
                    Name = "Bill Gates",
                    Email = "test@gmail.com",
                    Password = "1111",
                    AvatarPath = "https://cdn.allfamous.org/people/avatars/bill-gates-zdrr-allfamous.org.jpg",
                    BackgroundUserImagePath = "https://yapx.ru/viral/PMYaG",
                },
                new UserModel
                {
                    Id = 2,
                    Name = "Kate White",
                    Email = "test2@gmail.com",
                    Password = "2222",
                    AvatarPath = "https://www.iso.org/files/live/sites/isoorg/files/news/News_archive/2021/03/Ref2639/Ref2639.jpg/thumbnails/300x300",
                    BackgroundUserImagePath = "https://yapx.ru/viral/PMYaG",
                },
                new UserModel
                {
                    Id = 3,
                    Name = "Sam Smith",
                    Email = "test3@gmail.com",
                    Password = "3333",
                    AvatarPath = "https://i.ebayimg.com/images/g/6EIAAOSwJHlfnm3a/s-l300.jpg",
                    BackgroundUserImagePath = "https://yapx.ru/viral/PMYaG",
                },
                new UserModel
                {
                    Id = 4,
                    Name = "Steve Jobs",
                    Email = "test4@gmail.com",
                    Password = "4444",
                    AvatarPath = "https://www.acumarketing.com/wp-content/uploads/2011/08/steve-jobs.jpg",
                    BackgroundUserImagePath = "https://yapx.ru/viral/PMYaG",
                },
                new UserModel
                {
                    Id = 5,
                    Name = "Elon musk",
                    Email = "test5@gmail.com",
                    Password = "4444",
                    AvatarPath = "https://file.liga.net/images/general/2021/09/20/thumbnail-20210920123323-9397.jpg?v=1632132620",
                    BackgroundUserImagePath = "https://yapx.ru/viral/PMYaG",
                },
                new UserModel
                {
                    Id = 6,
                    Name = "Keano Reaves",
                    Email = "test6@gmail.com",
                    Password = "4444",
                    AvatarPath = "https://www.biography.com/.image/ar_1:1%2Cc_fill%2Ccs_srgb%2Cg_face%2Cq_auto:good%2Cw_300/MTE5NTU2MzE2MzU1NzI0ODEx/keanu-reeves-9454211-1-402.jpg",
                    BackgroundUserImagePath = "https://yapx.ru/viral/PMYaG",
                },
            };
        }

        private void InitTweets()
        {
            Tweets = new List<TweetModel>
            {
                new TweetModel
                {
                    Id = 1,
                    UserId = 1,
                    Text = "#NoNuanceNovember #NoNuance #AMAs #AM#As HI #workoutthere #workout ! #AMAs",
                    Media = Enums.EAttachedMediaType.Gif,
                    MediaPaths = new List<string>
                    {
                        "https://javasea.ru/uploads/posts/2013-12/1387635240_matrica.gif",
                    },
                    CreationTime = DateTime.Now,
                },
            };
        }

        private void InitHashtags()
        {
            Hashtags = new List<HashtagModel>
            {
                new HashtagModel()
                {
                    Id = 1,
                    Text = "#blockchain",
                    TweetsCount = 2,
                },
                new HashtagModel()
                {
                    Id = 2,
                    Text = "#AMAs",
                    TweetsCount = 4,
                },
                new HashtagModel()
                {
                    Id = 3,
                    Text = "#NoNuanceNovember",
                    TweetsCount = 3,
                },
                new HashtagModel()
                {
                    Id = 4,
                    Text = "#coffeeTime",
                    TweetsCount = 2,
                },
                new HashtagModel()
                {
                    Id = 5,
                    Text = "#teaTime",
                    TweetsCount = 1,
                },
                new HashtagModel()
                {
                    Id = 6,
                    Text = "#workout",
                    TweetsCount = 1,
                },
                new HashtagModel()
                {
                    Id = 7,
                    Text = "#cats",
                    TweetsCount = 1,
                },
            };
        }

        #endregion
    }
}
