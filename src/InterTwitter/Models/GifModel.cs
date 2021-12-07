using System;
using System.Collections.Generic;
using System.Text;

namespace InterTwitter.Models
{
    public class GifModel : IEntityBase
    {
        public int Id { get; set; }
        public int PostId { get; set; }
        public string Gif { get; set; }
    }
}
