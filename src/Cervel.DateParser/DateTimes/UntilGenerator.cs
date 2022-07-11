using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cervel.TimeParser.DateTimes
{
    public class UntilGenerator : DateTimeGenerator
    {
        private readonly IGenerator<Date> _scope;
        private readonly IGenerator<Date> _generator;

        public UntilGenerator(
            IGenerator<Date> scope,
            IGenerator<Date> generator,
            string name = null)
            : base(name ?? $"Until<{scope.Name}, {generator.Name}>")
        {
            _scope = scope;
            _generator = generator;
        }

        public override IEnumerable<Date> Generate(DateTime fromDate)
        {
            var scopeEnum = _scope.Generate(fromDate).GetEnumerator();
            if (!scopeEnum.MoveNext())
                return _generator.Generate(fromDate);
            else
                return _generator.Generate(fromDate, scopeEnum.Current.DateTime);
        }
    }
}
