using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cervel.Web.Models.Dates
{
    public abstract class NthDayBuilderBase
    {
        protected bool TryBuild(int year, Month month, int dayRank, out DateTime date)
        {
            date = default;

            int daysInMonth = DateTime.DaysInMonth(year, (int)month);
            if (Math.Abs(dayRank) > daysInMonth)
                return false;
            else
            {
                int day = dayRank > 0 ? dayRank : daysInMonth + 1 + dayRank;
                date = new DateTime(year, (int)month, day);
                return true;
            }
        }
    }
}
