using System;
using System.Globalization;
using Visyn.Windows.Io.Exceptions;

namespace Visyn.Windows.Io.FileHelper.Converters
{
    /// <summary>
    /// COnvert a string into a byte value
    /// </summary>
    internal sealed class ByteConverter : CultureConverter
    {
        /// <summary>
        /// Convert a string to a byte value using the default decimal separator
        /// </summary>
        public ByteConverter() : this(ConverterFactory.DefaultDecimalSep) { }

        /// <summary>
        /// Convert a string to a byte
        /// </summary>
        /// <param name="decimalSep">decimal separator to use '.' or ','</param>
        public ByteConverter(string decimalSep) : base(typeof(Byte), decimalSep) { }

        /// <summary>
        /// Convert a string to a byte value
        /// </summary>
        /// <param name="from">string to parse</param>
        /// <returns>byte value</returns>
        protected override object ParseString(string from)
        {
            byte res;
            if (!byte.TryParse(StringHelper.RemoveBlanks(from), NumberStyles.Number, Culture, out res))
                throw new ConvertException(from, Type);
            return res;
        }
    }
}