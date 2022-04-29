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
        public static ITimeGenerator<DateTime> Shift(this ITimeGenerator<DateTime> generator, TimeSpan timeSpan)
        {
            return new DateTimes.ShiftGenerator(generator, timeSpan);
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
