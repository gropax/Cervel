using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cervel.Web.Models.Dates.Yearly
{
    public class FirstValidDateBuilder : IYearlyDateBuilder
    {
        private readonly IYearlyDateBuilder[] _builders;

        public FirstValidDateBuilder(params IYearlyDateBuilder[] builders)
        {
            if (builders.Length == 0)
                throw new ArgumentNullException(nameof(builders));

            _builders = builders;
        }

        public bool TryBuild(int year, out DateTime date)
        {
            foreach (var builder in _builders)
            {
                if (builder.TryBuild(year, out var d))
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
