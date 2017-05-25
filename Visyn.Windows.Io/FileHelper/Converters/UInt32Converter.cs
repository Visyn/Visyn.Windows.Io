using System;
using System.Globalization;
using Visyn.Windows.Io.Exceptions;

namespace Visyn.Windows.Io.FileHelper.Converters
{
    /// <summary>
    /// Unsigned integer converter
    /// </summary>
    internal sealed class UInt32Converter : CultureConverter<UInt32>
    {
        /// <summary>
        /// Unsigned integer converter
        /// </summary>
        public UInt32Converter() : this(ConverterFactory.DefaultDecimalSeparator) { }

        /// <summary>
        /// Unsigned integer converter with a decimal separator
        /// </summary>
        /// <param name="decimalSeparator">dot or comma for to separate decimal</param>
        public UInt32Converter(string decimalSeparator) : base( decimalSeparator) { }

        /// <summary>
        /// Convert a string to a unsigned integer value
        /// </summary>
        /// <param name="text">String value to parse</param>
        /// <returns>Unsigned integer object</returns>
        protected override object ParseString(string text)
        {
            uint res;
            if (UInt32.TryParse(StringHelper.RemoveBlanks(text), NumberStyles.Number | NumberStyles.AllowExponent, Culture, out res))
                return res;
            throw new ConvertException(text, Type);
        }
    }
}