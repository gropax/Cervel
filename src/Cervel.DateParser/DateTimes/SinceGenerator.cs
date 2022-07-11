using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cervel.TimeParser.DateTimes
{
    public class SinceGenerator : DateTimeGenerator
    {
        private IGenerator<Date> _scope;
        private IGenerator<Date> _generator;

        public SinceGenerator(
            IGenerator<Date> scope,
            IGenerator<Date> generator,
            string name = null)
            : base(name ?? $"Since<{scope.Name}, {generator.Name}>")
        {
            _scope = scope;
            _generator = generator;
        }

        public override IEnumerable<Date> Generate(DateTime fromDate)
        {
            var scopeEnum = _scope.Generate(fromDate).GetEnumerator();
            if (!scopeEnum.MoveNext())
                yield break;

            foreach (var date in _generator.Generate(scopeEnum.Current.DateTime))
                yield return date;
        }
    }
}
