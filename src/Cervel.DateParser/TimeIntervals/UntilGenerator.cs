using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cervel.TimeParser.TimeIntervals
{
    public class UntilGenerator : TimeIntervalGenerator
    {
        private IGenerator<TimeInterval> _generator;
        private DateTime _limit;

        public UntilGenerator(
            IGenerator<TimeInterval> generator,
            DateTime limit,
            string name = null)
            : base(name ?? $"Until<{limit}, {generator.Name}>")
        {
            _generator = generator;
            _limit = limit;
        }

        public override IEnumerable<TimeInterval> Generate(DateTime fromDate)
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
