using System;
using System.Collections.Generic;

namespace Cervel.TimeParser
{
    public interface ITimeIntervalGenerator
    {
        IEnumerable<TimeInterval> Generate(DateTime fromDate);
        IEnumerable<TimeInterval> Generate(DateTime fromDate, DateTime toDate)
        {
            var enumerator = Generate(fromDate).GetEnumerator();
            while (enumerator.MoveNext())
            {
                if (enumerator.Current.End < toDate)
                    yield return enumerator.Current;
                else
                {
                    yield return new TimeInterval(enumerator.Current.Start, toDate);
                    yield break;
                }
            }
        }
    }
}
