using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cervel.Web.Models.Dates.Monthly
{
    public class FirstValidDateBuilder : IMonthlyDateBuilder
    {
        private readonly IMonthlyDateBuilder[] _builders;

        public FirstValidDateBuilder(params IMonthlyDateBuilder[] builders)
        {
            if (builders.Length == 0)
                throw new ArgumentNullException(nameof(builders));

            _builders = builders;
        }

        public bool TryBuild(int year, Month month, out DateTime date)
        {
            foreach (var builder in _builders)
            {
                if (builder.TryBuild(year, month, out var d))
                {
                    date = d;
                    return true;
                }
            }

            date = default;
            return false;
        }
    }
}
