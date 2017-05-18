using System.Text.RegularExpressions;

namespace Drape.Slug
{
    /// source: http://stackoverflow.com/a/2921135/6096446
    /// Extension converting string to slug
    public static class SlugParser
    {
        public static string ToSlug(this string phrase)
        {
            string str = phrase.ToLower();
            // remove invalid chars
            str = Regex.Replace(str, @"[^a-z0-9\s-]", "");
            // convert multiple spaces into one space   
            str = Regex.Replace(str, @"\s+", " ").Trim();
            // cut and trim 
            str = str.Substring(0, str.Length <= 45 ? str.Length : 45).Trim();
            // add hyphens
            str = Regex.Replace(str, @"\s", "-");
            return str;
        }
    }
}