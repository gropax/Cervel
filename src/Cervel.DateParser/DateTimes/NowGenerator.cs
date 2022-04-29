using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cervel.TimeParser.DateTimes
{
    public class NowGenerator : DateTimeGenerator
    {
        public override IEnumerable<DateTime> Generate(DateTime fromDate)
        {
            yield return fromDate;
        }
    }
}
