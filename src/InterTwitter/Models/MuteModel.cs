using System;
using System.Collections.Generic;
using System.Text;

namespace InterTwitter.Models
{
    public class MuteModel : IEntityBase
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int MutedUserId { get; set; }
    }
}
