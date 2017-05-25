using System;
using System.Globalization;
using Visyn.Windows.Io.Exceptions;

namespace Visyn.Windows.Io.FileHelper.Converters
{
    /// <summary>
    /// Convert a value to a single floating point
    /// </summary>
    internal sealed class DoubleConverter : CultureConverter<double>
    {
        /// <summary>
        /// Convert a value to a floating point
        /// </summary>
        public DoubleConverter() : this(ConverterFactory.DefaultDecimalSeparator) { }

        /// <summary>
        /// Convert a value to a floating point
        /// </summary>
        /// <param name="decimalSeparator">dot or comma for separator</param>
        public DoubleConverter(string decimalSeparator) : base( decimalSeparator) { }

        /// <summary>
        /// Convert a string to an floating point
        /// </summary>
        /// <param name="text">String value to convert</param>
        /// <returns>Floating point value</returns>
        protected override object ParseString(string text)
        {
            double res;
            if (double.TryParse(StringHelper.RemoveBlanks(text),
                NumberStyles.Number | NumberStyles.AllowExponent,
                Culture,
                out res)) return res;
            throw new ConvertException(text, Type);
        }
    }
}