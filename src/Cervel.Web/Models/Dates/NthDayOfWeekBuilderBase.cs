using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cervel.Web.Models.Dates
{
    public abstract class NthDayOfWeekBuilderBase
    {
        protected bool TryBuild(int year, Month month, DayOfWeek dow, int dayRank, out DateTime date)
        {
            date = default;

            int fstDow = (int)new DateTime(year, (int)month, 1).DayOfWeek;
            if (fstDow > (int)dow)
                fstDow -= 7;

            int fstDay = (int)dow - fstDow + 1;
            int day = fstDay + (dayRank - 1) * 7;

            int days = DateTime.DaysInMonth(year, (int)month);
            if (day > days)
                return false;
            else
            {
                date = new DateTime(year, (int)month, day);
                return true;
            }
        }

        protected void ValidateDayOfWeekRank(int rank)
        {
            int rankAbs = Math.Abs(rank);
            if (rankAbs == 0)
                throw new Exception($"DayOfWeek rank can't be 0.");
            else if (rankAbs > 5)
                throw new Exception($"DayOfWeek rank must be <= 5 but was [{rank}].");
        }
    }
}
