namespace Core
{
    public static class Constants
    {
        public const int USERS_COUNT = 100;

        public static readonly string[] USER_EMAIL_DOMAINS = { "hotmail.com", "gmail.com", "live.com" };

        public static readonly string[] USER_FIRST_NAMES =
        {
            "John",
            "Gringo",
            "Mark",
            "Lisa",
            "Maria",
            "Sonya",
            "Philip",
            "Jose",
            "Lorenzo",
            "George",
            "Justin"
        };

        public static readonly string[] USER_LAST_NAMES =
        {
            "Johnson",
            "Lamas",
            "Jackson",
            "Brown",
            "Mason",
            "Rodriguez",
            "Roberts",
            "Thomas",
            "Rose",
            "McDonalds"
        };

        public const int USER_PROJECTS_MAX_COUNT = 20;

        public const int USER_PROJECTS_MIN_COUNT = 1;

        public const int PER_PAGE_COUNT = 10;

        public const int PROJECTS_COUNT = 3;

        public static readonly string[] PROJECT_NAMES = { "My own", "Free Time", "Work" };

        public const double MAX_HOURS_PER_DATE = 8;

        public const double MIN_HOURS_PER_DATE = 0.25;
    }
}