#region Copyright (c) 2015-2017 Visyn
// The MIT License(MIT)
// 
// Copyright(c) 2015-2017 Visyn
// 
// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the "Software"), to deal
// in the Software without restriction, including without limitation the rights
// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions:
//
// The above copyright notice and this permission notice shall be included in all
// copies or substantial portions of the Software.
//
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
// SOFTWARE.
#endregion

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
