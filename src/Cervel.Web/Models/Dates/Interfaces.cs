using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cervel.Web.Models.Dates
{
    public interface IYearlyDate
    {
        DateTime GetDateTime(int year);
    }
}
