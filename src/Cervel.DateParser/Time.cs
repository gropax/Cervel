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
        public static ITimeGenerator<DateTime> Today() => Now().Date();
        public static ITimeGenerator<DateTime> Yesterday() => Today().ShiftDay(-1);
        public static ITimeGenerator<DateTime> Tomorrow() => Today().ShiftDay(1);

        public static ITimeGenerator<DateTime> NextDayOfWeek(DayOfWeek dow) => Tomorrow().Next(dow);

        public static ITimeGenerator<DateTime> NextWeekDays(IEnumerable<DayOfWeek> dows)
        {
            var hashset = new HashSet<DayOfWeek>(dows);
            return new DayFilterGenerator((d) => hashset.Contains(d.DayOfWeek), take: 1)
                .Shift(TimeSpan.FromDays(1));
        }

        public static ITimeGenerator<DateTime> EveryDayOfWeek(DayOfWeek dow)
        {
            return new DayFilterGenerator((d) => d.DayOfWeek == dow);
        }


        public static ITimeGenerator<DateTime> Once(DateTime d) => new OnceGenerator(d);

        public static ITimeGenerator<DateTime> Yearly(int n) => new YearlyGenerator(n);
        public static ITimeGenerator<DateTime> Monthly(int n) => new MonthlyGenerator(n);
        public static ITimeGenerator<DateTime> Weekly(int n) => new DailyGenerator(TimeSpan.FromDays(n * 7));
        public static ITimeGenerator<DateTime> Daily(int n) => new DailyGenerator(TimeSpan.FromDays(n));
        public static ITimeGenerator<DateTime> Hourly(int n) => new DailyGenerator(TimeSpan.FromHours(n));


        public static ITimeGenerator<TimeInterval> TodayInterval()
        {
            return new TimeIntervals.DayFilterGenerator(TimeSpan.FromDays(1), take: 1);
        }

        public static ITimeGenerator<TimeInterval> NextWeekDaysInterval(IEnumerable<DayOfWeek> dows)
        {
            var hashset = new HashSet<DayOfWeek>(dows);
            return new TimeIntervals.DayFilterGenerator(TimeSpan.FromDays(1), (d) => hashset.Contains(d.DayOfWeek), take: 1)
                .Shift(TimeSpan.FromDays(1));
        }

        public static ITimeGenerator<TimeInterval> EveryDayOfWeekInterval(DayOfWeek dow)
        {
            return new TimeIntervals.DayFilterGenerator(TimeSpan.FromDays(1), (d) => d.DayOfWeek == dow);
        }
    }
}
