using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cervel.TimeParser.Models
{
    public class DayInterval : ITimeInterval<DayInterval>
    {
        public int Year { get; }
        public int Month { get; }
        public int Day { get; }

        public DayInterval(DateTime d)
            : this(d.Year, d.Month, d.Day) { }

        public DayInterval(int year, int month, int day)
        {
            Year = year;
            Month = month;
            Day = day;
        }

        public DateTime Start => new DateTime(Year, Month, Day);
        public DateTime End => Start + TimeSpan.FromDays(1);
        public TimeInterval ToTimeInterval() => new TimeInterval(Start, End);

        public DayInterval Cut(DateTime endTime)
        {
            return this;
        }

        public DayInterval Shift(TimeSpan timeSpan)
        {
            var dayShift = timeSpan.TotalDays;
            dayShift = dayShift > 0 ? Math.Floor(dayShift) : Math.Ceiling(dayShift);
            return new DayInterval(Start + TimeSpan.FromDays(dayShift));
        }
    }
}
