using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using Cervel.TimeParser.Dates;
using Cervel.TimeParser.Extensions;

namespace Cervel.TimeParser.TimeIntervals
{
    [DebuggerDisplay("{Name}")]
    public class ToIntervalsGenerator : IGenerator<TimeInterval>
    {
        public string Name { get; }
        private IGenerator<Date> _generator;
        private ITimeMeasure _timeMeasure;

        public ToIntervalsGenerator(
            IGenerator<Date> generator,
            ITimeMeasure timeMeasure,
            string name = null)
        {
            _generator = generator;
            _timeMeasure = timeMeasure;
            Name = name ?? $"ToInterval<{timeMeasure.Name}, {generator.Name}>";
        }

        public IEnumerable<TimeInterval> Generate(DateTime fromDate)
        {
            foreach (var date in _generator.Generate(fromDate))
                if (TryGetTimeInterval(date, out var interval))
                    yield return interval;
        }

        public bool TryGetTimeInterval(Date date, out TimeInterval interval)
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
