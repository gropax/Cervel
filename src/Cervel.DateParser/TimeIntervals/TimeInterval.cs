using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cervel.TimeParser
{
    public struct TimeInterval
    {
        public DateTime Start { get; }
        public DateTime End { get; }

        public TimeInterval(DateTime start, DateTime end)
        {
            if (start > end)
                throw new Exception($"Interval start must be <= to its end.");

            Start = start;
            End = end;
        }
    }
}
