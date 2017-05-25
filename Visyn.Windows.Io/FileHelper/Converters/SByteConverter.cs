using System;
using System.Globalization;
using Visyn.Windows.Io.Exceptions;

namespace Visyn.Windows.Io.FileHelper.Converters
{
    /// <summary>
    /// Signed byte converter (8 bit signed integer)
    /// </summary>
    internal sealed class SByteConverter : CultureConverter
    {
        /// <summary>
        /// Signed byte converter (8 bit signed integer)
        /// </summary>
        public SByteConverter() : this(ConverterFactory.DefaultDecimalSep) { }

        /// <summary>
        /// Signed byte converter (8 bit signed integer)
        /// </summary>
        /// <param name="decimalSep">dot or comma for separator</param>
        public SByteConverter(string decimalSep)
            : base(typeof(SByte), decimalSep) { }

        /// <summary>
        /// Convert a string to an signed byte
        /// </summary>
        /// <param name="from">String value to convert</param>
        /// <returns>Signed byte value</returns>
        protected override object ParseString(string from)
        {
            sbyte res;
            if (!SByte.TryParse(StringHelper.RemoveBlanks(from), NumberStyles.Number, Culture, out res))
                throw new ConvertException(from, Type);
            return res;
        }
    }
}