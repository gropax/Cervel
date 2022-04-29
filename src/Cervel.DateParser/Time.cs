using Cervel.TimeParser.DateTimes;
using Cervel.TimeParser.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cervel.TimeParser
{
    public static class Time
    {
        public static ITimeGenerator<DateTime> Now() => new OnceGenerator();

        public static ITimeGenerator<DateTime> Yesterday() => Today().ShiftDay(-1);
        public static ITimeGenerator<DateTime> Today() => Now().Date();
        public static ITimeGenerator<DateTime> Tomorrow() => Today().ShiftDay(1);

        public static ITimeGenerator<DateTime> Next(DayOfWeek dow) => Tomorrow().Next(dow);
        public static ITimeGenerator<DateTime> Each(DayOfWeek dow) => Today().Next(dow).Weekly();

        public static ITimeGenerator<TimeInterval> EveryDayOfWeekInterval(DayOfWeek dow)
        {
            return new TimeIntervals.DayFilterGenerator(TimeSpan.FromDays(1), (d) => d.DayOfWeek == dow);
        }
    }
}
