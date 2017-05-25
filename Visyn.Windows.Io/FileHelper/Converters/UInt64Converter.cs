using System;
using System.Globalization;
using Visyn.Windows.Io.Exceptions;

namespace Visyn.Windows.Io.FileHelper.Converters
{
    /// <summary>
    /// Unsigned long converter
    /// </summary>
    internal sealed class UInt64Converter : CultureConverter
    {
        /// <summary>
        /// Unsigned long converter
        /// </summary>
        public UInt64Converter() : this(ConverterFactory.DefaultDecimalSep) { }

        /// <summary>
        /// Unsigned long with decimal separator
        /// </summary>
        /// <param name="decimalSep">dot or comma for separator</param>
        public UInt64Converter(string decimalSep)
            : base(typeof(UInt64), decimalSep) { }

        /// <summary>
        /// Convert a string to an unsigned integer long
        /// </summary>
        /// <param name="from">String value to convert</param>
        /// <returns>Unsigned long value</returns>
        protected override object ParseString(string from)
        {
            ulong res;
            if (
                !UInt64.TryParse(StringHelper.RemoveBlanks(from),
                    NumberStyles.Number | NumberStyles.AllowExponent,
                    Culture,
                    out res))
                throw new ConvertException(from, Type);
            return res;
        }
    }
}