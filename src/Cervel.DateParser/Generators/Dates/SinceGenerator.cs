using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cervel.TimeParser.Dates
{
    [DebuggerDisplay("{Name}")]
    public class SinceGenerator : IGenerator<Date>
    {
        public string Name { get; }
        private IGenerator<Date> _scope;
        private IGenerator<Date> _generator;

        public SinceGenerator(
            IGenerator<Date> scope,
            IGenerator<Date> generator,
            string name = null)
        {
            _scope = scope;
            _generator = generator;
            Name = name ?? $"Since<{scope.Name}, {generator.Name}>";
        }

        public IEnumerable<Date> Generate(DateTime fromDate)
        {
            var scopeEnum = _scope.Generate(fromDate).GetEnumerator();
            if (!scopeEnum.MoveNext())
                yield break;

            foreach (var date in _generator.Generate(scopeEnum.Current.DateTime))
                yield return date;
        }
    }
}
