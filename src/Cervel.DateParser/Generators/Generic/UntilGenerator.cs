using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace Cervel.TimeParser.Dates
{
    [DebuggerDisplay("{Name}")]
    public class UntilGenerator<T, TScope> : IGenerator<T>
        where T : ITimeInterval<T>
        where TScope : ITimeInterval<TScope>
    {
        public string Name { get; }
        private readonly IGenerator<TScope> _scope;
        private readonly IGenerator<T> _generator;

        public UntilGenerator(
            IGenerator<TScope> scope,
            IGenerator<T> generator,
            string name = null)
        {
            _scope = scope;
            _generator = generator;
            Name = name ?? $"Until<{scope.Name}, {generator.Name}>";
        }

        public IEnumerable<T> Generate(DateTime fromDate)
        {
            var scopeEnum = _scope.Generate(fromDate).GetEnumerator();
            if (!scopeEnum.MoveNext())
                return _generator.Generate(fromDate);
            else
                return _generator.Generate(fromDate, scopeEnum.Current.Start);
        }
    }
}
