using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cervel.TimeParser.TimeSpans
{
    public class NeverGenerator : ITimeSpanGenerator
    {
        public IEnumerable<TimeSpan> Generate(DateTime fromDate)
        {
            yield break;
        }
    }
}
