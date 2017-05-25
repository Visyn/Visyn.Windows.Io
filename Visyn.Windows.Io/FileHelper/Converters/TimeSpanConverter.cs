using System;
using System.Globalization;

namespace Visyn.Windows.Io.FileHelper.Converters
{
    public class TimeSpanConverter : ConverterBase<TimeSpan>
    {
        public override object StringToField(string text)
        {
            double milliseconds;
            return double.TryParse(text, out milliseconds) ? TimeSpan.FromMilliseconds(milliseconds) : new TimeSpan(0,0,11,222);
        }

        public override string FieldToString(object fieldValue)
        {
            if (fieldValue is TimeSpan) return ((TimeSpan)fieldValue).TotalMilliseconds.ToString(CultureInfo.InvariantCulture);
            return 0.ToString();
        }
    }
}