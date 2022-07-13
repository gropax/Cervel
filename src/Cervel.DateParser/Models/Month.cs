using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cervel.TimeParser
{
    public class Month :
        ITimeUnit<Month>,
        IEquatable<Month>,
        IComparable<Month>
    {
        public int Year { get; }
        public MonthOfYear MonthOfYear { get; }
        public DateTime Start { get; }
        public DateTime End => TimeMeasures.Month.Shift(Start, 1);

        public Month(Date d) : this(d.DateTime) { }
        public Month(DateTime d)
        {
            Year = d.Year;
            MonthOfYear = (MonthOfYear)d.Month;
            Start = new DateTime(d.Year, d.Month, 1);
        }

        public Month(int year, int month)
        {
            Year = year;
            MonthOfYear = (MonthOfYear)month;
            Start = new DateTime(year, month, 1);
        }

        public TimeInterval ToTimeInterval() => new TimeInterval(Start, End);


        public Month CutStart(DateTime startTime) => this;
        public Month CutEnd(DateTime endTime) => this;

        public Month Next(int shift = 1) => TimeMeasures.Month.Shift(this, shift);

        public Month Shift(TimeSpan timeSpan)
        {
            return new Month(Start + timeSpan);
        }


        public bool Equals(Month other)
        {
            return Start.Equals(other.Start);
        }

        public override bool Equals(object obj)
        {
            return obj is Month other && Equals(other);
        }

        public override int GetHashCode()
        {
            return Start.GetHashCode();
        }

        public int CompareTo(Month other)
        {
            return Start.CompareTo(other.Start);
        }

        public static bool operator < (Month left, Month right)
        {
            return left.CompareTo(right) < 0;
        }

        public static bool operator <= (Month left, Month right)
        {
            return left.CompareTo(right) <= 0;
        }

        public static bool operator > (Month left, Month right)
        {
            return left.CompareTo(right) > 0;
        }

        public static bool operator >= (Month left, Month right)
        {
            return left.CompareTo(right) >= 0;
        }
    }
}
