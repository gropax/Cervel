using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cervel.Web.Models.Dates.Monthly
{
    public class NthDayBuilder : NthDayBuilderBase, IMonthlyDateBuilder
    {
        private readonly int _day;
        public NthDayBuilder(int day)
        {
            ValidateDayRank(day);
            _day = day;
        }

        public bool TryBuild(int year, Month month, out DateTime date)
        {
            return TryBuild(year, month, _day, out date);
        }

        private void ValidateDayRank(int day)
        {
            int dayAbs = Math.Abs(day);
            if (dayAbs == 0)
                throw new Exception($"Day rank can't be 0.");
            else if (dayAbs > 31)
                throw new Exception($"Day rank must be <= 31 but was [{day}].");
        }
    }
}
