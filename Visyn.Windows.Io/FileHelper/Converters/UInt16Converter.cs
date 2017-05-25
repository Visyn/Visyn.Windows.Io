using System;
using System.Globalization;
using Visyn.Windows.Io.Exceptions;

namespace Visyn.Windows.Io.FileHelper.Converters
{
    /// <summary>
    /// Convert a string to a short integer
    /// </summary>
    internal sealed class UInt16Converter : CultureConverter
    {
        /// <summary>
        /// Convert a number to a short integer
        /// </summary>
        public UInt16Converter() : this(ConverterFactory.DefaultDecimalSep) { }

        /// <summary>
        /// Convert a number to a short integer
        /// </summary>
        /// <param name="decimalSep">Decimal separator</param>
        public UInt16Converter(string decimalSep)
            : base(typeof(UInt16), decimalSep) { }

        /// <summary>
        /// Parse a string to a short integer
        /// </summary>
        /// <param name="from">string representing short integer</param>
        /// <returns>short integer value</returns>
        protected override object ParseString(string from)
        {
            ushort res;
            if (
                !UInt16.TryParse(StringHelper.RemoveBlanks(from),
                    NumberStyles.Number | NumberStyles.AllowExponent,
                    Culture,
                    out res))
                throw new ConvertException(from, Type);
            return res;
        }
    }
}