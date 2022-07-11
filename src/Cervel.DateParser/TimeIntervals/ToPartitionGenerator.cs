using Cervel.TimeParser.DateTimes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace Cervel.TimeParser.TimeIntervals
{
    [DebuggerDisplay("{Name}")]
    public class ToPartitionGenerator : IGenerator<TimeInterval>
    {
        public string Name { get; }
        private IGenerator<Date> _generator;
        public ToPartitionGenerator(
            IGenerator<Date> generator,
            string name = null)
        {
            _generator = generator;
            Name = name ?? $"ToPartition<{generator.Name}>";
        }

        public IEnumerable<TimeInterval> Generate(DateTime fromDate)
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
