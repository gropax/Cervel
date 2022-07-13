using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cervel.TimeParser.TimeIntervals
{
    public class CoalesceGenerator<T> : IGenerator<TimeInterval>
        where T : ITimeInterval<T>
    {
        public string Name { get; }

        private IGenerator<T> _generator;
        public CoalesceGenerator(
            IGenerator<T> generator,
            string name = null)
        {
            _generator = generator;
            Name = name ?? $"Coalesce<{generator.Name}>";
        }

        public IEnumerable<TimeInterval> Generate(DateTime fromDate) => Generate(fromDate, DateTime.MaxValue);
        public IEnumerable<TimeInterval> Generate(DateTime fromDate, DateTime toDate)
        {
            var enumerator = _generator.Generate(fromDate, toDate).GetEnumerator();
            if (!enumerator.MoveNext())
                yield break;

            var current = enumerator.Current;
            var start = current.Start;
            var end = current.End;

            if (start >= toDate)
                yield break;
            else if (end >= toDate)
            {
                yield return new TimeInterval(start, toDate);
                yield break;
            } 

            while (enumerator.MoveNext())
            {
                current = enumerator.Current;

                if (current.Start < start)
                {
                    throw new Exception("TimeIntervals must be sorted by increasing Start.");
                }
                else if (current.Start > end)
                {
                    yield return new TimeInterval(start, end);

                    if (current.Start >= toDate)
                        yield break;
                    else if (current.End >= toDate)
                    {
                        yield return new TimeInterval(current.Start, toDate);
                        yield break;
                    } 
                    else
                    {
                        start = current.Start;
                        end = current.End;
                    }
                }
                else
                {
                    if (current.End >= toDate)
                    {
                        yield return new TimeInterval(start, toDate);
                        yield break;
                    } 
                    else
                        end = current.End > end ? current.End : end;  // max
                }
            }

            yield return new TimeInterval(start, end);
        }
    }
}
