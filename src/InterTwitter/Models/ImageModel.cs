namespace InterTwitter.Models
{
    public class ImageModel : IEntityBase
    {
        public int Id { get; set; }
        public int PostId { get; set; }
        public string Image { get; set; }
    }
}
