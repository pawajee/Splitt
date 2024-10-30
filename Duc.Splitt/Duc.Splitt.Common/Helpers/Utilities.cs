namespace Duc.Splitt.Common.Helpers
{
    public class Utilities
    {

        public static bool ExcludeSensitiveApis { get; set; }

        public static List<string>? RequestLogExcludedApis { get; set; }
        public static Guid AnonymousUserID { get; set; }


    }
}
