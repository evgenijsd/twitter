using InterTwitter.Models;
using System;
using System.Collections.Generic;
using System.Text;

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
        public List<UserModel> Users { get; set; }
        public List<TweetModel> Tweets { get; set; }

        #endregion

        #region -- Private helpers --

        private void InitUsers()
        {
            Users = new List<UserModel>
            {
                new UserModel
                {
                    Id = 1,
                    Name = "Tom Black",
                    Email = "test@gmail.com",
                    Password = "1111",
                    AvatarPath = "https://yapx.ru/viral/O39J0",
                    BackgroundUserImage = "https://yapx.ru/viral/PMYaG",
                },
                new UserModel
                {
                    Id = 2,
                    Name = "Kate White",
                    Email = "test2@gmail.com",
                    Password = "2222",
                    AvatarPath = "https://ru.depositphotos.com/stock-photos/%D0%BB%D0%B8%D1%86%D0%BE.html?qview=36297389",
                    BackgroundUserImage = "https://yapx.ru/viral/PMYaG",
                },
                new UserModel
                {
                    Id = 3,
                    Name = "Sam Smith",
                    Email = "test3@gmail.com",
                    Password = "3333",
                    AvatarPath = "https://ru.depositphotos.com/stock-photos/%D0%BB%D0%B8%D1%86%D0%BE.html?qview=19266867",
                    BackgroundUserImage = "https://yapx.ru/viral/PMYaG",
                },
                new UserModel
                {
                    Id = 3,
                    Name = "Tim White",
                    Email = "test4@gmail.com",
                    Password = "4444",
                    AvatarPath = "https://ru.depositphotos.com/stock-photos/%D0%BB%D0%B8%D1%86%D0%BE.html?qview=10729165",
                    BackgroundUserImage = "https://yapx.ru/viral/PMYaG",
                },
                new UserModel
                {
                    Id = 4,
                    Name = "Tim White",
                    Email = "test4@gmail.com",
                    Password = "4444",
                    AvatarPath = "https://ru.depositphotos.com/stock-photos/%D0%BB%D0%B8%D1%86%D0%BE.html?qview=24980299",
                    BackgroundUserImage = "https://yapx.ru/viral/PMYaG",
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
                    Text = "Lorem ipsum dolor sit amet consectetur adipisicing elit. Maxime mollitia, molestiae quas vel sint commodi repudiandae consequuntur voluptatum laborum numquam blanditiis harum quisquam eius sed odit fugiat iusto fuga praesentium optio, eaque rerum! Provident similique accusantium nemo autem. Veritatisobcaecati tenetur iure eius earum ut molestias architecto voluptate aliquam",
                    Media = Enums.TweetType.ImagesTweet,
                    MediaPaths = new List<string>
                    {
                        "https://www.google.com/search?q=%D0%BA%D0%B0%D1%80%D1%82%D0%B8%D0%BD%D0%BA%D0%B8&sxsrf=AOaemvKFjuN0q93Rk8eQw1xn0wsgsdC48w:1638891686174&tbm=isch&source=iu&ictx=1&fir=HnhKplPWRplAkM%252CL8rVp5-ioCDE7M%252C_%253BYpoI8CfGWzIXnM%252C1fs8sSJXUJGajM%252C_%253BRFhiNUszsG7cMM%252C8BIoRL6-LGNLiM%252C_%253BD0bk8ElIOhiZ5M%252CZJLWMCPC-_5rnM%252C_%253BbPlxidVrXgqXEM%252COnwwlJcQkF8MpM%252C_%253B2UgBGuhHoDuRLM%252CVfMqV6vzF2zCOM%252C_%253BAeiapjk76udJkM%252CCsQ2GrWsr0EnMM%252C_%253BIeY4BSkRnz6uzM%252Csv_TK010Or4XHM%252C_%253B_X0kq5PtD0pEcM%252CGdIVzLX49klOrM%252C_%253BUgREe_1vqkFAUM%252C3zMrEijU_-MiFM%252C_%253BD7vBWSFMCZDnPM%252CbzlpbwEP8kf0HM%252C_%253BjhNZyWamo_ATjM%252CgUF_TIJvyPy1nM%252C_%253Bu1v9qDnMo17dYM%252Cd1AsRu93nuUrhM%252C_%253BWQJDCwjnjFuftM%252CPTj2zCP0IlvLWM%252C_%253BmhJzJuUufQVwHM%252CsEGIYATiGbAkcM%252C_%253B2Ph16iACytFGwM%252CcrHo620giV5_JM%252C_%253BuFV_9rgQPyFBkM%252C4tK2AtcQn54dXM%252C_%253BUx_pBzczrhsiMM%252Cl8a2l50v2XhkBM%252C_&vet=1&usg=AI4_-kSFi3ZDecMqKIbCdrARKChWUpB8Mg&sa=X&ved=2ahUKEwih7J2mg9L0AhVPhv0HHfDwC1wQ9QF6BAgPEAE&biw=1654&bih=1262&dpr=1",
                        "https://mirpozitiva.ru/photo/1252-krasivye-kartinki.html",
                        "https://mirpozitiva.ru/photo/1252-krasivye-kartinki.html",
                        "https://mirpozitiva.ru/photo/1252-krasivye-kartinki.html",
                        "https://mirpozitiva.ru/photo/1252-krasivye-kartinki.html",
                        "https://mirpozitiva.ru/photo/1252-krasivye-kartinki.html",
                    },
                    CreationTime = DateTime.Now,
                },
                new TweetModel
                {
                    Id = 2,
                    UserId = 2,
                    Text = "onsequuntur voluptatum laborum numquam blanditiis harum quisquam eius sed odit fugiat iusto fuga praesentium optio, eaque rerum! Provident similique accusantium nemo autem. Veritatisobcaecati tenetur iure eius earum ut molestias architecto voluptate aliquam",
                    Media = Enums.TweetType.GifTweet,
                    MediaPaths = new List<string>
                    {
                        "https://i.gifer.com/7rF1.mp4",
                    },
                    CreationTime = DateTime.Now,
                },
                new TweetModel
                {
                    Id = 3,
                    UserId = 3,
                    Text = "um quisquam eius sed odit fugiat iusto fuga praesentium optio, eaque rerum! Provident similique accusantium nemo autem. Veritatisobcaecati tenetur iure eius earum ut molestias architecto voluptate aliquam",
                    Media = Enums.TweetType.VideoTweet,
                    MediaPaths = new List<string>
                    {
                        "https://www.youtube.com/embed/mcHt8L6CAB8",
                    },
                    CreationTime = DateTime.Now,
                },
                new TweetModel
                {
                    Id = 4,
                    UserId = 4,
                    Text = "um quisquam eius sed odit fugiat iusto fuga praesentium optio, eaque rerum! Provident similique accusantium nemo autem. Veritatisobcaecati tenetur iure eius earum ut molestias architecto voluptate aliquam",
                    CreationTime = DateTime.Now,
                },
            };
        }

        #endregion
    }
}
