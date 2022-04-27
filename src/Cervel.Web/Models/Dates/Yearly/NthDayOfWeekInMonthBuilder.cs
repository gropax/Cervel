using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cervel.Web.Models.Dates.Yearly
{
    public class NthDayOfWeekInMonthBuilder : NthDayOfWeekBuilderBase, IYearlyDateBuilder
    {
        private readonly int _rank;
        private readonly DayOfWeek _weekday;
        private readonly Month _month;

        public NthDayOfWeekInMonthBuilder(int rank, DayOfWeek weekday, Month month)
        {
            ValidateDayOfWeekRank(rank);
            _rank = rank;
            _weekday = weekday;
            _month = month;
        }

        public bool TryBuild(int year, out DateTime date)
        {
            return TryBuild(year, _month, _weekday, _rank, out date);
        }
    }
}
