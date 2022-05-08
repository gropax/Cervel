using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cervel.TimeParser.Extensions;

namespace Cervel.TimeParser
{
    public static class TimeIntervalExtensions
    {
        public static TimeInterval Shift(this TimeInterval dateTime, TimeSpan timeSpan)
        {
            var start = dateTime.Start.Shift(timeSpan);
            var end = dateTime.End.Shift(timeSpan);
            return new TimeInterval(start, end);
        }

        public static IEnumerable<TimeInterval> Disjunction(this IEnumerable<TimeInterval> intervals)
        {
            var enumerator = intervals.GetEnumerator();
            if (!enumerator.MoveNext())
                yield break;

            var current = enumerator.Current;
            var start = current.Start;
            var end = current.End;

            while (enumerator.MoveNext())
            {
                current = enumerator.Current;

                if (current.Start < start)
                    throw new Exception("TimeIntervals must be sorted by increasing Start.");

                else if (current.Start > end)
                {
                    yield return new TimeInterval(start, end);
                    start = current.Start;
                    end = current.End;
                }
                else
                    end = current.End > end ? current.End : end;  // max
            }

            yield return new TimeInterval(start, end);
        }
    }
}
