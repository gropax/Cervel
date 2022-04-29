using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cervel.TimeParser.TimeIntervals
{
    public class DayFilterGenerator : TimeIntervalGenerator
    {
        private TimeSpan _timeSpan;
        private DateTimes.DayFilterGenerator _dateGenerator;

        public DayFilterGenerator(
            TimeSpan timeSpan,
            Func<DateTime, bool> dateTimeSelector = null,
            Func<int, bool> indexSelector = null,
            int? skip = null,
            int? take = null)
        {
            _timeSpan = timeSpan;
            _dateGenerator = new DateTimes.DayFilterGenerator(dateTimeSelector, indexSelector, skip, take);
        }

        public override IEnumerable<TimeInterval> Generate(DateTime fromDate)
        {
            return _dateGenerator
                .Generate(fromDate)
                .Select(d => d.ToInterval(_timeSpan))
                .Disjunction();
        }
    }
}
