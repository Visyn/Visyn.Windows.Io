using System.Globalization;
using Visyn.Windows.Io.Exceptions;

namespace Visyn.Windows.Io.FileHelper.Converters
{
    /// <summary>
    /// Convert a value to a short integer
    /// </summary>
    internal sealed class Int16Converter : CultureConverter
    {
        /// <summary>
        /// Convert a value to a short integer
        /// </summary>
        public Int16Converter() : this(ConverterFactory.DefaultDecimalSep) { }

        /// <summary>
        /// Convert a value to a short integer
        /// </summary>
        /// <param name="decimalSep">dot or comma for separator</param>
        public Int16Converter(string decimalSep) : base(typeof(short), decimalSep) { }

        /// <summary>
        /// Convert a string to an short integer
        /// </summary>
        /// <param name="from">String value to convert</param>
        /// <returns>Short signed value</returns>
        protected override object ParseString(string from)
        {
            short res;
            if (
                !short.TryParse(StringHelper.RemoveBlanks(from),
                    NumberStyles.Number | NumberStyles.AllowExponent,
                    Culture,
                    out res))
                throw new ConvertException(from, Type);
            return res;
        }
    }
}