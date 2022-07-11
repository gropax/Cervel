using Cervel.TimeParser.DateTimes;
using Cervel.TimeParser.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cervel.TimeParser
{
    [DebuggerDisplay("[{Start}, {End}]")]
    public struct TimeInterval : ITimeInterval<TimeInterval>
    {
        public DateTime Start { get; }
        public DateTime End { get; }

        public TimeInterval(Date start, Date end)
            : this(start.DateTime, end.DateTime)
        { }

        public TimeInterval(DateTime start, DateTime end)
        {
            if (start > end)
                throw new Exception($"Interval start must be <= to its end.");

            Start = start;
            End = end;
        }

        public TimeSpan Length => End - Start;

        public override string ToString()
        {
            return $"[{Start}, {End}]";
        }

        public TimeInterval Cut(DateTime end)
        {
            return new TimeInterval(Start, end);
        }

        public TimeInterval Shift(TimeSpan timeSpan)
        {
            return new TimeInterval(Start + timeSpan, End + timeSpan);
        }
    }


    [DebuggerDisplay("[{Start}, {End}, {Value}]")]
    public struct TimeInterval<T> : ITimeInterval<TimeInterval<T>>
    {
        public DateTime Start { get; }
        public DateTime End { get; }
        public T Value { get; }

        public TimeInterval(TimeInterval interval, T value) : this(interval.Start, interval.End, value) { }
        public TimeInterval(DateTime start, DateTime end, T value)
        {
            if (start > end)
                throw new Exception($"Interval start must be <= to its end.");

            Start = start;
            End = end;
            Value = value;
        }

        public TimeSpan Length => End - Start;

        public override string ToString()
        {
            return $"[{Start}, {End}, {Value}]";
        }

        public TimeInterval<T> Cut(DateTime endTime)
        {
            return new TimeInterval<T>(Start, endTime, Value);
        }

        public TimeInterval<T> Shift(TimeSpan timeSpan)
        {
            return new TimeInterval<T>(Start + timeSpan, End + timeSpan, Value);
        }
    }
}
