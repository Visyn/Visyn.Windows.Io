using System;
using System.Globalization;
using Visyn.Windows.Io.Exceptions;

namespace Visyn.Windows.Io.FileHelper.Converters
{
    /// <summary>
    /// Unsigned long converter
    /// </summary>
    internal sealed class UInt64Converter : CultureConverter<UInt64>
    {
        /// <summary>
        /// Unsigned long converter
        /// </summary>
        public UInt64Converter() : this(ConverterFactory.DefaultDecimalSeparator) { }

        /// <summary>
        /// Unsigned long with decimal separator
        /// </summary>
        /// <param name="decimalSeparator">dot or comma for separator</param>
        public UInt64Converter(string decimalSeparator) : base( decimalSeparator) { }

        /// <summary>
        /// Convert a string to an unsigned integer long
        /// </summary>
        /// <param name="text">String value to convert</param>
        /// <returns>Unsigned long value</returns>
        protected override object ParseString(string text)
        {
            ulong res;
            if (UInt64.TryParse(StringHelper.RemoveBlanks(text), NumberStyles.Number | NumberStyles.AllowExponent, Culture, out res))
                return res;
            throw new ConvertException(text, Type);
        }
    }
}