using System;
using System.Globalization;
using Visyn.Windows.Io.Exceptions;

namespace Visyn.Windows.Io.FileHelper.Converters
{
    /// <summary>
    /// Convert a value to a single floating point
    /// </summary>
    internal sealed class DoubleConverter : CultureConverter
    {
        /// <summary>
        /// Convert a value to a floating point
        /// </summary>
        public DoubleConverter() : this(ConverterFactory.DefaultDecimalSep) { }

        /// <summary>
        /// Convert a value to a floating point
        /// </summary>
        /// <param name="decimalSep">dot or comma for separator</param>
        public DoubleConverter(string decimalSep)
            : base(typeof(Double), decimalSep) { }

        /// <summary>
        /// Convert a string to an floating point
        /// </summary>
        /// <param name="from">String value to convert</param>
        /// <returns>Floating point value</returns>
        protected override object ParseString(string from)
        {
            double res;
            if (
                !Double.TryParse(StringHelper.RemoveBlanks(from),
                    NumberStyles.Number | NumberStyles.AllowExponent,
                    Culture,
                    out res))
                throw new ConvertException(from, Type);
            return res;
        }
    }
}