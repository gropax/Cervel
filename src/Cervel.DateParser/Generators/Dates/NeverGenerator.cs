using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace Cervel.TimeParser.Dates
{
    [DebuggerDisplay("{Name}")]
    public class NeverGenerator : IGenerator<Date>
    {
        public string Name { get; }
        public NeverGenerator(string name = null)
        {
            Name = name ?? "Never";
        }

        public IEnumerable<Date> Generate(DateTime fromDate)
        {
            yield break;
        }
    }
}
