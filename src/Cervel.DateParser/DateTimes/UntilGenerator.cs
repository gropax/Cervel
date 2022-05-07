using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cervel.TimeParser.DateTimes
{
    public class UntilGenerator : DateTimeGenerator
    {
        private IGenerator<DateTime> _scope;
        private IGenerator<DateTime> _generator;

        public UntilGenerator(IGenerator<DateTime> scope, IGenerator<DateTime> generator)
        {
            _scope = scope;
            _generator = generator;
        }

        public override IEnumerable<DateTime> Generate(DateTime fromDate)
        {
            var scopeEnum = _scope.Generate(fromDate).GetEnumerator();
            if (!scopeEnum.MoveNext())
                return _generator.Generate(fromDate);
            else
                return _generator.Generate(fromDate, scopeEnum.Current);
        }
    }
}
