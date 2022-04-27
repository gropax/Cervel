using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cervel.Web.Models.Dates.Monthly
{
    public class NthDayOfWeekBuilder : NthDayOfWeekBuilderBase, IMonthlyDateBuilder
    {
        private readonly int _rank;
        private readonly DayOfWeek _weekday;

        public NthDayOfWeekBuilder(int rank, DayOfWeek weekday)
        {
            ValidateDayOfWeekRank(rank);
            _rank = rank;
            _weekday = weekday;
        }

        public bool TryBuild(int year, Month month, out DateTime date)
        {
            return TryBuild(year, month, _weekday, _rank, out date);
        }
    }
}
