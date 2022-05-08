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
        private ITimeMeasure _timeMeasure;

        public ToIntervalsGenerator(
            IGenerator<DateTime> generator,
            ITimeMeasure timeMeasure,
            string name = null)
            : base(name ?? $"ToInterval<{timeMeasure.Name}, {generator.Name}>")
        {
            _generator = generator;
            _timeMeasure = timeMeasure;
        }

        public override IEnumerable<TimeInterval> Generate(DateTime fromDate)
        {
            foreach (var date in _generator.Generate(fromDate))
                if (TryGetTimeInterval(date, out var interval))
                    yield return interval;
        }

        public bool TryGetTimeInterval(DateTime date, out TimeInterval interval)
        {
            interval = default;
            var translated = _timeMeasure.AddTo(date);

            var cmp = date.CompareTo(translated);
            if (cmp == 0)
                return false;

            if (cmp == -1)
                interval = new TimeInterval(date, translated);
            else if (cmp == 1)
                interval = new TimeInterval(translated, date);

            return true;
        }
    }
}
