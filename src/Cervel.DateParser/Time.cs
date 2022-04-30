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
        public static DateTime Min(DateTime dateTime, DateTime other)
        {
            return dateTime <= other ? dateTime : other;
        }

        public static DateTime Max(DateTime dateTime, DateTime other)
        {
            return dateTime >= other ? dateTime : other;
        }

        public static IGenerator<DateTime> Start() => new OnceGenerator();
        public static IGenerator<DateTime> Now() => new OnceGenerator(DateTime.Now);

        public static IGenerator<DateTime> Yesterday() => Today().ShiftDay(-1);
        public static IGenerator<DateTime> Today() => Now().Date();
        public static IGenerator<DateTime> Tomorrow() => Today().ShiftDay(1);

        public static IGenerator<DateTime> Next(DayOfWeek dow) => Tomorrow().Next(dow);
        public static IGenerator<DateTime> Each(DayOfWeek dow) => Start().Next(dow).Weekly();

        public static IGenerator<TimeInterval> DayScopes() => new DailyGenerator().ToScopes(TimeSpan.FromDays(1));

        public static IGenerator<TimeInterval> EveryDayOfWeekInterval(DayOfWeek dow)
        {
            return new TimeIntervals.DayFilterGenerator(TimeSpan.FromDays(1), (d) => d.DayOfWeek == dow);
        }

        public static Func<IGenerator<DateTime>, IGenerator<DateTime>> DayShift(int n) => (g) => g.ShiftDay(n);
    }
}
