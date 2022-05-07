using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cervel.TimeParser.TimeIntervals.Fuzzy
{
    public class SplitGenerator : IGenerator<IEnumerable<FuzzyInterval>>
    {
        protected string _name;
        public string Name => _name;

        private readonly IGenerator<FuzzyInterval> _generator;
        private readonly IGenerator<TimeInterval> _scopeGenerator;

        public SplitGenerator(IGenerator<FuzzyInterval> generator, IGenerator<TimeInterval> scopeGenerator)
        {
            _generator = generator;
            _scopeGenerator = scopeGenerator;
        }

        public IEnumerable<IEnumerable<FuzzyInterval>> Generate(DateTime fromDate)
        {
            foreach (var scope in _scopeGenerator.Generate(fromDate))
                yield return _generator.Generate(scope);
        }

        public IEnumerable<IEnumerable<FuzzyInterval>> Generate(DateTime fromDate, DateTime toDate)
        {
            foreach (var scope in _scopeGenerator.Generate(fromDate, toDate))
                yield return _generator.Generate(scope);
        }
    }
}
