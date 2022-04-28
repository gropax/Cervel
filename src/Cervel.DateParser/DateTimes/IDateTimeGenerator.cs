using System;
using System.Collections.Generic;

namespace Cervel.TimeParser
{
    public interface IDateTimeGenerator
    {
        IEnumerable<DateTime> Generate(DateTime fromDate);
        IEnumerable<DateTime> Generate(DateTime fromDate, DateTime toDate)
        {
            var enumerator = Generate(fromDate).GetEnumerator();
            while (enumerator.MoveNext() && enumerator.Current <= toDate)
                yield return enumerator.Current;
        }
    }
}
