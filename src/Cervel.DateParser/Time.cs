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


        private static DateTime? _now;
        public static void SetNow(DateTime date) => _now = date;
        public static DateTime GetNow() => _now ?? DateTime.Now;

        public static IGenerator<DateTime> Now() => new OnceGenerator(GetNow());

        public static IGenerator<DateTime> Yesterday() => Today().ShiftDay(-1);
        public static IGenerator<DateTime> Today() => Now().Date();
        public static IGenerator<DateTime> Tomorrow() => Today().ShiftDay(1);

        public static IGenerator<DateTime> EveryDay() => Start().Daily();

        public static IGenerator<DateTime> Next(DayOfWeek dow) => Tomorrow().Next(dow);
        public static IGenerator<DateTime> Each(DayOfWeek dow) => Start().Next(dow).Weekly();
        public static IGenerator<DateTime> Each(Month month) => Start().Next(month).Yearly();
        public static IGenerator<DateTime> Union(params IGenerator<DateTime>[] generators) => new UnionGenerator(generators);
        public static IGenerator<DateTime> Since(IGenerator<DateTime> scope, IGenerator<DateTime> generator) => new SinceGenerator(scope, generator);
        public static IGenerator<DateTime> Until(IGenerator<DateTime> scope, IGenerator<DateTime> generator) => new UntilGenerator(scope, generator);

        public static IGenerator<TimeInterval> DayScopes() => new DailyGenerator().ToScopes(TimeSpan.FromDays(1));

        public static IGenerator<TimeInterval> EveryDayOfWeekInterval(DayOfWeek dow)
        {
            return new TimeIntervals.DayFilterGenerator(TimeSpan.FromDays(1), (d) => d.DayOfWeek == dow);
        }

        public static Func<IGenerator<DateTime>, IGenerator<DateTime>> DayShift(int n) => (g) => g.ShiftDay(n);
    }
}
