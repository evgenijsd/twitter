namespace InterTwitter
{
    public static class Constants
    {
        public static class Messages
        {
            public const string TAB_SELECTED = nameof(TAB_SELECTED);
            public const string OPEN_SIDEBAR = nameof(OPEN_SIDEBAR);
            public const string TAB_CHANGE = nameof(TAB_CHANGE);
            public const string USER_PROFILE_CHANGED = nameof(USER_PROFILE_CHANGED);
        }

        public static class RegexPatterns
        {
            public const string USERNAME_REGEX = @"^[A-Za-z ]{1,}$";
            public const string EMAIL_REGEX = @"^[\w\.]+@([\w-]+\.)+[\w-]{1,}$";
            public const string PASSWORD_REGEX = @"^(?=.*\d)(?=.*[A-ZА-ЯЁ]).{6,}$";
        }

        public static class Navigation
        {
            public const string CURRENT_USER = nameof(CURRENT_USER);
            public const string MUTELIST = nameof(MUTELIST);
            public const string BLACKLIST = nameof(BLACKLIST);
            public const string USER = nameof(USER);
            public const string MESSAGE = nameof(MESSAGE);
        }

        public static class DialogParameterKeys
        {
            public const string MESSAGE = nameof(MESSAGE);
            public const string TITLE = nameof(TITLE);
            public const string OK_BUTTON_TEXT = nameof(OK_BUTTON_TEXT);
            public const string CANCEL_BUTTON_TEXT = nameof(CANCEL_BUTTON_TEXT);
            public const string ACCEPT = nameof(ACCEPT);
        }
    }
}
