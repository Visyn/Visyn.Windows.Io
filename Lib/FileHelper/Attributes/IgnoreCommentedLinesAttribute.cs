#region Copyright (c) 2015-2018 Visyn
// The MIT License(MIT)
// 
// Copyright (c) 2015-2018 Visyn
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
using Visyn.Exceptions;
using Visyn.Windows.Io.Exceptions;

namespace Visyn.Windows.Io.FileHelper.Attributes
{
    /// <summary>Indicates that the engine must ignore commented lines while reading.</summary>
    /// <remarks>See the <a href="http://www.filehelpers.net/mustread">complete attributes list</a> for more information and examples of each one.</remarks>
    [AttributeUsage(AttributeTargets.Class)]
	[Obsolete("This attribute will be removed in next version, use INotifyRead to provide custom filtering")]
    public sealed class IgnoreCommentedLinesAttribute : Attribute
    {
        /// <summary>
        /// Comment marker string
        /// </summary>
        public string CommentMarker { get; private set; }

        /// <summary>
        /// Can the comment marker have preceding spaces
        /// </summary>
        public bool AnyPlace { get; private set; }

        /// <summary>Indicates that the engine will ignore commented lines while reading.
        /// (The Comment Marker can have any number of spaces or tabs to the left)</summary>
        /// <param name="commentMarker">The comment marker used to ignore the lines</param>
        public IgnoreCommentedLinesAttribute(string commentMarker)
            : this(commentMarker, true) {}

        /// <summary>Indicates that the engine will ignore commented lines while reading.</summary>
        /// <param name="commentMarker">The comment marker used to ignore the lines</param>
        /// <param name="anyPlace">Indicates if the comment can have spaces or tabs to the left (true by default)</param>
        public IgnoreCommentedLinesAttribute(string commentMarker, bool anyPlace)
        {
            if (commentMarker == null ||
                commentMarker.Trim().Length == 0)
                throw new BadUsageException("The comment string parameter can't be null or empty.");

            CommentMarker = commentMarker.Trim();
            AnyPlace = anyPlace;
        }
    }
}
