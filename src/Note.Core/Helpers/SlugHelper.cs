using System.Text.RegularExpressions;

namespace Note.Core.Helpers
{
    public static class SlugHelper
    {
        public static bool Validate(string slug)
        {
            return Regex.IsMatch(slug, @"^(?i)[a-z0-9]+(?:-[a-z0-9]+)*$");
        }
    }
}
