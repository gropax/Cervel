using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cervel.TimeParser.DateTimes
{
    public class ScopeGenerator : IDateTimeGenerator
    {
        private IDateTimeGenerator _dateGenerator;
        private ITimeGenerator<TimeInterval> _intervalGenerator;

        public ScopeGenerator(IDateTimeGenerator dateGenerator, ITimeGenerator<TimeInterval> intervalGenerator)
        {
            _dateGenerator = dateGenerator;
            _intervalGenerator = intervalGenerator;
        }

        public IEnumerable<DateTime> Generate(DateTime fromDate)
        {
            foreach (var interval in _intervalGenerator.Generate(fromDate))
                foreach (var date in _dateGenerator.Generate(interval))
                    yield return date;
        }
    }
}
