using System;

namespace Visyn.Windows.Io.FileHelper.Core
{
    /// <summary>
    /// Helpers that work with conditions to make them easier to write
    /// </summary>
    internal static class ConditionHelper
    {
        /// <summary>
        /// Test whether string begins with another string
        /// </summary>
        /// <param name="line">string to test</param>
        /// <param name="selector">value we want to check for</param>
        /// <returns>true if string begins with the selector</returns>
        [Obsolete("Use line.StartsWith...")]
        public static bool BeginsWith(this string line, string selector)
        {
            return line.StartsWith(selector);
        }

        /// <summary>
        /// Test whether string ends with another string
        /// </summary>
        /// <param name="line">string to test</param>
        /// <param name="selector">value we want to check for</param>
        /// <returns>true if string ends with the selector</returns>
        [Obsolete("Use line.EndsWith...")]
        public static bool EndsWith(this string line, string selector)
        {
            return line.EndsWith(selector);
        }

        /// <summary>
        /// Test whether string contains with another string
        /// </summary>
        /// <param name="line">string to test</param>
        /// <param name="selector">value we want to check for</param>
        /// <returns>true if string contains the selector</returns>
        [Obsolete("Use line.IndexOf >= 0...")]
        public static bool Contains(this string line, string selector)
        {
            return line.IndexOf(selector) >= 0;
        }

        /// <summary>
        /// Test whether string begins and ends with another string
        /// </summary>
        /// <param name="line">string to test</param>
        /// <param name="selector">value we want to check for</param>
        /// <returns>true if string begins and ends with the selector</returns>
        public static bool Enclosed(this string line, string selector)
        {
            return line.StartsWith(selector) && line.EndsWith(selector);
        }
    }
}