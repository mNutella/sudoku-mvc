using System;
using System.Globalization;
using System.Text;

namespace Sudoku.Mvc.Common.Extensions
{
    public static class StringExtensions
    {
        public static string Limit(this string input, int maxSize)
        {
            if (string.IsNullOrEmpty(input))
            {
                return string.Empty;
            }

            return input.Length <= maxSize
                ? input
                : input.Substring(0, maxSize) + " ***** LIMITED TO " + maxSize + " BYTES *****";
        }

        public static bool IsNullOrWhiteSpace(this string input)
        {
            return string.IsNullOrWhiteSpace(input);
        }

        public static bool IsNullOrEmpty(this string input)
        {
            return string.IsNullOrEmpty(input);
        }

        public static string RemoveAllBeforeChar(this string input, char divider)
        {
            var output = input.Substring(input.IndexOf(divider) + 1);

            return output;
        }

        public static string GetUntilOrEmpty(this string text, string stopAt = "-")
        {
            if (!text.IsNullOrWhiteSpace())
            {
                var charLocation = text.IndexOf(stopAt, StringComparison.Ordinal);

                if (charLocation > 0)
                {
                    return text.Substring(0, charLocation);
                }
            }

            return string.Empty;
        }

        public static string ConvertToAscii(this string unicode)
        {
            var mapping = new IdnMapping();
            var ascii = mapping.GetAscii(unicode);
            return ascii;
        }

        public static string TryTruncateHeaderWithThreeDots(this string header, int threshold)
        {
            if (header.Length <= threshold)
            {
                return header;
            }

            var str = new StringBuilder(header.Substring(0, threshold)).Append("...");

            return str.ToString();
        }
    }
}