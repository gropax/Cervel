using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace Cervel.TimeParser.TimeIntervals
{
    [DebuggerDisplay("{Name}")]
    public class ComplementGenerator : IGenerator<TimeInterval>
    {
        public string Name { get; }
        private IGenerator<TimeInterval> _generator;

        public ComplementGenerator(
            IGenerator<TimeInterval> generator,
            string name = null)
        {
            _generator = generator;
            Name = name ?? $"Compl<{generator.Name}>";
        }

        public IEnumerable<TimeInterval> Generate(DateTime fromDate)
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
