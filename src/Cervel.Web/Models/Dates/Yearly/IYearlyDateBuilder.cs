using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cervel.Web.Models.Dates.Yearly
{
    public interface IYearlyDateBuilder
    {
        bool TryBuild(int year, out DateTime date);
    }
}
