using InterTwitter.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace InterTwitter.Models
{
    public class TweetModel : IEntityBase
    {
        public int Id { get; set; }

        public int UserId { get; set; }

        public string Text { get; set; }

        public ETypeAttachedMedia Media { get; set; }

        public IEnumerable<string> MediaPaths { get; set; }

        public DateTime CreationTime { get; set; }
    }
}
