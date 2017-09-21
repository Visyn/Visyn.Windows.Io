using System;
using System.Diagnostics;

namespace Visyn.Windows.Io.FileHelper.Converters
{
    public class TimeSpanToHoursMinutesSecondsConverter : ConverterBase<TimeSpan>, IHasUnits
    {
        #region Implementation of IHasUnits
        public string Units => "HH:mm:ss";
        #endregion

        public override object StringToField(string text)
        {
            var split = text.Split(':');
            if (split?.Length != 3) return TimeSpan.Zero;
            int hours = 0;
            int minutes = 0;
            int seconds = 0;
            double milliseconds = 0.0;
            if (!int.TryParse(split[0], out hours))
            {
                hours = 0;
            }
            if (!int.TryParse(split[1], out minutes))
            {
                minutes = 0;
            }
            if(split[2].Contains("."))
            {
                if(double.TryParse(split[2], out milliseconds))
                {
                    seconds = (int)Math.Floor(milliseconds);
                    milliseconds = (milliseconds - seconds)*1000;
                    Debug.Assert(milliseconds >= 0.0 && milliseconds < 1000);
                }
                else
                {
                    seconds = 0;
                    milliseconds = 0.0;
                }
            }
            else
            {
                if (!int.TryParse(split[2], out seconds))
                {
                    seconds = 0;
                }
            }

            return new TimeSpan(hours, minutes, seconds, (int)milliseconds);
        }

        public override string FieldToString(object fieldValue)
        {
            if (!(fieldValue is TimeSpan)) return 0.ToString();
            var timespan = (TimeSpan)fieldValue;
            return timespan.Milliseconds == 0 ? 
                $"{timespan.Hours:00}:{timespan.Minutes:00}:{timespan.Seconds:00}" : 
                $"{timespan.Hours:00}:{timespan.Minutes:00}:{timespan.Seconds:00}.{timespan.Milliseconds:000}";
        }
    }
}