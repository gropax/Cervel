using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cervel.TimeParser
{
    public static class DateTimeExtensions
    {
        public static TimeInterval ToInterval(this DateTime dateTime, TimeSpan timeSpan)
        {
            if (timeSpan.Ticks < 0)
                return new TimeInterval(dateTime + timeSpan, dateTime);
            else
                return new TimeInterval(dateTime, dateTime + timeSpan);
        }
    }
}
