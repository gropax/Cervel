using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace Cervel.TimeParser.TimeIntervals
{
    [DebuggerDisplay("{Name}")]
    public class AlwaysGenerator : IGenerator<TimeInterval>
    {
        public string Name { get; }
        public AlwaysGenerator(string name = null)
        {
            Name = name ?? $"Always<>";
        }

        public IEnumerable<TimeInterval> Generate(DateTime fromDate)
        {
            yield return new TimeInterval(fromDate, DateTime.MaxValue);
        }
    }
}
