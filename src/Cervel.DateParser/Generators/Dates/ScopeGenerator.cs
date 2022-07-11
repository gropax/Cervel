using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cervel.TimeParser.Dates
{
    public class ScopeGenerator : IGenerator<Date>
    {
        public string Name { get; }

        private IGenerator<TimeInterval> _scopeGenerator;
        private IGenerator<Date> _dateGenerator;

        public ScopeGenerator(
            IGenerator<TimeInterval> scopeGenerator,
            IGenerator<Date> dateGenerator,
            string name = null)
        {
            _scopeGenerator = scopeGenerator;
            _dateGenerator = dateGenerator;
            Name = name ?? $"Scope<{scopeGenerator.Name}, {dateGenerator.Name}>";
        }
        public IEnumerable<Date> Generate(DateTime fromDate) => Generate(fromDate, DateTime.MaxValue);
        public IEnumerable<Date> Generate(DateTime fromDate, DateTime toDate)
        {
            foreach (var interval in _scopeGenerator.Generate(fromDate, toDate))
                foreach (var date in _dateGenerator.Generate(interval))
                    yield return date;
        }
    }
}
