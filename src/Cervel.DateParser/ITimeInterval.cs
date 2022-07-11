using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cervel.TimeParser
{
    public interface ITimeInterval
    {
        DateTime Start { get; }
        DateTime End { get; }
        bool IsBefore(DateTime date) => End < date;
        bool IsAfter(DateTime date) => date < Start;
        bool IsBefore(ITimeInterval other) => End < other.Start;
        bool IsAfter(ITimeInterval other) => other.End < Start;
    }

    public interface ITimeInterval<T> : ITimeInterval
    {
        T Cut(DateTime endTime);
        T Shift(TimeSpan timeSpan);
    }
}
