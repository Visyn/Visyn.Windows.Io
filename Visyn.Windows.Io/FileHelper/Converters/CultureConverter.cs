using System;
using System.Globalization;

namespace Visyn.Windows.Io.FileHelper.Converters
{
    /// <summary>
    /// Convert a numeric value with separators into a value
    /// </summary>
    internal abstract class CultureConverter : ConverterBase
    {
        /// <summary>
        /// Culture information based on the separator
        /// </summary>
        protected CultureInfo Culture;

        /// <summary>
        /// Type fo field being converted
        /// </summary>
 //       protected Type Type;

        /// <summary>
        /// Convert to a type given a decimal separator
        /// </summary>
        /// <param name="T">type we are converting</param>
        /// <param name="decimalSep">Separator</param>
        protected CultureConverter(Type T, string decimalSep) : base(T)
        {
            Culture = ConverterFactory.CreateCulture(decimalSep);
        }

        /// <summary>
        /// Convert the field to a string representation
        /// </summary>
        /// <param name="field">Object to convert</param>
        /// <returns>string representation</returns>
        public sealed override string FieldToString(object field) => ((IConvertible)field)?.ToString(Culture) ?? string.Empty;

        /// <summary>
        /// Convert a string to the object type
        /// </summary>
        /// <param name="text">String to convert</param>
        /// <returns>Object converted to</returns>
        public sealed override object StringToField(string text) => ParseString(text);

        /// <summary>
        /// Convert a string into the return object required
        /// </summary>
        /// <param name="from">Value to convert (string)</param>
        /// <returns>Converted object</returns>
        protected abstract object ParseString(string from);
    }
}