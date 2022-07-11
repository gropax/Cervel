using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace Cervel.TimeParser.TimeIntervals
{
    [DebuggerDisplay("{Name}")]
    public class NextDayOfWeekGenerator : IGenerator<TimeInterval>
    {
        public string Name { get; }
        private DayOfWeek _dayOfWeek;
        public NextDayOfWeekGenerator(
            DayOfWeek dayOfWeek,
            string name = null)
        {
            _dayOfWeek = dayOfWeek;
            Name = name ?? $"NextDOW<{dayOfWeek}>";
        }

        public IEnumerable<TimeInterval> Generate(DateTime fromDate)
        {
            //var currentDow = fromDate.DayOfWeek;
            //int refDow = _dayOfWeek > currentDow ? (int)_dayOfWeek : (int)_dayOfWeek + 7;
            //int shift = _dayOfWeek > currentDow ? _dayOfWeek - currentDow : 

            //if (_dayOfWeek < currentDow)
            throw new NotImplementedException();
        }
    }
}
