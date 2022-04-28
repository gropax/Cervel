using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cervel.TimeParser
{
    public static class DateTimeExtensions
    {
        public static DateTime Shift(this DateTime dateTime, TimeSpan timeSpan)
        {
            if (timeSpan.Ticks < 0 && DateTime.MinValue - timeSpan >= dateTime)
                return DateTime.MinValue;
            else if (timeSpan.Ticks > 0 && DateTime.MaxValue - timeSpan <= dateTime)
                return DateTime.MaxValue;
            else
                return dateTime + timeSpan;
        }

        public static TimeInterval ToInterval(this DateTime dateTime, TimeSpan timeSpan)
        {
            if (timeSpan.Ticks < 0)
                return new TimeInterval(dateTime.Shift(timeSpan), dateTime);
            else
                return new TimeInterval(dateTime, dateTime.Shift(timeSpan));
        }
    }
}
