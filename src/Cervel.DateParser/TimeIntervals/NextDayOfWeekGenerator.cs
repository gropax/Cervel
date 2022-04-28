using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cervel.TimeParser.TimeIntervals
{
    public class NextDayOfWeekGenerator : ITimeIntervalGenerator
    {
        private DayOfWeek _dayOfWeek;
        public NextDayOfWeekGenerator(DayOfWeek dayOfWeek)
        {
            _dayOfWeek = dayOfWeek;
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
