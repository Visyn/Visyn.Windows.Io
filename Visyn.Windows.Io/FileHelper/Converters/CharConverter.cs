using System;
using Visyn.Windows.Io.Exceptions;

namespace Visyn.Windows.Io.FileHelper.Converters
{
    /// <summary>
    /// Allow characters to be converted to upper and lower case automatically.
    /// </summary>
    internal sealed class CharConverter : ConverterBase<char>
    {
        /// <summary>
        /// whether we upper or lower case the character on input
        /// </summary>
        private enum CharFormat
        {
            /// <summary>
            /// Don't change the case
            /// </summary>
            NoChange = 0,

            /// <summary>
            /// Change to lower case
            /// </summary>
            Lower,

            /// <summary>
            /// change to upper case
            /// </summary>
            Upper,
        }

        /// <summary>
        /// default to not upper or lower case
        /// </summary>
        private readonly CharFormat _format = CharFormat.NoChange;


        /// <summary>
        /// Create a single character converter that does not upper or lower case result
        /// </summary>
        public CharConverter() : this("") // default,  no upper or lower case conversion
        { }

        /// <summary>
        /// Single character converter that optionally makes it upper (X) or lower case (x)
        /// </summary>
        /// <param name="format"> empty string for no upper or lower,  x for lower case,  X for Upper case</param>
        public CharConverter(string format)
        {
            switch (format.Trim())
            {
                case "x":
                case "lower":
                    _format = CharFormat.Lower;
                    break;

                case "X":
                case "upper":
                    _format = CharFormat.Upper;
                    break;

                case "":
                    _format = CharFormat.NoChange;
                    break;

                default:
                    throw new BadUsageException(
                        "The format of the Char Converter must be \"\", \"x\" or \"lower\" for lower case, \"X\" or \"upper\" for upper case");
            }
        }

        /// <summary>
        /// Extract the first character with optional upper or lower case
        /// </summary>
        /// <param name="text">String contents</param>
        /// <returns>Character (may be upper or lower case)</returns>
        public override object StringToField(string text)
        {
            if (string.IsNullOrEmpty(text)) return Char.MinValue;

            try
            {
                switch (_format)
                {
                    case CharFormat.NoChange: return text[0];

                    case CharFormat.Lower: return char.ToLower(text[0]);

                    case CharFormat.Upper: return char.ToUpper(text[0]);

                    default:
                        throw new ConvertException(text,
                            this.Type,
                            "Unknown char convert flag: " + _format);
                }
            }
            catch
            {
                throw new ConvertException(text, Type, "Upper or lower case of input string failed");
            }
        }

        /// <summary>
        /// Convert from a character to a string for output
        /// </summary>
        /// <param name="from">Character to convert from</param>
        /// <returns>String containing the character</returns>
        public override string FieldToString(object from)
        {
            switch (_format)
            {
                case CharFormat.NoChange: return Convert.ToChar(from).ToString();

                case CharFormat.Lower: return char.ToLower(Convert.ToChar(from)).ToString();

                case CharFormat.Upper: return char.ToUpper(Convert.ToChar(from)).ToString();

                default:
                    throw new ConvertException("", Type, "Unknown char convert flag " + _format);
            }
        }
    }
}