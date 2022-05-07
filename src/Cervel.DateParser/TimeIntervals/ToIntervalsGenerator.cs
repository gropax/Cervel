using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cervel.TimeParser.TimeIntervals
{
    public class ToIntervalsGenerator : TimeIntervalGenerator
    {
        private IGenerator<DateTime> _generator;
        private TimeSpan _timeSpan;

        public ToIntervalsGenerator(
            IGenerator<DateTime> generator,
            TimeSpan timeSpan,
            string name = null)
            : base(name ?? $"ToInterval<{timeSpan}, {generator.Name}>")
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
