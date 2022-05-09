using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cervel.TimeParser.TimeIntervals
{
    public class ToPartitionGenerator : TimeIntervalGenerator
    {
        private IGenerator<DateTime> _generator;
        public ToPartitionGenerator(
            IGenerator<DateTime> generator,
            string name = null)
            : base(name ?? $"ToPartition<{generator.Name}>")
        {
            _generator = generator;
        }

        public override IEnumerable<TimeInterval> Generate(DateTime fromDate)
        {
            var enumerator = _generator.Generate(fromDate).GetEnumerator();
            if (!enumerator.MoveNext())
                yield return new TimeInterval(fromDate, DateTime.MaxValue);

            var last = enumerator.Current;
            if (last > fromDate)
                yield return new TimeInterval(fromDate, last);

            while (enumerator.MoveNext())
            {
                yield return new TimeInterval(last, enumerator.Current);
                last = enumerator.Current;
            }

            if (last < DateTime.MaxValue)
                yield return new TimeInterval(last, DateTime.MaxValue);
        }
    }
}
