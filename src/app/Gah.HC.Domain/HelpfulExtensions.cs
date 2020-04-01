namespace Gah.HC.Domain
{
    using System;
    using System.Globalization;
    using System.Text;
    using System.Text.RegularExpressions;

    /// <summary>
    /// Class HelpfulExtensions.
    /// </summary>
    internal static class HelpfulExtensions
    {
        /// <summary>
        /// Converts to slug.
        /// </summary>
        /// <param name="toConvert">To convert.</param>
        /// <returns>System.String.</returns>
        public static string ToSlug(this string toConvert) => Regex.Replace(toConvert.Replace(".", string.Empty, true, CultureInfo.InvariantCulture), @"\W", "-").ToLowerInvariant();
    }
}
