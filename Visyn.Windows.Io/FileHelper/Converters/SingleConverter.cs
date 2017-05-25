using System;
using System.Globalization;
using Visyn.Windows.Io.Exceptions;

namespace Visyn.Windows.Io.FileHelper.Converters
{
    /// <summary>
    /// Convert a value to a single floating point
    /// </summary>
    internal sealed class SingleConverter : CultureConverter
    {
        /// <summary>
        /// Convert a value to a single floating point
        /// </summary>
        public SingleConverter() : this(ConverterFactory.DefaultDecimalSep) { }

        /// <summary>
        /// Convert a value to a single floating point
        /// </summary>
        /// <param name="decimalSep">dot or comma for separator</param>
        public SingleConverter(string decimalSep)
            : base(typeof(Single), decimalSep) { }

        /// <summary>
        /// Convert a string to an single precision floating point
        /// </summary>
        /// <param name="from">String value to convert</param>
        /// <returns>Single floating point value</returns>
        protected override object ParseString(string from)
        {
            float res;
            if (
                !Single.TryParse(StringHelper.RemoveBlanks(from),
                    NumberStyles.Number | NumberStyles.AllowExponent,
                    Culture,
                    out res))
                throw new ConvertException(from, Type);
            return res;
        }
    }
}