using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cervel.Web.Models.Dates
{
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
        private static readonly Month[] LONG_MONTHS = new[]
        {
            Month.January, Month.March, Month.May, Month.July, Month.August, Month.October, Month.December,
        };
        public static bool IsLong(this Month month) => LONG_MONTHS.Contains(month);
        public static bool IsShort(this Month month) => !IsLong(month);
    }
}
