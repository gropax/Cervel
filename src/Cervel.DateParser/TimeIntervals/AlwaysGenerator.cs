using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cervel.TimeParser.TimeIntervals
{
    public class AlwaysGenerator : ITimeIntervalGenerator
    {
        public IEnumerable<TimeInterval> Generate(DateTime fromDate)
        {
            yield return new TimeInterval(fromDate, DateTime.MaxValue);
        }
    }
}
