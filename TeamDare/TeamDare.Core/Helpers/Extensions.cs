using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;

namespace TeamDare.Core.Helpers
{
    public static class Extensions
    {
        public static bool ContainsAny(this string haystack, params string[] needles)
        {
            var culture = new CultureInfo("en");
            foreach (string needle in needles)
            {
                if (culture.CompareInfo.IndexOf(haystack, needle, CompareOptions.IgnoreCase) >= 0)
                    return true;
            }

            return false;
        }
    }
}
