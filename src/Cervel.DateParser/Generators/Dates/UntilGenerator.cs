using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace Cervel.TimeParser.Dates
{
    [DebuggerDisplay("{Name}")]
    public class UntilGenerator : IGenerator<Date>
    {
        public string Name { get; }
        private readonly IGenerator<Date> _scope;
        private readonly IGenerator<Date> _generator;

        public UntilGenerator(
            IGenerator<Date> scope,
            IGenerator<Date> generator,
            string name = null)
        {
            _scope = scope;
            _generator = generator;
            Name = name ?? $"Until<{scope.Name}, {generator.Name}>";
        }

        public IEnumerable<Date> Generate(DateTime fromDate)
        {
            var scopeEnum = _scope.Generate(fromDate).GetEnumerator();
            if (!scopeEnum.MoveNext())
                return _generator.Generate(fromDate);
            else
                return _generator.Generate(fromDate, scopeEnum.Current.DateTime);
        }
    }
}
