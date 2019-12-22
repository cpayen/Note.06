using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace Note.Core.Helpers
{
    public static class SlugHelper
    {
        public const string ValidSludRegex = "^[a-z0-9]+(?:-[a-z0-9]+)*$";

        public static bool Validate(string slug)
        {
            return Regex.IsMatch(slug, ValidSludRegex);
        }

        public static string GetUniqueSlug(string slug, IEnumerable<string> existingSlugs)
        {
            var uniqueSlug = slug;

            if (existingSlugs != null && existingSlugs.Any())
            {
                if (existingSlugs.Contains(slug))
                {
                    uint counter = 2;
                    uniqueSlug = $"{slug}-{counter}";

                    while (existingSlugs.Contains(uniqueSlug))
                    {
                        counter++;
                        uniqueSlug = $"{slug}-{counter}";
                    }
                }
            }

            return uniqueSlug;
        }
    }
}
