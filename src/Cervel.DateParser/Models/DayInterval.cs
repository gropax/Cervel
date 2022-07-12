using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cervel.TimeParser
{
    public class DayInterval :
        ITimeMeasure<DayInterval>,
        IEquatable<DayInterval>,
        IComparable<DayInterval>
    {
        public int Year { get; }
        public int Month { get; }
        public int Day { get; }
        public DateTime Start { get; }
        public DateTime End => Start + TimeSpan.FromDays(1);

        public DayInterval(Date d) : this(d.DateTime) { }
        public DayInterval(DateTime d)
        {
            Year = d.Year;
            Month = d.Month;
            Day = d.Day;
            Start = d;
        }

        public DayInterval(int year, int month, int day)
        {
            Year = year;
            Month = month;
            Day = day;
            Start = new DateTime(year, month, day);
        }

        public TimeInterval ToTimeInterval() => new TimeInterval(Start, End);

        public DayInterval Next() => new DayInterval(Start + TimeSpan.FromDays(1));
        public bool TryGetNext(out DayInterval dayInterval)
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

        public DayInterval CutStart(DateTime startTime) => this;
        public DayInterval CutEnd(DateTime endTime) => this;

        public DayInterval Shift(TimeSpan timeSpan)
        {
            var dayShift = timeSpan.TotalDays;
            dayShift = dayShift > 0 ? Math.Floor(dayShift) : Math.Ceiling(dayShift);
            return new DayInterval(Start + TimeSpan.FromDays(dayShift));
        }

        public DayInterval Increment(int shift)
        {
            return new DayInterval(Start + TimeSpan.FromDays(shift));
        }

        public bool Equals(DayInterval other)
        {
            return Start.Equals(other.Start);
        }



        public int CompareTo(DayInterval other)
        {
            return Start.CompareTo(other.Start);
        }

        public override bool Equals(object obj)
        {
            return obj is DayInterval other && Equals(other);
        }

        public override int GetHashCode()
        {
            return Start.GetHashCode();
        }

        public static bool operator < (DayInterval left, DayInterval right)
        {
            return left.CompareTo(right) < 0;
        }

        public static bool operator <= (DayInterval left, DayInterval right)
        {
            return left.CompareTo(right) <= 0;
        }

        public static bool operator > (DayInterval left, DayInterval right)
        {
            return left.CompareTo(right) > 0;
        }

        public static bool operator >= (DayInterval left, DayInterval right)
        {
            return left.CompareTo(right) >= 0;
        }
    }
}
