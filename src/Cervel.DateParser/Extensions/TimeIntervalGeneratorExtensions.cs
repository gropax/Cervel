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
        public static IGenerator<TimeInterval> Shift(this IGenerator<TimeInterval> generator, TimeSpan timeSpan)
        {
            return new ShiftGenerator(generator, timeSpan);
        }
    }
}
