using Cervel.TimeParser.Dates;
using Cervel.TimeParser.Generators.DayIntervals;
using Cervel.TimeParser.TimeIntervals;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cervel.TimeParser.Extensions
{
    public static class ITimeIntervalGeneratorExtensions
    {
        public static IGenerator<DayInterval> CoveringDays<T>(
            this IGenerator<T> generator)
            where T : ITimeInterval<T>
        {
            return new CoveringDaysGenerator<T>(generator);
        }

        public static IGenerator<TimeInterval> Coalesce<T>(
            this IGenerator<T> generator)
            where T : ITimeInterval<T>
        {
            return new CoalesceGenerator<T>(generator);
        }
    }
}
