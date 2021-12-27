using InterTwitter.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace InterTwitter.Services
{
    public class MockService : IMockService
    {
        private readonly TaskCompletionSource<bool> _initCompletionSource = new TaskCompletionSource<bool>();

        private IList<UserModel> _users;
        private IList<TweetModel> _tweets;
        private IList<Bookmark> _bookmarks;
        private IList<LikeModel> _likes;
        private IList<HashtagModel> _hashtags;
        private IList<BlockModel> _blackList;
        private IList<MuteModel> _muteList;

        private Dictionary<Type, object> _base;

        public MockService()
        {
            Task.Run(InitMocksAsync);
        }

        #region -- IMockService implementation --

        public async Task<int> AddAsync<T>(T entity)
            where T : IEntityBase, new()
        {
            await _initCompletionSource.Task;

            int id = GetBase<T>().Max(x => x.Id) + 1;
            entity.Id = id;
            GetBase<T>().Add(entity);

            return id;
        }

        public async Task<IEnumerable<T>> GetAllAsync<T>()
            where T : IEntityBase, new()
        {
            await _initCompletionSource.Task;

            return GetBase<T>();
        }

        public async Task<T> GetByIdAsync<T>(int id)
            where T : IEntityBase, new()
        {
            await _initCompletionSource.Task;

            return GetBase<T>().FirstOrDefault(x => x.Id == id);
        }

        public async Task<bool> RemoveAsync<T>(T entity)
            where T : IEntityBase, new()
        {
            await _initCompletionSource.Task;

            var entityDelete = GetBase<T>().FirstOrDefault(x => x.Id == entity.Id);

            return GetBase<T>().Remove(entityDelete);
        }

        public async Task<int> RemoveAllAsync<T>(Predicate<T> predicate)
            where T : IEntityBase, new()
        {
            await _initCompletionSource.Task;

            return GetBase<T>().RemoveAll(predicate);
        }

        public async Task<T> UpdateAsync<T>(T entity)
            where T : IEntityBase, new()
        {
            await _initCompletionSource.Task;

            var entityUpdate = GetBase<T>().FirstOrDefault(x => x.Id == entity.Id);
            entityUpdate = entity;

            return entityUpdate;
        }

        public async Task<T> FindAsync<T>(Func<T, bool> expression)
            where T : IEntityBase, new()
        {
            await _initCompletionSource.Task;

            return GetBase<T>().FirstOrDefault<T>(expression);
        }

        public async Task<bool> AnyAsync<T>(Func<T, bool> expression)
            where T : IEntityBase, new()
        {
            await _initCompletionSource.Task;

            return GetBase<T>().Any<T>(expression);
        }

        public async Task<IEnumerable<T>> GetAsync<T>(Func<T, bool> expression)
            where T : IEntityBase, new()
        {
            await _initCompletionSource.Task;

            return GetBase<T>().Where<T>(expression);
        }

        #endregion

        #region -- Private helpers --

        private List<T> GetBase<T>()
        {
            return (List<T>)_base[typeof(T)];
        }

        private async Task InitMocksAsync()
        {
            _base = new Dictionary<Type, object>();

            await Task.WhenAll(
                InitUsersAsync(),
                InitTweetsAsync(),
                InitBookmarks(),
                InitLikes(),
                InitHashtags(),
                InitBlackList(),
                InitMuteList());

            _initCompletionSource.TrySetResult(true);
        }

        private Task InitBlackList() => Task.Run(() =>
        {
            _blackList = new List<BlockModel>();

            _base.Add(typeof(BlockModel), _blackList);
        });

        private Task InitMuteList() => Task.Run(() =>
        {
            _muteList = new List<MuteModel>();

            _base.Add(typeof(MuteModel), _muteList);
        });

        private Task InitHashtags() => Task.Run(() =>
        {
            _hashtags = new List<HashtagModel>
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

            _base.Add(typeof(HashtagModel), _hashtags);
        });

        private Task InitUsersAsync() => Task.Run(() =>
        {
            _users = new List<UserModel>
            {
                new UserModel
                {
                    Id = 1,
                    Name = "Bill Gates",
                    Email = "aaa@aaa.aaa",
                    Password = "1234567A",
                    AvatarPath = "https://cdn.allfamous.org/people/avatars/bill-gates-zdrr-allfamous.org.jpg",
                    BackgroundUserImagePath = "https://yapx.ru/viral/PMYaG",
                },
                new UserModel
                {
                    Id = 2,
                    Name = "Kate White",
                    Email = "bbb@bbb.bbb",
                    Password = "1234567A",
                    AvatarPath = "https://www.iso.org/files/live/sites/isoorg/files/news/News_archive/2021/03/Ref2639/Ref2639.jpg/thumbnails/300x300",
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

            _base.Add(typeof(UserModel), _users);
        });

        private Task InitTweetsAsync() => Task.Run(() =>
        {
            var culture = CultureInfo.GetCultureInfo("ru-RU");

            _tweets = new List<TweetModel>
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
                    Id = 8,
                    UserId = 2,
                    Text = "#amas masd",
                    Media = Enums.EAttachedMediaType.Gif,
                    MediaPaths = new List<string>
                    {
                        "https://thumbs.gfycat.com/PaltryWickedCrayfish-max-1mb.gif",
                    },
                    CreationTime = DateTime.Parse("01.02.2021 13:12:12", culture),
                },
                new TweetModel
                {
                    Id = 9,
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

            _base.Add(typeof(TweetModel), _tweets);
        });

        private Task InitLikes() => Task.Run(() =>
        {
            var culture = CultureInfo.GetCultureInfo("ru-RU");

            _likes = new List<LikeModel>
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

            _base.Add(typeof(LikeModel), _likes);
        });

        private Task InitBookmarks() => Task.Run(() =>
        {
            var culture = CultureInfo.GetCultureInfo("ru-RU");

            _bookmarks = new List<Bookmark>
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

            _base.Add(typeof(Bookmark), _bookmarks);
        });

        #endregion
    }
}