namespace InterTwitter.Models
{
    public class GifModel : IEntityBase
    {
        public int Id { get; set; }

        public int TweetId { get; set; }

        public string Gif { get; set; }
    }
}
