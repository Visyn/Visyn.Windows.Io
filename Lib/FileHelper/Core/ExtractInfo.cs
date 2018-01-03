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
using System.Diagnostics;
using Visyn.Serialize;

namespace Visyn.Windows.Io.FileHelper.Core
{
    /// <summary>
    /// A single field extracted from the 'record'
    /// </summary>
    /// <remarks>
    /// Record is defined by the way the data is input
    /// </remarks>
    [DebuggerDisplay("{ExtractedString()} [{ExtractedFrom}-{ExtractedTo}]")]
    public struct ExtractedInfo
    {
        /// <summary>
        /// Allows for the actual string to be overridden 
        /// </summary>
        internal string mCustomExtractedString;

        /// <summary>
        /// The string value of the field extracted from the record
        /// </summary>
        /// <returns></returns>
        public string ExtractedString()
        {
            return mCustomExtractedString ?? mLine.Substring(ExtractedFrom, ExtractedTo - ExtractedFrom + 1);
        }

        /// <summary>
        /// Length of the field
        /// </summary>
        public int Length => ExtractedTo - ExtractedFrom + 1;

        /// <summary>
        /// Contains the line of data read
        /// </summary>
        public LineInfo mLine;

        /// <summary>
        /// Position of first character of the field in mLine.mLine
        /// </summary>
        public int ExtractedFrom;

        /// <summary>
        /// Position of last character of the field in mLine.mLine
        /// </summary>
        public int ExtractedTo;

        /// <summary>
        /// Extract the rest of the line into my variable
        /// </summary>
        /// <param name="line"></param>
        public ExtractedInfo(LineInfo line)
        {
            mLine = line;
            ExtractedFrom = line.mCurrentPos;
            ExtractedTo = line.mLineStr.Length - 1;
            mCustomExtractedString = null;
        }

        /// <summary>
        /// Extract field from current position to specified position
        /// </summary>
        /// <param name="line">Record information</param>
        /// <param name="extractTo">Position to extract to</param>
        public ExtractedInfo(LineInfo line, int extractTo)
        {
            mLine = line;
            ExtractedFrom = line.mCurrentPos;
            ExtractedTo = extractTo - 1;
            mCustomExtractedString = null;
        }

        /// <summary>
        /// Allow a default string or a specific string for this
        /// variable to be applied
        /// </summary>
        /// <param name="customExtract"></param>
        public ExtractedInfo(string customExtract)
        {
            mLine = null;
            ExtractedFrom = 0;
            ExtractedTo = 0;
            mCustomExtractedString = customExtract;
        }

        internal static readonly ExtractedInfo Empty = new ExtractedInfo(string.Empty);

        /// <summary>
        /// Check that the value returned only has these specific characters in it
        /// </summary>
        /// <param name="sortedArray">Sorted array of characters to check</param>
        /// <returns>false if any character in value does not match</returns>
        public bool HasOnlyThisChars(char[] sortedArray)
        {
            // Check if the chars at pos or right are empty ones
            if (mCustomExtractedString != null)
            {
                var pos = 0;
                while (pos < mCustomExtractedString.Length)
                {
                    if (Array.BinarySearch(sortedArray, mCustomExtractedString[pos]) < 0)
                        return false;
                    pos++;
                }
                return true;
            }
            else
            {
                var pos = ExtractedFrom;
                while (pos <= ExtractedTo)
                {
                    if (Array.BinarySearch(sortedArray, mLine.mLineStr[pos]) < 0) return false;
                    pos++;
                }

                return true;
            }
        }
    }
}
