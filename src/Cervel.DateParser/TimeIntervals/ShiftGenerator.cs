using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cervel.TimeParser.TimeIntervals
{
    public class ShiftGenerator : ITimeIntervalGenerator
    {
        private ITimeIntervalGenerator _generator;
        private TimeSpan _timeSpan;

        public ShiftGenerator(ITimeIntervalGenerator generator, TimeSpan timeSpan)
        {
            _generator = generator;
            _timeSpan = timeSpan;
        }

        public IEnumerable<TimeInterval> Generate(DateTime fromDate)
        {
            return _generator.Generate(fromDate + _timeSpan);
        }
    }
}
