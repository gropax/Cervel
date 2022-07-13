using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace Cervel.TimeParser.Generators.DayIntervals
{
    [DebuggerDisplay("{Name}")]
    public class CoveringDaysGenerator<T> : IGenerator<Day>
        where T : ITimeInterval<T>
    {
        public string Name { get; }
        private IGenerator<T> _generator;

        public CoveringDaysGenerator(
            IGenerator<T> generator,
            string name = null)
        {
            _generator = generator;
            Name = name ?? $"CovDays<{_generator.Name}>";
        }

        public IEnumerable<Day> Generate(DateTime fromDate)
        {
            Day last = null;
            foreach (var interval in _generator.Generate(fromDate))
            {
                var current = new Day(interval.Start);
                if (last == null || last < current)
                {
                    yield return current;
                    last = current;
                }

                while (current.TryGetNext(out var next) && next.Start < interval.End)
                {
                    yield return current;
                    current = next;
                    last = current;
                }
            }
        }
    }
}
