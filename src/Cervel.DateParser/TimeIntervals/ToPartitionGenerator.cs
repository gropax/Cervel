using Cervel.TimeParser.DateTimes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cervel.TimeParser.TimeIntervals
{
    public class ToPartitionGenerator : TimeIntervalGenerator
    {
        private IGenerator<Date> _generator;
        public ToPartitionGenerator(
            IGenerator<Date> generator,
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
            if (last.DateTime > fromDate)
                yield return new TimeInterval(fromDate, last.DateTime);

            while (enumerator.MoveNext())
            {
                yield return new TimeInterval(last.DateTime, enumerator.Current.DateTime);
                last = enumerator.Current;
            }

            if (last.DateTime < DateTime.MaxValue)
                yield return new TimeInterval(last.DateTime, DateTime.MaxValue);
        }
    }
}
