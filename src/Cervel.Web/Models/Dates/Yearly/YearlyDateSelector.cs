using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cervel.Web.Models.Dates.Yearly
{
    public abstract class YearlyDateSelector : IYearlyDateBuilder
    {
        private readonly IYearlyDateBuilder[] _builders;

        protected YearlyDateSelector(IYearlyDateBuilder[] builders)
        {
            if (builders.Length == 0)
                throw new ArgumentNullException(nameof(builders));

            _builders = builders;
        }

        protected abstract DateTime SelectDate(IEnumerable<DateTime> dates);

        public bool TryBuild(int year, out DateTime date)
        {
            var dates = new List<DateTime>();

            foreach (var builder in _builders)
                if (builder.TryBuild(year, out var d))
                    dates.Add(d);

            if (dates.Count == 0)
            {
                date = default;
                return false;
            }
            else
            {
                date = SelectDate(dates);
                return true;
            }
        }
    }
}
