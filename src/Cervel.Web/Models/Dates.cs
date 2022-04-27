using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cervel.Web.Models
{
    public interface IYearlyDateDescriptor
    {
        DateTime GetDateTime(int year);
    }

    public enum Month
    {
        January = 1,
        February = 2,
        March = 3,
        April = 4,
        May = 5,
        June = 6,
        July = 7,
        August = 8,
        September = 9,
        October = 10,
        November = 11,
        December = 12,
    }

    public static class MonthExtensions
    {
        public static Month[] LONG_MONTHS = new[]
        {
            Month.January, Month.March, Month.May, Month.July, Month.August, Month.October, Month.December,
        };
        public static bool IsLong(this Month month) => LONG_MONTHS.Contains(month);
        public static bool IsShort(this Month month) => !IsLong(month);
    }

    public class NthInMonthDescriptor : IYearlyDateDescriptor
    {
        private int _day;
        private Month _month;
        private Func<int, DateTime> _dateTimeGetter;

        public NthInMonthDescriptor(int day, Month month)
        {
            ValidateDayRank(month, day);
            _day = day;
            _month = month;
        }

        public DateTime GetDateTime(int year)
        {
            int daysInMonth = DateTime.DaysInMonth(year, (int)_month);
            int day = _day > 0 ? Math.Min(_day, daysInMonth) : daysInMonth + 1 + _day;
            return new DateTime(year, (int)_month, day);
        }

        public void ValidateDayRank(Month month, int day)
        {
            int dayAbs = Math.Abs(day);
            if (dayAbs == 0)
                throw new ArgumentOutOfRangeException($"Day rank can't be 0.");
            else if (dayAbs > 31)
                throw new ArgumentOutOfRangeException($"Day rank must be <= 31 but was [{day}].");
            else if (dayAbs == 31 && month.IsShort())
                throw new ArgumentOutOfRangeException($"Day rank can't be 31 for month [{month}].");
            else if (dayAbs == 30 && month == Month.February)
                throw new ArgumentOutOfRangeException($"Day rank can't be 30 for month [{month}].");
        }
    }
}
