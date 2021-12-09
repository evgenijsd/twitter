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
        }

        #region -- IMockService implementation --

        public IEnumerable<UserModel> Users { get; set; }
        public IEnumerable<TweetModel> Tweets { get; set; }

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
                    AvatarPath = "https://inhabitat.com/wp-content/blogs.dir/1/files/2017/08/Bill-Gates-889x598.jpg",
                    BackgroundUserImagePath = "https://yapx.ru/viral/PMYaG",
                },
                new UserModel
                {
                    Id = 2,
                    Name = "Kate White",
                    Email = "test2@gmail.com",
                    Password = "2222",
                    AvatarPath = "https://i.pinimg.com/236x/01/e1/10/01e11011168eb3e1c83d16747192d490.jpg",
                    BackgroundUserImagePath = "https://yapx.ru/viral/PMYaG",
                },
                new UserModel
                {
                    Id = 3,
                    Name = "Sam Smith",
                    Email = "test3@gmail.com",
                    Password = "3333",
                    AvatarPath = "http://www.kinofilms.ua/images/person/big/738231.jpg",
                    BackgroundUserImagePath = "https://yapx.ru/viral/PMYaG",
                },
                new UserModel
                {
                    Id = 4,
                    Name = "Steve Jobs",
                    Email = "test4@gmail.com",
                    Password = "4444",
                    AvatarPath = "https://upload.wikimedia.org/wikipedia/commons/b/b9/Steve_Jobs_Headshot_2010-CROP.jpg",
                    BackgroundUserImagePath = "https://yapx.ru/viral/PMYaG",
                },
                new UserModel
                {
                    Id = 5,
                    Name = "Elon musk ",
                    Email = "test4@gmail.com",
                    Password = "4444",
                    AvatarPath = "https://ichef.bbci.co.uk/news/640/cpsprodpb/81F4/production/_118486233_gettyimages-1229892674.jpg",
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
                    Media = Enums.ETypeAttachedMedia.Photos,
                    MediaPaths = new List<string>
                    {
                        "https://images.unsplash.com/photo-1610878180933-123728745d22?ixlib=rb-1.2.1&ixid=MnwxMjA3fDB8MHxzZWFyY2h8MXx8Y2FuYWRhJTIwbmF0dXJlfGVufDB8fDB8fA%3D%3D&w=1000&q=80",
                        "https://i.pinimg.com/originals/a7/3d/6e/a73d6e4ac85c6a822841e449b24c78e1.jpg",
                        "https://media.springernature.com/full/springer-cms/rest/v1/img/18893370/v1/height/320",
                        "https://www.lombardodier.com/files/live/sites/loportail/files/news/2021/May/20210521/Nature_LOcom.jpg",
                        "https://aka.ms/campus.jpg",
                    },
                    CreationTime = DateTime.Now,
                },
                new TweetModel
                {
                    Id = 2,
                    UserId = 2,
                    Text = "onsequuntur voluptatum laborum numquam blanditiis harum quisquam eius sed odit fugiat iusto fuga praesentium optio, eaque rerum! Provident similique accusantium nemo autem. Veritatisobcaecati tenetur iure eius earum ut molestias architecto voluptate aliquam",
                    Media = Enums.ETypeAttachedMedia.Gif,
                    MediaPaths = new List<string>
                    {
                        "https://c.tenor.com/nMkmGwGH8s8AAAAd/elon-musk-smoke.gif",
                    },
                    CreationTime = DateTime.Now,
                },
                new TweetModel
                {
                    Id = 3,
                    UserId = 3,
                    Text = "um quisquam eius sed odit fugiat iusto fuga praesentium optio, eaque rerum! Provident similique accusantium nemo autem. Veritatisobcaecati tenetur iure eius earum ut molestias architecto voluptate aliquam",
                    Media = Enums.ETypeAttachedMedia.Video,
                    MediaPaths = new List<string>
                    {
                       "https://www.youtube.com/embed/BWfBP5sTW18",
                    },
                    CreationTime = DateTime.Now,
                },
                new TweetModel
                {
                    Id = 4,
                    UserId = 4,
                    Text = "Only text",
                    Media = Enums.ETypeAttachedMedia.None,
                    CreationTime = DateTime.Now,
                },
                new TweetModel
                {
                    Id = 5,
                    UserId = 5,
                    Text = "Hi there!",
                    Media = Enums.ETypeAttachedMedia.Photos,
                    CreationTime = DateTime.Now,
                    MediaPaths = new List<string>
                    {
                       "https://i.guim.co.uk/img/media/32e584f481b3900eb8ffb0727990cafccec100e0/0_129_5321_3194/master/5321.jpg?width=465&quality=45&auto=format&fit=max&dpr=2&s=b463391383c3c2f3444f99697bcdc2c6",
                       "http://internetua.com/uploads/wysiwyg/RomanR/ELON.SPACEX.web_.jpg",
                    },
                },
                new TweetModel
                {
                    Id = 5,
                    UserId = 5,
                    Media = Enums.ETypeAttachedMedia.Photos,
                    CreationTime = DateTime.Now,
                    MediaPaths = new List<string>
                    {
                       "https://i.pinimg.com/474x/f7/f7/73/f7f7733f9e3409f0ef433f3074525790.jpg",
                    },
                },
            };
        }

        #endregion
    }
}
