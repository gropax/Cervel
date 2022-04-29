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
        public static ITimeGenerator<DateTime> Map(
            this ITimeGenerator<DateTime> generator,
            Func<IEnumerable<DateTime>, IEnumerable<DateTime>> modifier)
        {
            return new DateTimeMapGenerator(generator, modifier);
        }

        public static ITimeGenerator<DateTime> Date(this ITimeGenerator<DateTime> generator)
        {
            return generator.Map(dx => dx.Select(d => d.Date));
        }

        public static ITimeGenerator<DateTime> Take(this ITimeGenerator<DateTime> generator, int n)
        {
            return generator.Map(dx => dx.Take(n));
        }

        public static ITimeGenerator<DateTime> Skip(this ITimeGenerator<DateTime> generator, int n)
        {
            return generator.Map(dx => dx.Skip(n));
        }

        public static ITimeGenerator<DateTime> First(this ITimeGenerator<DateTime> generator)
        {
            return generator.Map(dx => dx.Take(1));
        }

        public static ITimeGenerator<TimeInterval> FirstToInfinity(this ITimeGenerator<DateTime> generator)
        {
            return new FirstToInfinityGenerator(generator);
        }

        /// <summary>
        /// Attention ! Du fait de l'irrégularité des mois, un shift de mois peut générer des doublons
        /// (e.g. le 30 et 31 janviers seront rapportés au 28 février). Il faut donc nettoyer le flux.
        /// </summary>
        public static ITimeGenerator<DateTime> ShiftMonth(this ITimeGenerator<DateTime> generator, int n)
        {
            throw new NotImplementedException();
        }

        public static ITimeGenerator<DateTime> ShiftDay(this ITimeGenerator<DateTime> generator, int n)
        {
            return new DateTimes.ShiftGenerator(generator, TimeSpan.FromDays(n));
        }

        public static ITimeGenerator<DateTime> Shift(this ITimeGenerator<DateTime> generator, TimeSpan timeSpan)
        {
            return new DateTimes.ShiftGenerator(generator, timeSpan);
        }

        public static ITimeGenerator<DateTime> Next(this ITimeGenerator<DateTime> generator, DayOfWeek dow)
        {
            return generator.Daily().Where(dow).First();
        }

        public static ITimeGenerator<DateTime> Where(this ITimeGenerator<DateTime> generator, DayOfWeek dow)
        {
            return generator.Map(dx => dx.Where(d => d.DayOfWeek == dow));
        }

        public static ITimeGenerator<DateTime> Daily(this ITimeGenerator<DateTime> generator)
        {
            return Scope(new DailyGenerator(), generator.FirstToInfinity());
        }

        public static ITimeGenerator<TimeInterval> AllDay(this ITimeGenerator<DateTime> generator)
        {
            return generator.Date().ToIntervals(TimeSpan.FromDays(1));
        }

        public static ITimeGenerator<TimeInterval> ToIntervals(
            this ITimeGenerator<DateTime> generator, TimeSpan timeSpan)
        {
            return new ToIntervalsGenerator(generator, timeSpan);
        }


        public static ITimeGenerator<TimeInterval> Partition(this ITimeGenerator<DateTime> generator)
        {
            return new PartitionGenerator(generator);
        }


        public static ITimeGenerator<DateTime> Scope(
            this ITimeGenerator<DateTime> generator,
            ITimeGenerator<TimeInterval> scopeGenerator)
        {
            return new ScopeGenerator(scopeGenerator, generator);
        }
    }
}
