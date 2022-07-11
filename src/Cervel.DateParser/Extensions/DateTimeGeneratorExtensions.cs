using Cervel.TimeParser.Dates;
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
        public static IGenerator<Date> Map(
            this IGenerator<Date> generator,
            StrictOrderPreservingMap<Date> map,
            string name = null)
        {
            return new MapGenerator(generator, map.Invoke, name);
        }

        public static IGenerator<Date> Map(
            this IGenerator<Date> generator,
            OrderPreservingMap<Date> map,
            string name = null)
        {
            return new DeduplicateGenerator(new MapGenerator(generator, map.Invoke), name ?? $"Map<{generator.Name}>");
        }

        public static IGenerator<Date> StartOfDay(this IGenerator<Date> generator)
        {
            return generator.Map(Maps.StartOfDay());
        }

        public static IGenerator<Date> Take(
            this IGenerator<Date> generator,
            int n,
            string name = null)
        {
            return generator.Map(Maps.Take<Date>(n), name ?? $"Take<{n}, {generator.Name}>");
        }

        public static IGenerator<Date> Skip(
            this IGenerator<Date> generator,
            int n,
            string name = null)
        {
            return generator.Map(Maps.Skip<Date>(n), name ?? $"Skip<{n}, {generator.Name}>");
        }

        public static IGenerator<Date> NEveryM(
            this IGenerator<Date> generator,
            int n,
            int m,
            string name = null)
        {
            return generator.Map(Maps.NEveryM<Date>(n, m), name ?? $"NEveryM<{n}, {m}, {generator.Name}>");
        }

        public static IGenerator<Date> First(
            this IGenerator<Date> generator,
            string name = null)
        {
            return generator.Map(Maps.Take<Date>(1), name ?? $"First<{generator.Name}>");
        }

        public static IGenerator<TimeInterval> FirstToInfinity(this IGenerator<Date> generator)
        {
            return new FirstToInfinityGenerator(generator);
        }

        public static IGenerator<Date> ShiftDay(this IGenerator<Date> generator, int n)
        {
            return new Dates.ShiftGenerator(generator, TimeSpan.FromDays(n));
        }

        public static IGenerator<Date> Shift(this IGenerator<Date> generator, TimeSpan timeSpan)
        {
            return new Dates.ShiftGenerator(generator, timeSpan);
        }

        public static IGenerator<Date> Next(this IGenerator<Date> generator, DayOfWeek dow)
        {
            return generator.Daily().Where(dow).First();
        }

        public static IGenerator<Date> Where(this IGenerator<Date> generator, DayOfWeek dow)
        {
            return generator.Map(Maps.Filter<Date>(d => d.DateTime.DayOfWeek == dow));  // @refactor
        }

        public static IGenerator<TimeInterval> ToPartition(this IGenerator<Date> g) => new ToPartitionGenerator(g);

        public static IGenerator<Date> Daily(this IGenerator<Date> generator)
        {
            return new FrequencyGenerator(new DayMeasure()).Since(generator);
        }

        public static IGenerator<Date> Weekly(this IGenerator<Date> generator)
        {
            return Scope(new FrequencyGenerator(new DayMeasure(7)), generator.FirstToInfinity());
        }

        public static IGenerator<Date> YearlySince(
            this IGenerator<Date> generator,
            string name = null)
        {
            //return new YearlyGenerator().Scope(generator.FirstToInfinity());
            return new FrequencyGenerator(new YearMeasure()).Since(generator, name ?? $"YearlySince<{generator.Name}>");
        }


        public static IGenerator<TimeInterval> AllDay(this IGenerator<Date> generator)
        {
            return generator.StartOfDay().ToIntervals(new DayMeasure()).Coalesce();
        }

        public static IGenerator<TimeInterval> ToScopes(
            this IGenerator<Date> generator, TimeSpan timeSpan)
        {
            return new ToScopesGenerator(generator, timeSpan);
        }

        public static IGenerator<TimeInterval> ToIntervals(
            this IGenerator<Date> generator,
            ITimeMeasure timeMeasure)
        {
            return new ToIntervalsGenerator(generator, timeMeasure);
        }


        public static IGenerator<TimeInterval> Partition(
            this IGenerator<TimeInterval> generator,
            IGenerator<Date> cutGenerator)
        {
            return new PartitionGenerator(generator, cutGenerator);
        }


        public static IGenerator<Date> Since(
            this IGenerator<Date> generator,
            IGenerator<Date> scopeGenerator,
            string name = null)
        {
            return new SinceGenerator(scopeGenerator, generator, name);
        }

        public static IGenerator<Date> Scope(
            this IGenerator<Date> generator,
            IGenerator<TimeInterval> scopeGenerator,
            string name = null)
        {
            return new ScopeGenerator(scopeGenerator, generator, name);
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

        public static IGenerator<Date> Next(this IGenerator<Date> generator, Month month)
        {
            return generator.Monthly().Where(month).First();
        }

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
