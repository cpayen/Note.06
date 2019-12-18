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
    }
}
