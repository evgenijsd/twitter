using InterTwitter.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace InterTwitter.Models
{
    public class Tweet : IEntityBase
    {
        public int Id { get; set; }

        public int UserId { get; set; }

        public string Text { get; set; }

        public EAttachedMediaType Media { get; set; }

        public List<string> MediaPaths { get; set; }

        public DateTime CreationTime { get; set; }
    }
}
