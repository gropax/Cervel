using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cervel.TimeParser.TimeIntervals
{
    public class ToIntervalsGenerator : TimeIntervalGenerator
    {
        private ITimeGenerator<DateTime> _generator;
        private TimeSpan _timeSpan;

        public ToIntervalsGenerator(ITimeGenerator<DateTime> generator, TimeSpan timeSpan)
        {
            _generator = generator;
            _timeSpan = timeSpan;
        }

        public override IEnumerable<TimeInterval> Generate(DateTime fromDate)
        {
            return _generator
                .Generate(fromDate)
                .Select(d => d.ToInterval(_timeSpan))
                .Disjunction();  // Necessary as generated intervals may overlapse
        }
    }
}
