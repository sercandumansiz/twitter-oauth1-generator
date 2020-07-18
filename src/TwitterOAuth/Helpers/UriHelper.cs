using System.Linq;
using System.Collections.Generic;
using System;

namespace Twitter.OAuth.Helpers
{
    internal static class UriHelper
    {
        public static string GetBaseUrl(string url)
        {
            Uri uri = new Uri(url);

            return uri.GetLeftPart(UriPartial.Path);
        }

        public static Dictionary<string, string> GetQueryParameters(string url)
        {
            Uri uri = new Uri(url);

            Dictionary<string, string> parameters = uri.Query
            .TrimStart('?')
            .Split('&', StringSplitOptions.RemoveEmptyEntries)
            .ToDictionary(t => t.Split('=', StringSplitOptions.RemoveEmptyEntries)[0],
                          t => t.Split('=', StringSplitOptions.RemoveEmptyEntries)[1]);

            return parameters;
        }
    }
}