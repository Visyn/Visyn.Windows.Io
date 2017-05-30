using System;
using System.Globalization;
using System.Reflection;
using System.Text;
using Visyn.Windows.Io.Exceptions;
using Visyn.Windows.Io.FileHelper.Core;
using Visyn.Windows.Io.FileHelper.Enums;

namespace Visyn.Windows.Io.FileHelper.Fields
{
    /// <summary>
    /// Define a field that is delimited, eg CSV and may be quoted
    /// </summary>
    public sealed class DelimitedField : FieldBase
    {
        #region "  Constructor  "

        private static readonly CompareInfo mCompare = StringHelper.CreateComparer();

        /// <summary>
        /// Create an empty delimited field structure
        /// </summary>
        private DelimitedField() {}

        /// <summary>
        /// Create a delimited field with defined separator
        /// </summary>
        /// <param name="fi">field info structure</param>
        /// <param name="sep">field separator</param>
        internal DelimitedField(FieldInfo fi, string sep)
            : base(fi)
        {
            QuoteChar = '\0';
            QuoteMultiline = MultilineMode.AllowForBoth;
            Separator = sep; // string.Intern(sep);
        }

        #endregion

        #region "  Properties  "

        /// <summary>
        /// Set the separator string
        /// </summary>
        /// <remarks>Also sets the discard count</remarks>
        internal string Separator { get; set; }

        public override int CharsToDiscard => IsLast && IsArray == false ? 0 : Separator.Length;

        /// <summary>
        /// allow a quoted multiline format
        /// </summary>
        public MultilineMode QuoteMultiline { get; set; }

        /// <summary>
        /// whether quotes are optional for read and / or write
        /// </summary>
        public QuoteMode QuoteMode { get; set; }

        /// <summary>
        /// quote character around field (and repeated within it)
        /// </summary>
        public char QuoteChar { get; set; }

        #endregion

        #region "  Overrides String Handling  "

        /// <summary>
        /// Extract the field from the delimited file, removing separators and quotes
        /// and any duplicate quotes within the record
        /// </summary>
        /// <param name="line">line containing record input</param>
        /// <returns>Extract information</returns>
        public override ExtractedInfo ExtractFieldString(LineInfo line)
        {
            if (IsOptional && line.IsEOL())  return Visyn.Windows.Io.FileHelper.Core.ExtractedInfo.Empty;

            if (QuoteChar == '\0') return BasicExtractString(line);
            if (TrimMode == TrimMode.Both || TrimMode == TrimMode.Left)
                line.TrimStart(TrimChars);

            var quotedStr = QuoteChar.ToString();
            if (line.StartsWith(quotedStr)) {
                var res = StringHelper.ExtractQuotedString(line,
                    QuoteChar,
                    QuoteMultiline == MultilineMode.AllowForBoth || QuoteMultiline == MultilineMode.AllowForRead);

                if (TrimMode == TrimMode.Both || TrimMode == TrimMode.Right)
                    line.TrimStart(TrimChars);

                if (!IsLast &&
                    !line.StartsWith(Separator) &&
                    !line.IsEOL()) {
                    throw new BadUsageException(line,
                        "The field " + FieldInfo.Name + " is quoted but the quoted char: " + quotedStr +
                        " not is just before the separator (You can use [FieldTrim] to avoid this error)");
                }
                return res;
            }
            if (QuoteMode == QuoteMode.OptionalForBoth ||
                QuoteMode == QuoteMode.OptionalForRead)
                return BasicExtractString(line);
            if (line.StartsWithTrim(quotedStr)) {
                throw new BadUsageException(
                    $"The field '{FieldInfo.Name}' has spaces before the QuotedChar at line {line.mReader.LineNumber}. Use the TrimAttribute to by pass this error. Field String: {line.CurrentString}");
            }
            throw new BadUsageException(
                $"The field '{FieldInfo.Name}' does not begin with the QuotedChar at line {line.mReader.LineNumber}. You can use FieldQuoted(QuoteMode.OptionalForRead) to allow optional quoted field. Field String: {line.CurrentString}");
        }

        private Visyn.Windows.Io.FileHelper.Core.ExtractedInfo BasicExtractString(LineInfo line)
        {
            if (IsLast && !IsArray) {

                if (line.IndexOf(Separator) == -1) return new Visyn.Windows.Io.FileHelper.Core.ExtractedInfo(line);

                // Now check for one extra separator
                throw new BadUsageException(line.mReader.LineNumber, line.mCurrentPos, 
                    $"Delimiter '{Separator}' found after the last field '{FieldInfo.Name}' (the file is wrong or you need to add a field to the record class)");
            }
            var sepPos = line.IndexOf(Separator);

            if (sepPos != -1) return new Visyn.Windows.Io.FileHelper.Core.ExtractedInfo(line, sepPos);
            if (IsLast && IsArray) return new Visyn.Windows.Io.FileHelper.Core.ExtractedInfo(line);

            if ( NextIsOptional == false)
            {
                if (IsFirst && line.EmptyFromPos()) {
                    throw new FileHelpersException(line.mReader.LineNumber, line.mCurrentPos, 
                        $"The line {line.mReader.LineNumber} is empty. Maybe you need to use the attribute [IgnoreEmptyLines] in your record class.");
                }
                throw new FileHelpersException(line.mReader.LineNumber, line.mCurrentPos, 
                    $"Delimiter '{Separator}' not found after field '{FieldInfo.Name}' (the record has less fields, the delimiter is wrong or the next field must be marked as optional).");
            }
            sepPos = line.mLineStr.Length;

            return new Visyn.Windows.Io.FileHelper.Core.ExtractedInfo(line, sepPos);
        }

        /// <summary>
        /// Output the field string adding delimiters and any required quotes
        /// </summary>
        /// <param name="sb">buffer to add field to</param>
        /// <param name="fieldValue">value object to add</param>
        /// <param name="isLast">Indicates if we are processing last field</param>
        public override void CreateFieldString(StringBuilder sb, object fieldValue, bool isLast)
        {
            var text = base.CreateFieldString(fieldValue);
            var hasNewLine = mCompare.IndexOf(text, Environment.NewLine, CompareOptions.Ordinal) >= 0;

            // If have a new line and this is not allowed.  We throw an exception
            if (hasNewLine &&
                (QuoteMultiline == MultilineMode.AllowForRead ||
                 QuoteMultiline == MultilineMode.NotAllow)) {
                throw new BadUsageException($"One value for the field {FieldInfo.Name} has a new line inside. To allow write this value you must add a FieldQuoted attribute with the multiline option in true.");
            }

            // Add Quotes If:
            //     -  optional == false
            //     -  is optional and contains the separator
            //     -  is optional and contains a new line

            if ((QuoteChar != '\0') &&
                (QuoteMode == QuoteMode.AlwaysQuoted ||
                 QuoteMode == QuoteMode.OptionalForRead ||
                 ((QuoteMode == QuoteMode.OptionalForWrite || QuoteMode == QuoteMode.OptionalForBoth)
                  && mCompare.IndexOf(text, Separator, CompareOptions.Ordinal) >= 0) || hasNewLine))
                StringHelper.CreateQuotedString(sb, text, QuoteChar);
            else
                sb.Append(text);

            if (isLast == false) sb.Append(Separator);
        }

        /// <summary>
        /// create a field base class and populate the delimited values
        /// base class will add its own values
        /// </summary>
        /// <returns>fieldbase ready to be populated with extra info</returns>
        protected override FieldBase CreateClone()
        {
            var res = new DelimitedField {
                Separator = Separator,
                QuoteChar = QuoteChar,
                QuoteMode = QuoteMode,
                QuoteMultiline = QuoteMultiline
            };
            return res;
        }

        #endregion
    }
}