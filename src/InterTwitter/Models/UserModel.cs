﻿namespace InterTwitter.Models
{
    public class UserModel : IEntityBase
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }

        public string AvatarPath { get; set; }

        public string BackgroundUserImagePath { get; set; }
    }
}
