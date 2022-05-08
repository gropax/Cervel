using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cervel.TimeParser.Extensions;

namespace Cervel.TimeParser.TimeIntervals
{
    public class ToIntervalsGenerator : TimeIntervalGenerator
    {
        private IGenerator<DateTime> _generator;
        private IGenerator<DateTime> _frequency;

        public ToIntervalsGenerator(
            IGenerator<DateTime> generator,
            IGenerator<DateTime> frequency,
            string name = null)
            : base(name ?? $"ToInterval<{frequency.Name}, {generator.Name}>")
        {
            _generator = generator;
            _frequency = frequency;
        }

        public override IEnumerable<TimeInterval> Generate(DateTime fromDate)
        {
            return _generator
                .Generate(fromDate)
                .Select(d => d.ToInterval(GetTimeSpan(d)))
                .Disjunction();  // Necessary as generated intervals may overlapse
        }

        private TimeSpan GetTimeSpan(DateTime start)
        {
            var end = _frequency.Generate(start).First();
            return end - start;
        }
    }
}
