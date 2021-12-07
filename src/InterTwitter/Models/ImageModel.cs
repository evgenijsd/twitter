using System;
using System.Collections.Generic;
using System.Text;

namespace InterTwitter.Models
{
    public class ImageModel : IEntity
    {
        public int Id { get; set; }
        public int PostId { get; set; }
        public string Image { get; set; }
    }
}
