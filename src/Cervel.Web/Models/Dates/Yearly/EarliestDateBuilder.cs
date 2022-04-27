using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cervel.Web.Models.Dates.Yearly
{
    public class EarliestDateBuilder : YearlyDateSelector
    {
        public EarliestDateBuilder(params IYearlyDateBuilder[] builders) : base(builders) { }
        protected override DateTime SelectDate(IEnumerable<DateTime> dates)
        {
            return dates.Min();
        }
    }
}
