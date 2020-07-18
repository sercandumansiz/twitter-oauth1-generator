using System;
using System.Globalization;
using System.Text;

namespace Twitter.OAuth.Helpers
{
    internal static class OAuthHelper
    {
        public static string GenerateNonce()
        {
            return Convert.ToBase64String(new ASCIIEncoding().GetBytes(DateTime.Now.Ticks.ToString(CultureInfo.InvariantCulture)));
        }

        public static string GenerateTimeStamp()
        {
            TimeSpan timeSpan = DateTime.UtcNow - new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);

            return Convert.ToInt64(timeSpan.TotalSeconds).ToString(CultureInfo.InvariantCulture);
        }
    }
}