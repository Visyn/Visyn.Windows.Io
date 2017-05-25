using System.Globalization;
using Visyn.Windows.Io.Exceptions;

namespace Visyn.Windows.Io.FileHelper.Converters
{
    /// <summary>
    /// Convert a value to a integer
    /// </summary>
    internal sealed class Int32Converter : CultureConverter<int>
    {
        /// <summary>
        /// Convert a value to a integer
        /// </summary>
        public Int32Converter() : this(ConverterFactory.DefaultDecimalSeparator) { }

        /// <summary>
        /// Convert a value to a integer
        /// </summary>
        /// <param name="decimalSep">dot or comma for separator</param>
        public Int32Converter(string decimalSep) : base( decimalSep) { }

        /// <summary>
        /// Convert a string to an integer
        /// </summary>
        /// <param name="text">String value to convert</param>
        /// <returns>integer value</returns>
        protected override object ParseString(string text)
        {
            int res;
            if (int.TryParse(StringHelper.RemoveBlanks(text), NumberStyles.Number | NumberStyles.AllowExponent, Culture, out res))
                    return res;
            throw new ConvertException(text, Type);
        }
    }
}