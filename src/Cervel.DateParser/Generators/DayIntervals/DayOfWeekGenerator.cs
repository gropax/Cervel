using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace Cervel.TimeParser.TimeIntervals
{
    [DebuggerDisplay("{Name}")]
    public class DayOfWeekGenerator : IGenerator<Day>
    {
        private DayOfWeek _dayOfWeek;
        public string Name { get; }

        public DayOfWeekGenerator(
            DayOfWeek dayOfWeek,
            string name = null)
        {
            _dayOfWeek = dayOfWeek;
            Name = name ?? $"DayOfWeek<{dayOfWeek}>";
        }

        public IEnumerable<Day> Generate(DateTime fromDate)
        {
            var currentDow = fromDate.DayOfWeek;
            int nextTargetDow = _dayOfWeek >= currentDow ? (int)_dayOfWeek : (int)_dayOfWeek + 7;
            int inc = nextTargetDow - (int)currentDow;

            var day = new Day(fromDate).Increment(inc);
            yield return day;

            while (true)
            {
                day = day.Increment(7);
                yield return day;
            }
        }
    }
}
