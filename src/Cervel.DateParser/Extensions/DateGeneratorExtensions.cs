using Cervel.TimeParser.Dates;
using Cervel.TimeParser.TimeIntervals;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cervel.TimeParser.Extensions
{
    public static class DateGeneratorExtensions
    {
        public static IGenerator<TimeInterval> FirstToInfinity(this IGenerator<Date> generator)
        {
            return new FirstToInfinityGenerator(generator);
        }

        public static IGenerator<T> ShiftDay<T>(
            this IGenerator<T> generator,
            int n)
            where T : ITimeInterval<T>
        {
            return new ShiftGenerator<T>(generator, TimeSpan.FromDays(n));
        }

        public static IGenerator<T> Shift<T>(
            this IGenerator<T> generator,
            TimeSpan timeSpan)
            where T : ITimeInterval<T>
        {
            return new ShiftGenerator<T>(generator, timeSpan);
        }

        public static IGenerator<Date> Where(this IGenerator<Date> generator, DayOfWeek dow)
        {
            return generator.Map(Maps.Filter<Date>(d => d.DateTime.DayOfWeek == dow));  // @refactor
        }

        public static IGenerator<TimeInterval> ToPartition(this IGenerator<Date> g) => new ToPartitionGenerator(g);

        public static IGenerator<TimeInterval> ToScopes(
            this IGenerator<Date> generator,
            TimeSpan timeSpan)
        {
            return new ToScopesGenerator(generator, timeSpan);
        }

        public static IGenerator<TimeInterval> ToIntervals(
            this IGenerator<Date> generator,
            ITimeMeasure timeMeasure)
        {
            return new ToIntervalsGenerator(generator, timeMeasure);
        }


        public static IGenerator<T> Scope<T, TScope>(
            this IGenerator<T> generator,
            IGenerator<TScope> scopeGenerator,
            string name = null)
            where T : ITimeInterval<T>
            where TScope : ITimeInterval<TScope>
        {
            return new ScopeGenerator<T, TScope>(scopeGenerator, generator, name);
        }


        #region Month related methods

        public static IGenerator<Date> Monthly(
            this IGenerator<Date> g,
            string name = null)
        {
            return new FrequencyGenerator(new MonthMeasure()).Scope(g.FirstToInfinity(), name ?? $"EveryMonth");
        }

        public static IGenerator<Date> Yearly(
            this IGenerator<Date> g,
            string name = null)
        {
            return new FrequencyGenerator(new YearMeasure()).Scope(g.FirstToInfinity(), name ?? $"EveryYear");
        }

        public static IGenerator<TimeInterval> AllMonth(this IGenerator<Date> g) => Time.AllMonth(g);

        public static IGenerator<Date> StartOfMonth(this IGenerator<Date> g) => Time.StartOfMonth(g);

        public static IGenerator<Date> ShiftMonth(this IGenerator<Date> g, int n) => Time.ShiftMonth(g, n);

        public static IGenerator<Date> Where(this IGenerator<Date> generator, Month month)
        {
            return generator.Map(Maps.Filter<Date>(d => d.DateTime.Month == (int)month));  // @refactor
        }

        public static IGenerator<TimeInterval> DuringMonthes(this IGenerator<Date> g, int n) => Time.DuringMonthes(g, n);

        #endregion
    }
}
