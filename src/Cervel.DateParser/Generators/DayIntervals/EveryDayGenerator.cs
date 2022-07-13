using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace Cervel.TimeParser.TimeIntervals
{
    [DebuggerDisplay("{Name}")]
    public class EveryDayGenerator : IGenerator<Day>
    {
        public string Name { get; }

        public EveryDayGenerator(
            string name = null)
        {
            Name = name ?? $"EveryDay";
        }

        public IEnumerable<Day> Generate(DateTime fromDate)
        {
            var day = new Day(fromDate);
            yield return day;

            while (true)
            {
                day = day.Increment(1);
                yield return day;
            }
        }
    }
}
