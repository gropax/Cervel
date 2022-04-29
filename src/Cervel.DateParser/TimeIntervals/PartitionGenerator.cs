using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cervel.TimeParser.TimeIntervals
{
    public class PartitionGenerator : TimeIntervalGenerator
    {
        private ITimeGenerator<DateTime> _generator;
        public PartitionGenerator(ITimeGenerator<DateTime> generator)
        {
            _generator = generator;
        }

        public override IEnumerable<TimeInterval> Generate(DateTime fromDate)
        {
            var start = fromDate;
            var end = DateTime.MaxValue;

            var enumerator = _generator.Generate(fromDate).GetEnumerator();
            while (enumerator.MoveNext())
            {
                end = enumerator.Current;
                yield return new TimeInterval(start, end);
                start = end;
            }

            if (start < DateTime.MaxValue)
                yield return new TimeInterval(start, end);
        }
    }
}
