using System.Linq;
using System.Text.RegularExpressions;

namespace EpiServerBlogs.Logic
{
    public class CommentProvider
    {
        public static string GetTextWithoutBadWords(string text)
        {
            string[] badWords = {"admin", "kukushka", "админ", "кукушка"};
            const string badWordReplacement = ":P";
            const string patternTemplate = @"\b({0})(s?)\b";
            const RegexOptions options = RegexOptions.IgnoreCase;

            var badWordMatchers = badWords.Select(x => new Regex(string.Format(patternTemplate, x), options));

            return badWordMatchers.Aggregate(text, (current, matcher) => matcher.Replace(current, badWordReplacement));
        }
    }
}
