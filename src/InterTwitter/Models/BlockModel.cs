namespace InterTwitter.Models
{
    public class BlockModel : IEntityBase
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int BlockedUserId { get; set; }
    }
}
