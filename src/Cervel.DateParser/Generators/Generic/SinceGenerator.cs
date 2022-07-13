using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cervel.TimeParser.Dates
{
    [DebuggerDisplay("{Name}")]
    public class SinceGenerator<T, TScope> : IGenerator<T>
        where T : ITimeInterval<T>
        where TScope : ITimeInterval<TScope>
    {
        public string Name { get; }
        private IGenerator<TScope> _scope;
        private IGenerator<T> _generator;

        public SinceGenerator(
            IGenerator<TScope> scope,
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

            foreach (var date in _generator.Generate(scopeEnum.Current.Start))
                yield return date;
        }
    }
}
