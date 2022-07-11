using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace Cervel.TimeParser.TimeIntervals.Filters
{
    [DebuggerDisplay("{Name}")]
    public class FilterGenerator : IGenerator<TimeInterval>
    {
        public string Name { get; }
        public IGenerator<TimeInterval> _generator;
        public Func<TimeInterval, bool> _filter;

        public FilterGenerator(
            IGenerator<TimeInterval> generator,
            Func<TimeInterval, bool> filter,
            string name = null)
        {
            _generator = generator;
            _filter = filter;
            Name = name ?? $"Filter<{generator.Name}>";
        }

        public IEnumerable<TimeInterval> Generate(DateTime fromDate)
        {
            return _generator.Generate(fromDate).Where(i => _filter(i));
        }
    }
}
