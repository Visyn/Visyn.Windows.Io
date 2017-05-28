using System;
using System.Globalization;

namespace Visyn.Windows.Io.FileHelper.Converters
{
    public class TimeSpanToSecondsConverter : ConverterBase<TimeSpan>, IHasUnits
    {
        #region Implementation of IHasUnits

        public string Units => "S";

        #endregion

        public override object StringToField(string text)
        {
            double seconds;
            return double.TryParse(text, out seconds) ? TimeSpan.FromSeconds(seconds) : TimeSpan.Zero;
        }

        public override string FieldToString(object fieldValue)
        {
            if (fieldValue is TimeSpan) return ((TimeSpan)fieldValue).TotalSeconds.ToString(CultureInfo.InvariantCulture);
            return 0.ToString();
        }


    }
}