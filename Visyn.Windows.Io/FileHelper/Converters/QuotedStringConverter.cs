namespace Visyn.Windows.Io.FileHelper.Converters
{
    public class QuotedStringConverter : ConverterBase<string>
    {
        public override object StringToField(string text)
        {
           // var text = fieldValue as string;
            if (string.IsNullOrEmpty(text)) return string.Empty;
            return text.Trim('"');
        }

        public override string FieldToString(object fieldValue)
        {
            return $"\"{fieldValue}\"";
           
        }
    }
}