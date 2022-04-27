using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cervel.Web.Models.Dates
{
    public class NthDayOfWeekInMonth : IYearlyDate
    {
        private readonly int _rank;
        private readonly DayOfWeek _weekday;
        private readonly Month _month;

        public NthDayOfWeekInMonth(int rank, DayOfWeek weekday, Month month)
        {
            ValidateDayOfWeekRank(rank);
            _rank = rank;
            _weekday = weekday;
            _month = month;
        }

        public DateTime GetDateTime(int year)
        {
            int fstDow = (int)new DateTime(year, (int)_month, 1).DayOfWeek;
            if (fstDow > (int)_weekday)
                fstDow -= 7;

            int fstDay = (int)_weekday - fstDow + 1;
            int day = fstDay + (_rank - 1) * 7;

            int daysInMonth = DateTime.DaysInMonth(year, (int)_month);
            if (day > daysInMonth)
                day -= 7;

            return new DateTime(year, (int)_month, day);
        }

        public void ValidateDayOfWeekRank(int rank)
        {
            int rankAbs = Math.Abs(rank);
            if (rankAbs == 0)
                throw new Exception($"DayOfWeek rank can't be 0.");
            else if (rankAbs > 5)
                throw new Exception($"DayOfWeek rank must be <= 5 but was [{rank}].");
        }
    }
}
