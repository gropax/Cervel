using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cervel.Web.Models.Dates.Monthly
{
    public class EarliestDateBuilder : MonthlyDateSelector
    {
        public EarliestDateBuilder(params IMonthlyDateBuilder[] builders) : base(builders) { }
        protected override DateTime SelectDate(IEnumerable<DateTime> dates)
        {
            return dates.Min();
        }
    }
}
