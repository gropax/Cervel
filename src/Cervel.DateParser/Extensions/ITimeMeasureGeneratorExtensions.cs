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
    public static class ITimeMeasureGeneratorExtensions
    {
        public static IGenerator<T> Increment<T>(
            this IGenerator<T> generator,
            int shift)
            where T : ITimeUnit<T>
        {
            return new IncrementGenerator<T>(generator, shift);
        }
    }
}
