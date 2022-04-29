using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cervel.TimeParser.DateTimes
{
    public class ScopeGenerator : DateTimeGenerator
    {
        private ITimeGenerator<DateTime> _dateGenerator;
        private ITimeGenerator<TimeInterval> _intervalGenerator;

        public ScopeGenerator(ITimeGenerator<DateTime> dateGenerator, ITimeGenerator<TimeInterval> intervalGenerator)
        {
            _dateGenerator = dateGenerator;
            _intervalGenerator = intervalGenerator;
        }

        public override IEnumerable<DateTime> Generate(DateTime fromDate)
        {
            foreach (var interval in _intervalGenerator.Generate(fromDate))
                foreach (var date in _dateGenerator.Generate(interval))
                    yield return date;
        }
    }
}
