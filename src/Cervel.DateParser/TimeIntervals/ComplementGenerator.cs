using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cervel.TimeParser.TimeIntervals
{
    public class ComplementGenerator : TimeIntervalGenerator
    {
        private IGenerator<TimeInterval> _generator;
        public ComplementGenerator(
            IGenerator<TimeInterval> generator,
            string name = null)
            : base(name ?? $"Compl<{generator.Name}>")
        {
            _generator = generator;
        }

        public override IEnumerable<TimeInterval> Generate(DateTime fromDate)
        {
            var enumerator = _generator.Generate(fromDate).GetEnumerator();
            if (!enumerator.MoveNext())
                yield return new TimeInterval(fromDate, DateTime.MaxValue);

            var start = enumerator.Current.Start;
            var end = enumerator.Current.End;
            if (start > fromDate)
                yield return new TimeInterval(fromDate, start);

            while (enumerator.MoveNext())
            {
                start = enumerator.Current.Start;
                yield return new TimeInterval(end, start);
                end = enumerator.Current.End;
            }

            if (end < DateTime.MaxValue)
                yield return new TimeInterval(end, DateTime.MaxValue);
        }
    }
}
