using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cervel.TimeParser.TimeIntervals
{
    public class PartitionGenerator : ITimeIntervalGenerator
    {
        private IDateTimeGenerator _generator;
        public PartitionGenerator(IDateTimeGenerator generator)
        {
            _generator = generator;
        }

        public IEnumerable<TimeInterval> Generate(DateTime fromDate)
        {
            var enumerator = _generator.Generate(fromDate).GetEnumerator();
            if (!enumerator.MoveNext())
                yield break;

            var start = enumerator.Current;

            while (enumerator.MoveNext())
            {
                var end = enumerator.Current;
                yield return new TimeInterval(start, end);
                start = end;
            }
        }
    }
}
