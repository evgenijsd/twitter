using System;
using System.Collections.Generic;
using System.Text;

namespace InterTwitter.Models
{
    public class GifModel : IEntity
    {
        public int Id { get; set; }
        public int PostId { get; set; }
        public string Gif { get; set; }
    }
}
