using InterTwitter.Models;
using System;
using System.Collections.Generic;
using System.Globalization;

namespace InterTwitter.Services
{
    public class MockService : IMockService
    {
        public MockService()
        {
            MuteList = new List<MuteModel>();
            BlackList = new List<BlockModel>();

            InitUsers();
            InitTweets();
            InitBookmarks();
            InitLikes();
            InitHashtags();
        }

        #region -- IMockService implementation --

        public IList<UserModel> Users { get; set; }

        public IList<TweetModel> Tweets { get; set; }

        public IList<HashtagModel> Hashtags { get; set; }

        public IList<LikeModel> Likes { get; set; }

        public IList<BlockModel> BlackList { get; set; }

        public IList<Bookmark> Bookmarks { get; set; }

        public IList<MuteModel> MuteList { get; set; }

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
                    Email = "aaa@aaa.aaa",
                    Password = "1234567A",
                    AvatarPath = "https://avatars.mds.yandex.net/i?id=9124e8d6c175c189503fa5d7883c515d-5859422-images-thumbs&n=13",
                    BackgroundUserImagePath = "https://yapx.ru/viral/PMYaG",
                },
                new UserModel
                {
                    Id = 2,
                    Name = "Kate White",
                    Email = "bbb@bbb.bbb",
                    Password = "1234567A",
                    AvatarPath = "https://s1.1zoom.ru/b4344/471/Owls_Birds_Glance_537043_2560x1440.jpg",
                    BackgroundUserImagePath = "https://yapx.ru/viral/PMYaG",
                },
                new UserModel
                {
                    Id = 3,
                    Name = "Sam Smith",
                    Email = "ccc@ccc.ccc",
                    Password = "1234567A",
                    AvatarPath = "https://i.ebayimg.com/images/g/6EIAAOSwJHlfnm3a/s-l300.jpg",
                    BackgroundUserImagePath = "https://yapx.ru/viral/PMYaG",
                },
                new UserModel
                {
                    Id = 4,
                    Name = "Steve Jobs",
                    Email = "ddd@ddd.ddd",
                    Password = "1234567A",
                    AvatarPath = "https://www.acumarketing.com/wp-content/uploads/2011/08/steve-jobs.jpg",
                    BackgroundUserImagePath = "https://yapx.ru/viral/PMYaG",
                },
                new UserModel
                {
                    Id = 5,
                    Name = "Elon musk",
                    Email = "eee@eee.eee",
                    Password = "1234567A",
                    AvatarPath = "https://file.liga.net/images/general/2021/09/20/thumbnail-20210920123323-9397.jpg?v=1632132620",
                    BackgroundUserImagePath = "https://yapx.ru/viral/PMYaG",
                },
                new UserModel
                {
                    Id = 6,
                    Name = "Keano Reaves",
                    Email = "fff@fff.fff",
                    Password = "1234567A",
                    AvatarPath = "https://www.biography.com/.image/ar_1:1%2Cc_fill%2Ccs_srgb%2Cg_face%2Cq_auto:good%2Cw_300/MTE5NTU2MzE2MzU1NzI0ODEx/keanu-reeves-9454211-1-402.jpg",
                    BackgroundUserImagePath = "https://yapx.ru/viral/PMYaG",
                },
            };
        }

        private void InitTweets()
        {
            var culture = CultureInfo.GetCultureInfo("ru-RU");

            Tweets = new List<TweetModel>
            {
                new TweetModel
                {
                    Id = 1,
                    UserId = 1,
                    Media = Enums.EAttachedMediaType.Photos,
                    Text = "01 um quisquam eius sed odit fugiat iusto fuga praesentium optio, eaque rerum! Provident similique accusantium nemo autem. Veritatisobcaecati tenetur iure eius earum ut molestias architecto voluptate aliquam",
                    MediaPaths = new List<string>
                    {
                        "https://oboi-lux.com.ua/23613-home_default/fotooboi-s-prirodoj.jpg",
                        "https://fotooboimarket.com.ua/11134-home_default/priroda.jpg",
                        "https://oboi-lux.com.ua/23595-home_default/fotooboi-s-prirodoj.jpg",
                        "http://intpicture.com/wp-content/uploads/2011/07/Nature-64-034-Copy-300x300.jpg",
                    },
                    CreationTime = DateTime.Parse("01.01.2020 12:12:12", culture),
                },
                new TweetModel
                {
                    Id = 2,
                    UserId = 3,
                    Text = "#NoNuanceNovember um quisquam eius #AMAs sed odit fugiat iusto fuga #blockchain praesentium optio, eaque rerum! Provident similique accusantium nemo autem. Veritatisobcaecati tenetur iure eius earum ut molestias architecto voluptate aliquam",
                    Media = Enums.EAttachedMediaType.Video,
                    MediaPaths = new List<string>
                    {
                       "https://www.youtube.com/embed/_hGuLM4Y-xM",
                    },
                    CreationTime = DateTime.Parse("01.03.2020 13:12:12", culture),
                },
                new TweetModel
                {
                    Id = 3,
                    UserId = 4,
                    Text = "#AMAs Only #NoNuanceNovember text #coffeeTime",
                    Media = Enums.EAttachedMediaType.None,
                    CreationTime = DateTime.Parse("01.04.2020 15:12:12", culture),
                },
                new TweetModel
                {
                    Id = 4,
                    UserId = 5,
                    Text = " #teaTime Hi #blockchain there! ",
                    Media = Enums.EAttachedMediaType.Photos,
                    CreationTime = DateTime.Parse("01.05.2021 12:00:12", culture),
                    MediaPaths = new List<string>
                    {
                       "https://akm-img-a-in.tosshub.com/indiatoday/images/story/202110/Elon-Musk_1200x768.png?WgPbwF44wHXitAAL7xu8BNhZXQXIPBbV&size=770:433",
                       "https://i.pinimg.com/550x/23/b4/b1/23b4b13e9019667ca68d0897e154a755.jpg",
                    },
                },
                new TweetModel
                {
                    Id = 5,
                    UserId = 5,
                    Media = Enums.EAttachedMediaType.Photos,
                    CreationTime = DateTime.Parse("01.11.2020 12:00:12", culture),
                    MediaPaths = new List<string>
                    {
                       "https://i.pinimg.com/474x/f7/f7/73/f7f7733f9e3409f0ef433f3074525790.jpg",
                    },
                },
                new TweetModel
                {
                    Id = 6,
                    UserId = 6,
                    Text = "#workout #NoNuanceNovember #workout #AMAs #workouthello HI#workout there#workout!",
                    Media = Enums.EAttachedMediaType.Gif,
                    MediaPaths = new List<string>
                    {
                        "https://javasea.ru/uploads/posts/2013-12/1387635240_matrica.gif",
                    },
                    CreationTime = DateTime.Parse("01.01.2021 12:12:12", culture),
                },
                new TweetModel
                {
                    Id = 7,
                    UserId = 2,
                    Text = "#AMAs onsequuntur #cats voluptatum laborum #coffeeTime numquam blanditiis harum quisquam eius sed odit fugiat iusto fuga praesentium optio, eaque rerum! Provident similique accusantium nemo autem. Veritatisobcaecati tenetur iure eius earum ut molestias architecto voluptate aliquam",
                    Media = Enums.EAttachedMediaType.Gif,
                    MediaPaths = new List<string>
                    {
                        "https://thumbs.gfycat.com/PaltryWickedCrayfish-max-1mb.gif",
                    },
                    CreationTime = DateTime.Now,
                },
                new TweetModel
                {
                    Id = 2,
                    UserId = 2,
                    Text = "#amas masd",
                    Media = Enums.EAttachedMediaType.Gif,
                    MediaPaths = new List<string>
                    {
                        "https://thumbs.gfycat.com/PaltryWickedCrayfish-max-1mb.gif",
                    },
                    CreationTime = DateTime.Now,
                },
                new TweetModel
                {
                    Id = 3,
                    UserId = 3,
                    //Text = "#AMAs masd # masda as ama",
                    Media = Enums.EAttachedMediaType.Gif,
                    MediaPaths = new List<string>
                    {
                        "https://thumbs.gfycat.com/PaltryWickedCrayfish-max-1mb.gif",
                    },
                    CreationTime = DateTime.Parse("01.02.2021 12:12:12", culture),
                },
            };
        }

        private void InitLikes()
        {
            var culture = CultureInfo.GetCultureInfo("ru-RU");

            Likes = new List<LikeModel>
            {
                new LikeModel
                {
                    Id = 1,
                    UserId = 1,
                    TweetId = 1,
                    Notification = true,
                    CreationTime = DateTime.Parse("02.03.2021 12:12:12", culture),
                },
                new LikeModel
                {
                    Id = 2,
                    UserId = 1,
                    TweetId = 2,
                    Notification = true,
                    CreationTime = DateTime.Parse("01.03.2021 12:12:12", culture),
                },
                new LikeModel
                {
                    Id = 3,
                    UserId = 1,
                    TweetId = 3,
                    Notification = true,
                    CreationTime = DateTime.Parse("02.03.2021 12:12:12", culture),
                },
                new LikeModel
                {
                    Id = 4,
                    UserId = 1,
                    TweetId = 4,
                    Notification = true,
                    CreationTime = DateTime.Parse("03.03.2021 12:12:12", culture),
                },
                new LikeModel
                {
                    Id = 5,
                    UserId = 1,
                    TweetId = 5,
                    Notification = true,
                    CreationTime = DateTime.Parse("04.03.2021 12:12:12", culture),
                },
                new LikeModel
                {
                    Id = 6,
                    UserId = 2,
                    TweetId = 1,
                    Notification = true,
                    CreationTime = DateTime.Parse("05.03.2021 12:12:12", culture),
                },
                new LikeModel
                {
                    Id = 7,
                    UserId = 2,
                    TweetId = 2,
                    Notification = true,
                    CreationTime = DateTime.Parse("04.03.2021 12:12:12", culture),
                },
                new LikeModel
                {
                    Id = 8,
                    UserId = 2,
                    TweetId = 3,
                    Notification = true,
                    CreationTime = DateTime.Parse("03.03.2021 12:12:12", culture),
                },
                new LikeModel
                {
                    Id = 9,
                    UserId = 2,
                    TweetId = 4,
                    Notification = true,
                    CreationTime = DateTime.Parse("02.03.2021 12:12:12", culture),
                },
                new LikeModel
                {
                    Id = 10,
                    UserId = 2,
                    TweetId = 5,
                    Notification = true,
                    CreationTime = DateTime.Parse("01.03.2021 12:12:12", culture),
                },
                new LikeModel
                {
                    Id = 11,
                    UserId = 3,
                    TweetId = 1,
                    Notification = true,
                    CreationTime = DateTime.Parse("02.03.2021 12:12:12", culture),
                },
                new LikeModel
                {
                    Id = 12,
                    UserId = 3,
                    TweetId = 2,
                    Notification = true,
                    CreationTime = DateTime.Parse("03.03.2021 12:12:12", culture),
                },
                new LikeModel
                {
                    Id = 13,
                    UserId = 3,
                    TweetId = 3,
                    Notification = true,
                    CreationTime = DateTime.Parse("04.03.2021 12:12:12", culture),
                },
                new LikeModel
                {
                    Id = 14,
                    UserId = 3,
                    TweetId = 4,
                    Notification = true,
                    CreationTime = DateTime.Parse("05.03.2021 12:12:12", culture),
                },
                new LikeModel
                {
                    Id = 15,
                    UserId = 3,
                    TweetId = 5,
                    Notification = true,
                    CreationTime = DateTime.Parse("07.03.2021 12:12:12", culture),
                },
                new LikeModel
                {
                    Id = 16,
                    UserId = 3,
                    TweetId = 7,
                    Notification = true,
                    CreationTime = DateTime.Parse("07.03.2021 12:12:12", culture),
                },
                new LikeModel
                {
                    Id = 17,
                    UserId = 6,
                    TweetId = 1,
                    Notification = true,
                    CreationTime = DateTime.Parse("07.03.2021 12:12:12", culture),
                },
            };
        }

        private void InitBookmarks()
        {
            var culture = CultureInfo.GetCultureInfo("ru-RU");

            Bookmarks = new List<Bookmark>
            {
                new Bookmark
                {
                    Id = 1,
                    UserId = 1,
                    TweetId = 1,
                    Notification = true,
                    CreationTime = DateTime.Parse("01.03.2021 12:12:12", culture),
                },
                new Bookmark
                {
                    Id = 2,
                    UserId = 1,
                    TweetId = 2,
                    Notification = true,
                    CreationTime = DateTime.Parse("10.03.2021 12:12:12", culture),
                },
                new Bookmark
                {
                    Id = 3,
                    UserId = 1,
                    TweetId = 3,
                    Notification = true,
                    CreationTime = DateTime.Parse("01.03.2021 12:12:12", culture),
                },
                new Bookmark
                {
                    Id = 4,
                    UserId = 1,
                    TweetId = 4,
                    Notification = true,
                    CreationTime = DateTime.Parse("05.03.2021 12:12:12", culture),
                },
                new Bookmark
                {
                    Id = 5,
                    UserId = 1,
                    TweetId = 5,
                    Notification = true,
                    CreationTime = DateTime.Parse("06.03.2021 12:12:12", culture),
                },
                new Bookmark
                {
                    Id = 6,
                    UserId = 2,
                    TweetId = 1,
                    Notification = true,
                    CreationTime = DateTime.Parse("07.03.2021 12:12:12", culture),
                },
                new Bookmark
                {
                    Id = 7,
                    UserId = 2,
                    TweetId = 2,
                    Notification = true,
                    CreationTime = DateTime.Parse("08.03.2021 12:12:12", culture),
                },
                new Bookmark
                {
                    Id = 8,
                    UserId = 2,
                    TweetId = 3,
                    Notification = true,
                    CreationTime = DateTime.Parse("09.03.2021 12:12:12", culture),
                },
                new Bookmark
                {
                    Id = 9,
                    UserId = 2,
                    TweetId = 4,
                    Notification = true,
                    CreationTime = DateTime.Parse("09.03.2021 12:12:12", culture),
                },
                new Bookmark
                {
                    Id = 10,
                    UserId = 2,
                    TweetId = 5,
                    Notification = true,
                    CreationTime = DateTime.Parse("08.03.2021 12:12:12", culture),
                },
                new Bookmark
                {
                    Id = 11,
                    UserId = 3,
                    TweetId = 1,
                    Notification = true,
                    CreationTime = DateTime.Parse("07.03.2021 12:12:12", culture),
                },
                new Bookmark
                {
                    Id = 12,
                    UserId = 3,
                    TweetId = 2,
                    Notification = true,
                    CreationTime = DateTime.Parse("06.03.2021 12:12:12", culture),
                },
                new Bookmark
                {
                    Id = 13,
                    UserId = 3,
                    TweetId = 3,
                    Notification = true,
                    CreationTime = DateTime.Parse("05.03.2021 12:12:12", culture),
                },
                new Bookmark
                {
                    Id = 14,
                    UserId = 3,
                    TweetId = 4,
                    Notification = true,
                    CreationTime = DateTime.Parse("04.03.2021 12:12:12", culture),
                },
                new Bookmark
                {
                    Id = 15,
                    UserId = 3,
                    TweetId = 5,
                    Notification = true,
                    CreationTime = DateTime.Parse("03.03.2021 12:12:12", culture),
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