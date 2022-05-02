using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cervel.TimeParser.DateTimes
{
    public class UntilGenerator : DateTimeGenerator
    {
        private IGenerator<DateTime> _generator;
        private DateTime _limit;

        public UntilGenerator(IGenerator<DateTime> generator, DateTime limit)
        {
            _generator = generator;
            _limit = limit;
        }

        public override IEnumerable<DateTime> Generate(DateTime fromDate)
        {
            var enumerator = _generator.Generate(fromDate).GetEnumerator();
            while (enumerator.MoveNext() && enumerator.Current <= _limit)
                yield return enumerator.Current;
        }
    }
}
