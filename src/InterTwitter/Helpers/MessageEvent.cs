namespace InterTwitter.Helpers
{
    public class MessageEvent
    {
        public static string DeleteBookmark => nameof(DeleteBookmark);
        public static string AddBookmark => nameof(AddBookmark);
        public static string DeleteLike => nameof(DeleteLike);
        public static string AddLike => nameof(AddLike);

        public int UnTweetId { get; }

        public MessageEvent(int unTweetId)
        {
            UnTweetId = unTweetId;
        }
    }
}
