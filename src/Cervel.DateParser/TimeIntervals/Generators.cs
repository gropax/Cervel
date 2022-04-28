using Cervel.TimeParser.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cervel.TimeParser.TimeIntervals
{
    public static class Generators
    {
        public static ITimeIntervalGenerator Today()
        {
            return new DayFilterGenerator(TimeSpan.FromDays(1), take: 1);
        }

        public static ITimeIntervalGenerator NextWeekDays(IEnumerable<DayOfWeek> dows)
        {
            var hashset = new HashSet<DayOfWeek>(dows);
            return new DayFilterGenerator(TimeSpan.FromDays(1), (d) => hashset.Contains(d.DayOfWeek), take: 1)
                .Shift(TimeSpan.FromDays(1));
        }

        public static ITimeIntervalGenerator EveryDayOfWeek(DayOfWeek dow)
        {
            return new DayFilterGenerator(TimeSpan.FromDays(1), (d) => d.DayOfWeek == dow);
        }
    }
}
