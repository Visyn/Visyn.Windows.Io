using System;
using System.Globalization;
using Visyn.Windows.Io.Exceptions;

namespace Visyn.Windows.Io.FileHelper.Converters
{
    /// <summary>
    /// Convert a string to a short integer
    /// </summary>
    internal sealed class UInt16Converter : CultureConverter<UInt16>
    {
        /// <summary>
        /// Convert a number to a short integer
        /// </summary>
        public UInt16Converter() : this(ConverterFactory.DefaultDecimalSeparator) { }

        /// <summary>
        /// Convert a number to a short integer
        /// </summary>
        /// <param name="decimalSeparator">Decimal separator</param>
        public UInt16Converter(string decimalSeparator) : base(decimalSeparator) { }

        /// <summary>
        /// Parse a string to a short integer
        /// </summary>
        /// <param name="text">string representing short integer</param>
        /// <returns>short integer value</returns>
        protected override object ParseString(string text)
        {
            ushort res;
            if (UInt16.TryParse(StringHelper.RemoveBlanks(text), NumberStyles.Number | NumberStyles.AllowExponent, Culture, out res))
                return res;
            throw new ConvertException(text, Type);
        }
    }
}