using System;
using System.Collections.Generic;

namespace Cervel.TimeParser
{
    public interface ITimeSpanGenerator
    {
        IEnumerable<TimeSpan> Generate(DateTime fromDate);
        IEnumerable<TimeSpan> Generate(DateTime fromDate, DateTime toDate)
        {
            var enumerator = Generate(fromDate).GetEnumerator();
            while (enumerator.MoveNext())
            {
                if (enumerator.Current.End < toDate)
                    yield return enumerator.Current;
                else
                {
                    yield return new TimeSpan(enumerator.Current.Start, toDate);
                    yield break;
                }
            }
        }
    }
}
