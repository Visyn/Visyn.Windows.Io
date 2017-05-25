using System.Globalization;
using Visyn.Windows.Io.Exceptions;

namespace Visyn.Windows.Io.FileHelper.Converters
{
    /// <summary>
    /// Convert a value to a short integer
    /// </summary>
    internal sealed class Int16Converter : CultureConverter<short>
    {
        /// <summary>
        /// Convert a value to a short integer
        /// </summary>
        public Int16Converter() : this(ConverterFactory.DefaultDecimalSeparator) { }

        /// <summary>
        /// Convert a value to a short integer
        /// </summary>
        /// <param name="decimalSep">dot or comma for separator</param>
        public Int16Converter(string decimalSep) : base( decimalSep) { }

        /// <summary>
        /// Convert a string to an short integer
        /// </summary>
        /// <param name="text">String value to convert</param>
        /// <returns>Short signed value</returns>
        protected override object ParseString(string text)
        {
            short res;
            if (short.TryParse(StringHelper.RemoveBlanks(text),
                NumberStyles.Number | NumberStyles.AllowExponent,
                Culture, out res))
                    return res;
            throw new ConvertException(text, Type);
        }
    }
}