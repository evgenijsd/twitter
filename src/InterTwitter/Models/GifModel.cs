namespace InterTwitter.Models
{
    public class GifModel : IEntityBase
    {
        public int Id { get; set; }

        public int PostId { get; set; }

        public string Gif { get; set; }
    }
}
