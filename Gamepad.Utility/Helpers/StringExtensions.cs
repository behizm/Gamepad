using System.Linq;

namespace Gamepad.Utility.Helpers
{
    public static class StringExtensions
    {
        public static bool IsNothing(this string text)
        {
            return string.IsNullOrEmpty(text) || string.IsNullOrWhiteSpace(text);
        }

        public static bool IsNotNothing(this string text)
        {
            return !string.IsNullOrEmpty(text) && !string.IsNullOrWhiteSpace(text);
        }

        public static string TrimAndLower(this string text)
        {
            if (text.IsNothing())
            {
                return string.Empty;
            }
            return text.Trim().ToLower();
        }

        public static string Fill(this string text, string with)
        {
            return text.Contains("{0}") ? text.Replace("{0}", with) : text;
        }

        public static string Fill(this string text, string[] with)
        {
            for (var i = 0; i < with.Count(); i++)
            {
                var symbol = "{" + i + "}";
                if (!text.Contains(symbol))
                    return text;

                text = text.Replace(symbol, with[i]);
            }
            return text;
        }
    }
}
