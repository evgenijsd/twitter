using System;
using System.Collections.Generic;
using System.Text;

namespace InterTwitter.Models
{
    public class UserModel : IEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string UserPhoto { get; set; }
    }
}
