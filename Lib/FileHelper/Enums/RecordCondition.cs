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

namespace Visyn.Windows.Io.FileHelper.Enums
{
    /// <summary>The condition used to include or exclude each record.</summary>
    public enum RecordCondition
    {
        /// <summary>No Condition, Include it always.</summary>
        None = 0,

        /// <summary>Include the record if it contains the selector string.</summary>
        IncludeIfContains,

        /// <summary>Include the record if it begins with selector string.</summary>
        IncludeIfBegins,

        /// <summary>Include the record if it ends with selector string.</summary>
        IncludeIfEnds,

        /// <summary>Include the record if it begins and ends with selector string.</summary>
        IncludeIfEnclosed,
        /// <summary>Include the record if it matches the regular expression passed as selector.</summary>
        IncludeIfMatchRegex,

        /// <summary>Exclude the record if it contains the selector string.</summary>
        ExcludeIfContains,

        /// <summary>Exclude the record if it begins with selector string.</summary>
        ExcludeIfBegins,

        /// <summary>Exclude the record if it ends with selector string.</summary>
        ExcludeIfEnds,

        /// <summary>Exclude the record if it begins and ends with selector string.</summary>
        ExcludeIfEnclosed,

        /// <summary>Exclude the record if it matches the regular expression passed as selector.</summary>
        ExcludeIfMatchRegex

    }
}
