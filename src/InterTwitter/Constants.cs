using System.Collections.Generic;
using System.Linq;

namespace InterTwitter
{
    public static class Constants
    {
        public static class Messages
        {
            public const string TAB_SELECTED = "TabSelected";

            public const string OPEN_SIDEBAR = "OpenSidebar";

            public const string TAB_CHANGE = "TabChange";

            public const string MEDIA = "Media";
        }

        public static class Navigation
        {
            public const string USER = nameof(USER);
            public const string MESSAGE = nameof(MESSAGE);
        }

        public static class Methods
        {
            public static IEnumerable<string> GetUniqueWords(string text)
            {
                return text.Split(' ').Where(x => !string.IsNullOrWhiteSpace(x)).Distinct();
            }
        }

        public static class RegexPatterns
        {
            public const string HASHTAG_PATTERN = @"^\#[0-9a-zA-Zа-яА-Я_]{1,30}$";
        }

        public static class Limits
        {
            public const int MAX_COUNT_ATTACHED_PHOTOS = 6;
            public const int MAX_SIZE_ATTACHED_PHOTO = 5 * 1024 * 1024;
            public const int MAX_SIZE_ATTACHED_VIDEO = 15 * 1024 * 1024;
            public const int MAX_LENGTH_VIDEO = 180;
            public const int MAX_LENGTH_TEXT = 250;
        }
    }
}
