using System;
using System.Globalization;
using Visyn.Windows.Io.Exceptions;

namespace Visyn.Windows.Io.FileHelper.Converters
{
    /// <summary>
    /// Convert a value to a single floating point
    /// </summary>
    internal sealed class SingleConverter : CultureConverter<Single>
    {
        /// <summary>
        /// Convert a value to a single floating point
        /// </summary>
        public SingleConverter() : this(ConverterFactory.DefaultDecimalSeparator) { }

        /// <summary>
        /// Convert a value to a single floating point
        /// </summary>
        /// <param name="decimalSeparator">dot or comma for separator</param>
        public SingleConverter(string decimalSeparator) : base( decimalSeparator) { }

        /// <summary>
        /// Convert a string to an single precision floating point
        /// </summary>
        /// <param name="text">String value to convert</param>
        /// <returns>Single floating point value</returns>
        protected override object ParseString(string text)
        {
            float res;
            if (Single.TryParse(StringHelper.RemoveBlanks(text), NumberStyles.Number | NumberStyles.AllowExponent, Culture, out res))
                return res;
            throw new ConvertException(text, Type);
        }
    }
}