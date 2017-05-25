using System;
using System.Globalization;
using Visyn.Windows.Io.Exceptions;

namespace Visyn.Windows.Io.FileHelper.Converters
{
    /// <summary>
    /// COnvert a string into a byte value
    /// </summary>
    internal sealed class ByteConverter : CultureConverter<byte>
    {
        /// <summary>
        /// Convert a string to a byte value using the default decimal separator
        /// </summary>
        public ByteConverter() : this(ConverterFactory.DefaultDecimalSeparator) { }

        /// <summary>
        /// Convert a string to a byte
        /// </summary>
        /// <param name="decimalSeparator">decimal separator to use '.' or ','</param>
        public ByteConverter(string decimalSeparator) : base(decimalSeparator) { }

        /// <summary>
        /// Convert a string to a byte value
        /// </summary>
        /// <param name="text">string to parse</param>
        /// <returns>byte value</returns>
        protected override object ParseString(string text)
        {
            byte res;
            if (!byte.TryParse(StringHelper.RemoveBlanks(text), NumberStyles.Number, Culture, out res))
                throw new ConvertException(text, Type);
            return res;
        }
    }
}