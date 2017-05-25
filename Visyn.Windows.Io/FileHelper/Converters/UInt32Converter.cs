using System;
using System.Globalization;
using Visyn.Windows.Io.Exceptions;

namespace Visyn.Windows.Io.FileHelper.Converters
{
    /// <summary>
    /// Unsigned integer converter
    /// </summary>
    internal sealed class UInt32Converter : CultureConverter
    {
        /// <summary>
        /// Unsigned integer converter
        /// </summary>
        public UInt32Converter() : this(ConverterFactory.DefaultDecimalSep) { }

        /// <summary>
        /// Unsigned integer converter with a decimal separator
        /// </summary>
        /// <param name="decimalSep">dot or comma for to separate decimal</param>
        public UInt32Converter(string decimalSep)
            : base(typeof(UInt32), decimalSep) { }

        /// <summary>
        /// Convert a string to a unsigned integer value
        /// </summary>
        /// <param name="from">String value to parse</param>
        /// <returns>Unsigned integer object</returns>
        protected override object ParseString(string from)
        {
            uint res;
            if (
                !UInt32.TryParse(StringHelper.RemoveBlanks(from),
                    NumberStyles.Number | NumberStyles.AllowExponent,
                    Culture,
                    out res))
                throw new ConvertException(from, Type);
            return res;
        }
    }
}