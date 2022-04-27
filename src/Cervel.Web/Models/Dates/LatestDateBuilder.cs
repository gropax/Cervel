using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cervel.Web.Models.Dates
{
    public class LatestDateBuilder : YearlyDateSelector
    {
        public LatestDateBuilder(params IYearlyDateBuilder[] builders) : base(builders) { }
        protected override DateTime SelectDate(IEnumerable<DateTime> dates)
        {
            return dates.Max();
        }
    }
}
