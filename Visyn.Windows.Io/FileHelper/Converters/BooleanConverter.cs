using System;
using Visyn.Windows.Io.Exceptions;

namespace Visyn.Windows.Io.FileHelper.Converters
{
    /// <summary>
    /// Convert an input value to a boolean,  allows for true false values
    /// </summary>
    internal sealed class BooleanConverter : ConverterBase<bool>
    {
        private readonly string _trueString = null;
        private readonly string _falseString = null;
        private readonly string _trueStringLower = null;
        private readonly string _falseStringLower = null;

        /// <summary>
        /// Simple boolean converter
        /// </summary>
        public BooleanConverter() { }

        /// <summary>
        /// Boolean converter with true false values
        /// </summary>
        /// <param name="trueText">True string</param>
        /// <param name="falseText">False string</param>
        public BooleanConverter(string trueText, string falseText)
        {
            _trueString = trueText;
            _falseString = falseText;
            _trueStringLower = trueText.ToLower();
            _falseStringLower = falseText.ToLower();
        }

        /// <summary>
        /// convert a string to a boolean value
        /// </summary>
        /// <param name="text">string to convert</param>
        /// <returns>boolean value</returns>
        public override object StringToField(string text)
        {
            var textLower = text.ToLower();

            if (_trueString == null)
            {
                textLower = textLower.Trim();
                switch (textLower)
                {
                    case "true":
                    case "1":
                    case "y":
                    case "t":
                        return true;
                    case "false":
                    case "0":
                    case "n":
                    case "f":

                    // I don't think that this case is possible without overriding the CustomNullHandling
                    // and it is possible that defaulting empty fields to be false is not correct
                    case "":
                        return false;
                    default:
                        throw new ConvertException(text,
                                    typeof(bool),
                                    $"The string: {text} can't be recognized as boolean using default true/false values.");
                }
            }
            //  Most of the time the strings should match exactly.  To improve performance
            //  we skip the trim if the exact match is true
            if (textLower == _trueStringLower)  return true;
            if (textLower == _falseStringLower) return false;
            textLower = textLower.Trim();
            if (textLower == _trueStringLower) return true;
            if (textLower == _falseStringLower) return false;
            throw new ConvertException(text,
                        typeof(bool),
                        $"The string: {text} can't be recognized as boolean using the true/false values: { _trueString }/{_falseString}");
        }

        /// <summary>
        /// Convert to a true false string
        /// </summary>
        /// <param name="from"></param>
        /// <returns></returns>
        public override string FieldToString(object from) 
            => Convert.ToBoolean(@from) ? (_trueString ?? "True") : (_falseString ?? "False");
    }
}