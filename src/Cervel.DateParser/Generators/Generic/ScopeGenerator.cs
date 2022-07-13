using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cervel.TimeParser.Dates
{
    public class ScopeGenerator<T, TScope> : IGenerator<T>
        where T : ITimeInterval<T>
        where TScope : ITimeInterval<TScope>
    {
        public string Name { get; }

        private IGenerator<TScope> _scopeGenerator;
        private IGenerator<T> _dateGenerator;

        public ScopeGenerator(
            IGenerator<TScope> scopeGenerator,
            IGenerator<T> dateGenerator,
            string name = null)
        {
            _scopeGenerator = scopeGenerator;
            _dateGenerator = dateGenerator;
            Name = name ?? $"Scope<{scopeGenerator.Name}, {dateGenerator.Name}>";
        }
        public IEnumerable<T> Generate(DateTime fromDate) => Generate(fromDate, DateTime.MaxValue);
        public IEnumerable<T> Generate(DateTime fromDate, DateTime toDate)
        {
            foreach (var interval in _scopeGenerator.Generate(fromDate, toDate))
                foreach (var date in _dateGenerator.Generate(interval))
                    yield return date;
        }
    }
}
