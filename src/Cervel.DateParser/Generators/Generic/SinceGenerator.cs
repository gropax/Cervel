using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cervel.TimeParser.Dates
{
    [DebuggerDisplay("{Name}")]
    public class SinceGenerator<T> : IGenerator<T> where T : ITimeInterval<T>
    {
        public string Name { get; }
        private IGenerator<Date> _scope;
        private IGenerator<T> _generator;

        public SinceGenerator(
            IGenerator<Date> scope,
            IGenerator<T> generator,
            string name = null)
        {
            _scope = scope;
            _generator = generator;
            Name = name ?? $"Since<{scope.Name}, {generator.Name}>";
        }

        public IEnumerable<T> Generate(DateTime fromDate)
        {
            var scopeEnum = _scope.Generate(fromDate).GetEnumerator();
            if (!scopeEnum.MoveNext())
                yield break;

            foreach (var date in _generator.Generate(scopeEnum.Current.DateTime))
                yield return date;
        }
    }
}
