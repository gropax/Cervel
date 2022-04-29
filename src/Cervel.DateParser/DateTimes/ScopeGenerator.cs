using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cervel.TimeParser.DateTimes
{
    public class ScopeGenerator : DateTimeGenerator
    {
        private ITimeGenerator<TimeInterval> _scopeGenerator;
        private ITimeGenerator<DateTime> _dateGenerator;

        public ScopeGenerator(ITimeGenerator<TimeInterval> scopeGenerator, ITimeGenerator<DateTime> dateGenerator)
        {
            _scopeGenerator = scopeGenerator;
            _dateGenerator = dateGenerator;
        }

        public override IEnumerable<DateTime> Generate(DateTime fromDate)
        {
            foreach (var interval in _scopeGenerator.Generate(fromDate))
                foreach (var date in _dateGenerator.Generate(interval))
                    yield return date;
        }
    }
}
