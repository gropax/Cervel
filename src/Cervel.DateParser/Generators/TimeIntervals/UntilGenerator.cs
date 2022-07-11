using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace Cervel.TimeParser.TimeIntervals
{
    [DebuggerDisplay("{Name}")]
    public class UntilGenerator : IGenerator<TimeInterval>
    {
        public string Name { get; }
        private IGenerator<TimeInterval> _generator;
        private DateTime _limit;

        public UntilGenerator(
            IGenerator<TimeInterval> generator,
            DateTime limit,
            string name = null)
        {
            _generator = generator;
            _limit = limit;
            Name = name ?? $"Until<{limit}, {generator.Name}>";
        }

        public IEnumerable<TimeInterval> Generate(DateTime fromDate)
        {
            var enumerator = _generator.Generate(fromDate).GetEnumerator();
            while (enumerator.MoveNext())
            {
                if (enumerator.Current.End < _limit)
                    yield return enumerator.Current;
                else
                {
                    if (enumerator.Current.Start < _limit)
                        yield return new TimeInterval(enumerator.Current.Start, _limit);

                    yield break;
                }
            }
        }
    }
}
