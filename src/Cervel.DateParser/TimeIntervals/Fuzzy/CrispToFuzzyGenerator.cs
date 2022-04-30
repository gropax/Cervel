using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cervel.TimeParser.TimeIntervals.Fuzzy
{
    public class CrispToFuzzyGenerator : FuzzyIntervalGenerator
    {
        public IGenerator<TimeInterval> _generator;
        public CrispToFuzzyGenerator(IGenerator<TimeInterval> generator)
        {
            _generator = generator;
        }

        public override IEnumerable<FuzzyInterval> Generate(DateTime fromDate)
        {
            var enumerator = _generator.Generate(fromDate).GetEnumerator();
            var index = fromDate;

            while (enumerator.MoveNext())
            {
                var interval = enumerator.Current;
                if (index < interval.Start)
                    yield return new FuzzyInterval(index, interval.Start, 0, 0);

                yield return new FuzzyInterval(interval.Start, interval.End, 1, 1);
                index = interval.End;
            }
        }
    }
}
