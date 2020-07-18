using System.Text;

namespace Twitter.OAuth.Helpers
{
    internal static class EncodingHelper
    {
        public static string PercentEncode(string value)
        {
            if (string.IsNullOrEmpty(value))
            {
                return value;
            }

            var unreservedCharacters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-_.~";

            var input = new StringBuilder();

            foreach (var symbol in value)
            {
                if (unreservedCharacters.IndexOf(symbol) != -1)
                {
                    input.Append(symbol);
                }
                else
                {
                    byte[] bytes = Encoding.UTF8.GetBytes(symbol.ToString());

                    foreach (byte b in bytes)
                    {
                        input.AppendFormat("%{0:X2}", b);
                    }
                }
            }

            return input.ToString();
        }
    }
}