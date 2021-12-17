using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InterTwitter
{
    public static class Constants
    {
        public static class Messages
        {
            public const string TAB_SELECTED = "TabSelected";

            public const string OPEN_SIDEBAR = "OpenSidebar";

            public const string TAB_CHANGE = "TabChange";

            public const string UPDATE_HASHTAGS = "UpdateHashtags";
        }

        public static class TweetsSearch
        {
            public const string INACCURATE_REQUEST = "ForMoreAccurateSearchSnter2OrMoreCharacters";
            public const string NO_RESULTS_FOR = "NoResultsFor";
            public const int NUMBER_OF_POPULAR_HASHTAGS = 5;

            public static List<string> GetUniqueWords(string text)
            {
                return new List<string>(text.Split(' ').Where(x => !string.IsNullOrWhiteSpace(x)).Distinct());
            }
        }

        public static class RegexPatterns
        {
            public const string HASHTAG_PATTERN = @"^#[0-9a-zA-Zа-яА-Я_]{2,30}$";
        }
    }
}
