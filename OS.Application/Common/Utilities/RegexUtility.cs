using System.Text.RegularExpressions;

namespace OS.Application.Common.Utilities
{
    public class RegexUtility
    {
        private static Regex digitsOnly = new Regex(@"[^\d]");

        public static string CleanPhone(string phone)
        {
            return digitsOnly.Replace(phone, "");
        }
    }
}
