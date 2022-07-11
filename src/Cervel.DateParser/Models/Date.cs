using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cervel.TimeParser.Dates
{
    public class Date : ITimeInterval<Date>, IEquatable<Date>, IComparable<Date>
    {
        public DateTime DateTime { get; }
        public Date(DateTime dateTime)
        {
            DateTime = dateTime;
        }

        DateTime ITimeInterval.Start => DateTime;
        DateTime ITimeInterval.End => DateTime;

        public TimeInterval ToInterval(TimeSpan timeSpan)
        {
            if (timeSpan.Ticks < 0)
                return new TimeInterval(DateTime + timeSpan, DateTime);
            else
                return new TimeInterval(DateTime, DateTime + timeSpan);
        }

        public Date CutStart(DateTime endTime) => this;
        public Date CutEnd(DateTime endTime) => this;

        public Date Shift(TimeSpan timeSpan)
        {
            return new Date(DateTime + timeSpan);
        }

        public bool Equals(Date other)
        {
            return DateTime.Equals(other.DateTime);
        }

        public int CompareTo(Date other)
        {
            return DateTime.CompareTo(other.DateTime);
        }

        public override bool Equals(object obj)
        {
            return obj is Date other && Equals(other);
        }

        public override int GetHashCode()
        {
            return DateTime.GetHashCode();
        }

        public static bool operator <(Date left, Date right)
        {
            return left.CompareTo(right) < 0;
        }

        public static bool operator <=(Date left, Date right)
        {
            return left.CompareTo(right) <= 0;
        }

        public static bool operator >(Date left, Date right)
        {
            return left.CompareTo(right) > 0;
        }

        public static bool operator >=(Date left, Date right)
        {
            return left.CompareTo(right) >= 0;
        }
    }
}
