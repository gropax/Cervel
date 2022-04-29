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
        public static ITimeGenerator<DateTime> Date(this ITimeGenerator<DateTime> generator)
        {
            return new DateTimeStreamModifier(generator, dx => dx.Select(d => d.Date));
        }

        public static ITimeGenerator<DateTime> Take(this ITimeGenerator<DateTime> generator, int n)
        {
            return new DateTimeStreamModifier(generator, dx => dx.Take(n));
        }

        public static ITimeGenerator<DateTime> Skip(this ITimeGenerator<DateTime> generator, int n)
        {
            return new DateTimeStreamModifier(generator, dx => dx.Skip(n));
        }

        public static ITimeGenerator<DateTime> First(this ITimeGenerator<DateTime> generator)
        {
            return new DateTimeStreamModifier(generator, dx => dx.Take(1));
        }

        public static ITimeGenerator<DateTime> During(
            this ITimeGenerator<DateTime> generator,
            ITimeGenerator<DateTime> frequencyGenerator)
        {
            throw new NotImplementedException();
            //return frequencyGenerator.Partition()
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
            ITimeGenerator<TimeInterval> intervalGenerator)
        {
            return new ScopeGenerator(generator, intervalGenerator);
        }
    }
}
