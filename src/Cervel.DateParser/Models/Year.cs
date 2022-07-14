using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cervel.TimeParser
{
    public class Year :
        ITimeUnit<Year>,
        IEquatable<Year>,
        IComparable<Year>
    {
        public int YearNumber { get; }
        public DateTime Start { get; }
        public DateTime End => TimeMeasures.Year.Shift(Start, 1);

        public Year(Date d) : this(d.DateTime) { }
        public Year(DateTime d)
        {
            YearNumber = d.Year;
            Start = new DateTime(d.Year, 1, 1);
        }

        public Year(int year)
        {
            YearNumber = year;
            Start = new DateTime(year, 1, 1);
        }

        public TimeInterval ToTimeInterval() => new TimeInterval(Start, End);


        public Year CutStart(DateTime startTime) => this;
        public Year CutEnd(DateTime endTime) => this;

        public Year Next(int shift = 1) => TimeMeasures.Year.Shift(this, shift);

        public Year Shift(TimeSpan timeSpan)
        {
            return new Year(Start + timeSpan);
        }


        public bool Equals(Year other)
        {
            return Start.Equals(other.Start);
        }

        public override bool Equals(object obj)
        {
            return obj is Year other && Equals(other);
        }

        public override int GetHashCode()
        {
            return Start.GetHashCode();
        }

        public int CompareTo(Year other)
        {
            return Start.CompareTo(other.Start);
        }

        public static bool operator < (Year left, Year right)
        {
            return left.CompareTo(right) < 0;
        }

        public static bool operator <= (Year left, Year right)
        {
            return left.CompareTo(right) <= 0;
        }

        public static bool operator > (Year left, Year right)
        {
            return left.CompareTo(right) > 0;
        }

        public static bool operator >= (Year left, Year right)
        {
            return left.CompareTo(right) >= 0;
        }
    }
}
