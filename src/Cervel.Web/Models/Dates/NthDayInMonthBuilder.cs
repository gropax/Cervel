using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cervel.Web.Models.Dates
{
    public class NthDayInMonthBuilder : IYearlyDateBuilder
    {
        private readonly int _day;
        private readonly Month _month;

        public NthDayInMonthBuilder(int day, Month month)
        {
            ValidateDayRank(month, day);
            _day = day;
            _month = month;
        }

        public bool TryBuild(int year, out DateTime date)
        {
            date = default;

            int daysInMonth = DateTime.DaysInMonth(year, (int)_month);
            if (Math.Abs(_day) > daysInMonth)
                return false;
            else
            {
                int day = _day > 0 ? _day : daysInMonth + 1 + _day;
                date = new DateTime(year, (int)_month, day);
                return true;
            }
        }

        public void ValidateDayRank(Month month, int day)
        {
            int dayAbs = Math.Abs(day);
            if (dayAbs == 0)
                throw new Exception($"Day rank can't be 0.");
            else if (dayAbs > 31)
                throw new Exception($"Day rank must be <= 31 but was [{day}].");
            else if (dayAbs == 31 && month.IsShort())
                throw new Exception($"Day rank can't be 31 for month [{month}].");
            else if (dayAbs == 30 && month == Month.February)
                throw new Exception($"Day rank can't be 30 for month [{month}].");
        }
    }
}
