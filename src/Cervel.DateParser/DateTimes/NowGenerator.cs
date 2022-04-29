using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cervel.TimeParser.DateTimes
{
    public class NowGenerator : IDateTimeGenerator
    {
        public IEnumerable<DateTime> Generate(DateTime fromDate)
        {
            yield return fromDate;
        }
    }
}
