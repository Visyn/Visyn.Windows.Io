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
using Visyn.Windows.Io.FileHelper.Enums;

namespace Visyn.Windows.Io.FileHelper.Attributes
{
    /// <summary>Allow to declaratively set what records must be included or excluded while reading.</summary>
    /// <remarks>See the <a href="http://www.filehelpers.net/mustread">Complete attributes list</a> for more information and examples of each one.</remarks>
    /// <example>
    /// [DelimitedRecord(",")] 
    /// [ConditionalRecord(RecordCondition.ExcludeIfBegins, "//")] 
    /// public class ConditionalType1 
    /// { 
    /// 
    /// // Using Regular Expressions example
    /// [DelimitedRecord(",")]
    /// [ConditionalRecord(RecordCondition.IncludeIfMatchRegex, ".*abc??")]
    /// public class ConditionalType3
    /// { 
    /// </example>
    [AttributeUsage(AttributeTargets.Class)]
	//[Obsolete("This attribute will be removed in next version, is better to use INotifyRead and provide code for filtering")]
    public sealed class ConditionalRecordAttribute : Attribute
    {
        /// <summary> The condition used to include or exclude each record </summary>
        public RecordCondition Condition { get; private set; }

        /// <summary> The selector (match string) for the condition. </summary>
        /// <remarks>The string will have a condition, included, excluded start with etc</remarks>
        public string ConditionSelector { get; private set; }

        /// <summary>Allow to declaratively show what records must be included or excluded</summary>
        /// <param name="condition">The condition used to include or exclude each record <see cref="RecordCondition"/>conditions</param>
        /// <param name="conditionSelector">The selector (match string) for the condition.</param>
        public ConditionalRecordAttribute(RecordCondition condition, string conditionSelector)
        {
            Condition = condition;
            ConditionSelector = conditionSelector;
            ExHelper.CheckNullOrEmpty(conditionSelector, "conditionSelector");
        }
    }
}
