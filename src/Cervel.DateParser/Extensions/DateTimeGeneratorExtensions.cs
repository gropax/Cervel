using Cervel.TimeParser.DateTimes;
using Cervel.TimeParser.TimeIntervals;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cervel.TimeParser.Extensions
{
    public static class DateTimeGeneratorExtensions
    {
        public static IGenerator<DateTime> Map(
            this IGenerator<DateTime> generator,
            StrictOrderPreservingMap<DateTime> map,
            string name = null)
        {
            return new MapGenerator(generator, map.Invoke, name);
        }

        public static IGenerator<DateTime> Map(
            this IGenerator<DateTime> generator,
            OrderPreservingMap<DateTime> map,
            string name = null)
        {
            return new DeduplicateGenerator(new MapGenerator(generator, map.Invoke), name ?? $"Map<{generator.Name}>");
        }

        public static IGenerator<DateTime> StartOfDay(this IGenerator<DateTime> generator)
        {
            return generator.Map(Maps.StartOfDay());
        }

        public static IGenerator<DateTime> Take(this IGenerator<DateTime> generator, int n)
        {
            return generator.Map(Maps.Take<DateTime>(n), $"Take<{n}, {generator.Name}>");
        }

        public static IGenerator<DateTime> Skip(
            this IGenerator<DateTime> generator,
            int n,
            string name = null)
        {
            return generator.Map(Maps.Skip<DateTime>(n), name ?? $"Skip<{n}, {generator.Name}>");
        }

        public static IGenerator<DateTime> First(
            this IGenerator<DateTime> generator,
            string name = null)
        {
            return generator.Map(Maps.Take<DateTime>(1), name ?? $"First<{generator.Name}>");
        }

        public static IGenerator<TimeInterval> FirstToInfinity(this IGenerator<DateTime> generator)
        {
            return new FirstToInfinityGenerator(generator);
        }

        public static IGenerator<DateTime> ShiftDay(this IGenerator<DateTime> generator, int n)
        {
            return new DateTimes.ShiftGenerator(generator, TimeSpan.FromDays(n));
        }

        public static IGenerator<DateTime> Shift(this IGenerator<DateTime> generator, TimeSpan timeSpan)
        {
            return new DateTimes.ShiftGenerator(generator, timeSpan);
        }

        public static IGenerator<DateTime> Next(this IGenerator<DateTime> generator, DayOfWeek dow)
        {
            return generator.Daily().Where(dow).First();
        }

        public static IGenerator<DateTime> Where(this IGenerator<DateTime> generator, DayOfWeek dow)
        {
            return generator.Map(Maps.Filter<DateTime>(d => d.DayOfWeek == dow));
        }

        public static IGenerator<DateTime> Daily(this IGenerator<DateTime> generator)
        {
            return new FrequencyGenerator(new DayMeasure()).Since(generator);
        }

        public static IGenerator<DateTime> Weekly(this IGenerator<DateTime> generator)
        {
            return Scope(new FrequencyGenerator(new DayMeasure(7)), generator.FirstToInfinity());
        }

        public static IGenerator<DateTime> YearlySince(
            this IGenerator<DateTime> generator,
            string name = null)
        {
            //return new YearlyGenerator().Scope(generator.FirstToInfinity());
            return new FrequencyGenerator(new YearMeasure()).Since(generator, name ?? $"YearlySince<{generator.Name}>");
        }


        public static IGenerator<TimeInterval> AllDay(this IGenerator<DateTime> generator)
        {
            return generator.StartOfDay().ToIntervals(new DayMeasure()).Coalesce();
        }

        public static IGenerator<TimeInterval> ToScopes(
            this IGenerator<DateTime> generator, TimeSpan timeSpan)
        {
            return new ToScopesGenerator(generator, timeSpan);
        }

        public static IGenerator<TimeInterval> ToIntervals(
            this IGenerator<DateTime> generator,
            ITimeMeasure timeMeasure)
        {
            return new ToIntervalsGenerator(generator, timeMeasure);
        }


        public static IGenerator<TimeInterval> Partition(
            this IGenerator<TimeInterval> generator,
            IGenerator<DateTime> cutGenerator)
        {
            return new PartitionGenerator(generator, cutGenerator);
        }


        public static IGenerator<DateTime> Since(
            this IGenerator<DateTime> generator,
            IGenerator<DateTime> scopeGenerator,
            string name = null)
        {
            return new SinceGenerator(scopeGenerator, generator, name);
        }

        public static IGenerator<DateTime> Scope(
            this IGenerator<DateTime> generator,
            IGenerator<TimeInterval> scopeGenerator,
            string name = null)
        {
            return new ScopeGenerator(scopeGenerator, generator, name);
        }


        #region Month related methods

        public static IGenerator<DateTime> Monthly(
            this IGenerator<DateTime> g,
            string name = null)
        {
            return new FrequencyGenerator(new MonthMeasure()).Scope(g.FirstToInfinity(), name ?? $"EveryMonth");
        }

        public static IGenerator<DateTime> Yearly(
            this IGenerator<DateTime> g,
            string name = null)
        {
            return new FrequencyGenerator(new YearMeasure()).Scope(g.FirstToInfinity(), name ?? $"EveryYear");
        }

        public static IGenerator<TimeInterval> AllMonth(this IGenerator<DateTime> g) => Time.AllMonth(g);

        public static IGenerator<DateTime> Next(this IGenerator<DateTime> generator, Month month)
        {
            return generator.Monthly().Where(month).First();
        }

        public static IGenerator<DateTime> StartOfMonth(this IGenerator<DateTime> g) => Time.StartOfMonth(g);

        public static IGenerator<DateTime> ShiftMonth(this IGenerator<DateTime> g, int n) => Time.ShiftMonth(g, n);

        public static IGenerator<DateTime> Where(this IGenerator<DateTime> generator, Month month)
        {
            return generator.Map(Maps.Filter<DateTime>(d => d.Month == (int)month));
        }

        public static IGenerator<TimeInterval> DuringMonthes(this IGenerator<DateTime> g, int n) => Time.DuringMonthes(g, n);

        #endregion
    }
}
