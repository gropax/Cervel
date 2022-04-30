using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cervel.TimeParser.TimeIntervals.Fuzzy
{
    public abstract class FuzzyIntervalGenerator : IGenerator<FuzzyInterval>
    {
        public abstract IEnumerable<FuzzyInterval> Generate(DateTime fromDate);

        public IEnumerable<FuzzyInterval> Generate(DateTime fromDate, DateTime toDate)
        {
            var enumerator = Generate(fromDate).GetEnumerator();
            while (enumerator.MoveNext())
            {
                if (enumerator.Current.End < toDate)
                    yield return enumerator.Current;
                else
                {
                    if (enumerator.Current.Start < toDate)
                        yield return enumerator.Current.LeftCut(toDate);

                    yield break;
                }
            }
        }
    }
}
