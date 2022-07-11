using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cervel.TimeParser
{
    public interface IGenerator2<out T> where T : ITimeInterval<T>
    {
        string Name { get; }
        IEnumerable<T> Generate(DateTime fromDate);
        IEnumerable<T> Generate(ITimeInterval interval) => Generate(interval.Start, interval.End);
        IEnumerable<T> Generate(DateTime fromDate, DateTime toDate)
        {
            var enumerator = Generate(fromDate).GetEnumerator();
            while (enumerator.MoveNext())
            {
                if (enumerator.Current.End < toDate)
                    yield return enumerator.Current;
                else
                {
                    if (enumerator.Current.Start < toDate)
                        yield return enumerator.Current.Cut(toDate);

                    yield break;
                }
            }
        }
    }
}
