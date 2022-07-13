using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cervel.TimeParser
{
    public class Day :
        ITimeUnit<Day>,
        IEquatable<Day>,
        IComparable<Day>
    {
        public int Year { get; }
        public int Month { get; }
        public int DayInMonth { get; }
        public DayOfWeek DayOfWeek { get; }
        public DateTime Start { get; }
        public DateTime End => Start + TimeSpan.FromDays(1);

        public Day(Date d) : this(d.DateTime) { }
        public Day(DateTime d)
        {
            Year = d.Year;
            Month = d.Month;
            DayInMonth = d.Day;
            Start = d;
            DayOfWeek = Start.DayOfWeek;
        }

        public Day(int year, int month, int day)
        {
            Year = year;
            Month = month;
            DayInMonth = day;
            Start = new DateTime(year, month, day);
            DayOfWeek = Start.DayOfWeek;
        }

        public TimeInterval ToTimeInterval() => new TimeInterval(Start, End);

        public bool TryGetNext(out Day dayInterval)
        {
            try
            {
                dayInterval = Next();
                return true;
            }
            catch
            {
                dayInterval = default;
                return false;
            }
        }

        public Day CutStart(DateTime startTime) => this;
        public Day CutEnd(DateTime endTime) => this;

        public Day Next(int shift = 1) => TimeMeasures.Day.Shift(this, shift);
        public Day Shift(TimeSpan timeSpan)
        {
            var dayShift = timeSpan.TotalDays;
            dayShift = dayShift > 0 ? Math.Floor(dayShift) : Math.Ceiling(dayShift);
            return new Day(Start + TimeSpan.FromDays(dayShift));
        }

        public bool Equals(Day other)
        {
            return Start.Equals(other.Start);
        }


        public override bool Equals(object obj)
        {
            return obj is Day other && Equals(other);
        }

        public override int GetHashCode()
        {
            return Start.GetHashCode();
        }

        public int CompareTo(Day other)
        {
            return Start.CompareTo(other.Start);
        }

        public static bool operator < (Day left, Day right)
        {
            return left.CompareTo(right) < 0;
        }

        public static bool operator <= (Day left, Day right)
        {
            return left.CompareTo(right) <= 0;
        }

        public static bool operator > (Day left, Day right)
        {
            return left.CompareTo(right) > 0;
        }

        public static bool operator >= (Day left, Day right)
        {
            return left.CompareTo(right) >= 0;
        }
    }
}
