using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cervel.TimeParser.DateTimes
{
    public class ScopeGenerator : DateTimeGenerator
    {
        private IGenerator<TimeInterval> _scopeGenerator;
        private IGenerator<DateTime> _dateGenerator;

        public ScopeGenerator(
            IGenerator<TimeInterval> scopeGenerator,
            IGenerator<DateTime> dateGenerator,
            string name = null)
            : base(name ?? $"Scope<{scopeGenerator.Name}, {dateGenerator.Name}>")
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
