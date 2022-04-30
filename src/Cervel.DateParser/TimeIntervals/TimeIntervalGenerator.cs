using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cervel.TimeParser.TimeIntervals
{
    public abstract class TimeIntervalGenerator : ITimeGenerator<TimeInterval>
    {
        public abstract IEnumerable<TimeInterval> Generate(DateTime fromDate);

        public IEnumerable<TimeInterval> Generate(DateTime fromDate, DateTime toDate)
        {
            var enumerator = Generate(fromDate).GetEnumerator();
            while (enumerator.MoveNext())
            {
                if (enumerator.Current.End < toDate)
                    yield return enumerator.Current;
                else
                {
                    if (enumerator.Current.Start < toDate)
                        yield return new TimeInterval(enumerator.Current.Start, toDate);

                    yield break;
                }
            }
        }
    }
}
