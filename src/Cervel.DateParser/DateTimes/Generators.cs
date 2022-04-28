using Cervel.TimeParser.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cervel.TimeParser.DateTimes
{
    public static class Generators
    {
        public static IDateTimeGenerator NextWeekDays(IEnumerable<DayOfWeek> dows)
        {
            var hashset = new HashSet<DayOfWeek>(dows);
            return new DayFilterGenerator((d) => hashset.Contains(d.DayOfWeek), take: 1)
                .Shift(TimeSpan.FromDays(1));
        }
    }
}
