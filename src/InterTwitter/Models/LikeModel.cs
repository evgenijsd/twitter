namespace InterTwitter.Models
{
    public class LikeModel : IEntityBase
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int PostId { get; set; }
        public bool Notification { get; set; }
    }
}
