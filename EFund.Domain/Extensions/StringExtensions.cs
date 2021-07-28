using System.Text.RegularExpressions;

namespace Api.Extensions
{
    public static class StringExtensions
    {
        public static bool IsAddress(this string str) => Regex.IsMatch(str, @"0x[0-9a-z]{40}");
    }
}
