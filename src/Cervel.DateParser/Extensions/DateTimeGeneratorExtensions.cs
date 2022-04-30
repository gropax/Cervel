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
            Func<IEnumerable<DateTime>, IEnumerable<DateTime>> modifier)
        {
            return new DateTimeMapGenerator(generator, modifier);
        }

        public static IGenerator<DateTime> Date(this IGenerator<DateTime> generator)
        {
            return generator.Map(dx => dx.Select(d => d.Date));
        }

        public static IGenerator<DateTime> Take(this IGenerator<DateTime> generator, int n)
        {
            return generator.Map(dx => dx.Take(n));
        }

        public static IGenerator<DateTime> Skip(this IGenerator<DateTime> generator, int n)
        {
            return generator.Map(dx => dx.Skip(n));
        }

        public static IGenerator<DateTime> First(this IGenerator<DateTime> generator)
        {
            return generator.Map(dx => dx.Take(1));
        }

        public static IGenerator<TimeInterval> FirstToInfinity(this IGenerator<DateTime> generator)
        {
            return new FirstToInfinityGenerator(generator);
        }

        /// <summary>
        /// Attention ! Du fait de l'irrégularité des mois, un shift de mois peut générer des doublons
        /// (e.g. le 30 et 31 janviers seront rapportés au 28 février). Il faut donc nettoyer le flux.
        /// </summary>
        public static IGenerator<DateTime> ShiftMonth(this IGenerator<DateTime> generator, int n)
        {
            throw new NotImplementedException();
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
            return generator.Map(dx => dx.Where(d => d.DayOfWeek == dow));
        }

        public static IGenerator<DateTime> Daily(this IGenerator<DateTime> generator)
        {
            return Scope(new DailyGenerator(), generator.FirstToInfinity());
        }

        public static IGenerator<DateTime> Weekly(this IGenerator<DateTime> generator)
        {
            return Scope(new DailyGenerator(TimeSpan.FromDays(7)), generator.FirstToInfinity());
        }

        public static IGenerator<TimeInterval> AllDay(this IGenerator<DateTime> generator)
        {
            return generator.Date().ToIntervals(TimeSpan.FromDays(1));
        }

        public static IGenerator<TimeInterval> ToIntervals(
            this IGenerator<DateTime> generator, TimeSpan timeSpan)
        {
            return new ToIntervalsGenerator(generator, timeSpan);
        }


        public static IGenerator<TimeInterval> Partition(
            this IGenerator<TimeInterval> generator,
            IGenerator<DateTime> cutGenerator)
        {
            return new PartitionGenerator(generator, cutGenerator);
        }


        public static IGenerator<DateTime> Scope(
            this IGenerator<DateTime> generator,
            IGenerator<TimeInterval> scopeGenerator)
        {
            return new ScopeGenerator(scopeGenerator, generator);
        }
    }
}
