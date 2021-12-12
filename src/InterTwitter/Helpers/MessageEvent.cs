namespace InterTwitter.Helpers
{
    public class MessageEvent
    {
        public static string DeleteBookmark => nameof(DeleteBookmark);

        public int UnTweetId { get; }

        public MessageEvent(int unTweetId)
        {
            UnTweetId = unTweetId;
        }
    }
}
