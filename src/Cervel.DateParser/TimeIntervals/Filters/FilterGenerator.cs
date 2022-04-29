using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cervel.TimeParser.TimeIntervals.Filters
{
    public class FilterGenerator : ITimeIntervalGenerator
    {
        public ITimeIntervalGenerator _generator;
        public Func<TimeInterval, bool> _filter;

        public FilterGenerator(ITimeIntervalGenerator generator, Func<TimeInterval, bool> filter)
        {
            _generator = generator;
            _filter = filter;
        }

        public IEnumerable<TimeInterval> Generate(DateTime fromDate)
        {
            return _generator.Generate(fromDate).Where(i => _filter(i));
        }
    }
}
