using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace Cervel.TimeParser.TimeIntervals
{
    [DebuggerDisplay("{Name}")]
    public class NeverGenerator : IGenerator<TimeInterval>
    {
        public string Name { get; }
        public NeverGenerator(string name = null)
        {
            Name = name ?? $"Never<>";
        }

        public IEnumerable<TimeInterval> Generate(DateTime fromDate)
        {
            yield break;
        }
    }
}
