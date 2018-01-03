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
using System.ComponentModel;

namespace Visyn.Windows.Io.FileHelper.Attributes
{
    /// <summary>
    /// Indicates the value to assign to the field in the case of a NULL value.
    /// A default value if none supplied in the field itself.
    /// </summary>
    /// <remarks>
    /// You must specify a string and a converter that can be converted to the
    /// type or an object of the correct type to be directly assigned.
    /// <para/>
    /// See the <a href="http://www.filehelpers.net/mustread">complete attributes list</a> for more
    /// information and examples of each one.
    /// </remarks>

    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property)]
    public sealed class FieldNullValueAttribute : Attribute
    {
        /// <summary>Default value for a null value.</summary>
        public object NullValue { get; private set; }

//		internal bool NullValueOnWrite = false;


        /// <summary>
        /// Defines the default in event of a null value.
        /// Object must be of teh correct type
        /// </summary>
        /// <param name="nullValue">The value to assign the case of a NULL value.</param>
        public FieldNullValueAttribute(object nullValue)
        {
            NullValue = nullValue;
//			NullValueOnWrite = useOnWrite;
        }

//		/// <summary>Defines the default for a null value.</summary>
//		/// <param name="nullValue">The value to assign in the "NULL" case.</param>
//		public FieldNullValueAttribute(object nullValue): this(nullValue, false)
//		{}

//		/// <summary>Indicates a type and a string to be converted to that type.</summary>
//		/// <param name="type">The type of the null value.</param>
//		/// <param name="nullValue">The string to be converted to the specified type.</param>
//		/// <param name="useOnWrite">Indicates that if the field has that value when the library writes, then the engine use an empty string.</param>
//		public FieldNullValueAttribute(Type type, string nullValue, bool useOnWrite):this(Convert.ChangeType(nullValue, type, null), useOnWrite)
//		{}

        /// <summary>Indicates a type and a string to be converted to that type.</summary>
        /// <param name="type">The type of the null value.</param>
        /// <param name="nullValue">The string to be converted to the specified type.</param>
        public FieldNullValueAttribute(Type type, string nullValue)
            : this(TypeDescriptor.GetConverter(type).ConvertFromString(nullValue)) {}
    }
}
