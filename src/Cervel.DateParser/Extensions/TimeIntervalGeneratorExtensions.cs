using Cervel.TimeParser.TimeIntervals;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cervel.TimeParser.Extensions
{
    public static class TimeIntervalGeneratorExtensions
    {
        public static ITimeGenerator<TimeInterval> Shift(this ITimeGenerator<TimeInterval> generator, TimeSpan timeSpan)
        {
            return new ShiftGenerator(generator, timeSpan);
        }
    }
}
