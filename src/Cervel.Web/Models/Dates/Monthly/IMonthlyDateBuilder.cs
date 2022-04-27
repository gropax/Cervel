using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cervel.Web.Models.Dates.Monthly
{
    public interface IMonthlyDateBuilder
    {
        bool TryBuild(int year, Month month, out DateTime date);
    }
}
