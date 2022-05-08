using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cervel.TimeParser.TimeIntervals
{
    public class CoalesceGenerator : TimeIntervalGenerator
    {
        private IGenerator<TimeInterval> _generator;
        public CoalesceGenerator(
            IGenerator<TimeInterval> generator,
            string name = null)
            : base(name ?? $"Coalesce<{generator.Name}>")
        {
            _generator = generator;
        }

        public override IEnumerable<TimeInterval> Generate(DateTime fromDate)
        {
            var enumerator = _generator.Generate(fromDate).GetEnumerator();
            if (!enumerator.MoveNext())
                yield break;

            var current = enumerator.Current;
            var start = current.Start;
            var end = current.End;

            while (enumerator.MoveNext())
            {
                current = enumerator.Current;

                if (current.Start < start)
                    throw new Exception("TimeIntervals must be sorted by increasing Start.");

                else if (current.Start > end)
                {
                    yield return new TimeInterval(start, end);
                    start = current.Start;
                    end = current.End;
                }
                else
                    end = current.End > end ? current.End : end;  // max
            }

            yield return new TimeInterval(start, end);
        }
    }
}
