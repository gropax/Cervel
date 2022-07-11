using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace Cervel.TimeParser.Dates
{
    [DebuggerDisplay("{Name}")]
    public class NowGenerator : IGenerator<Date>
    {
        public string Name { get; }

        public NowGenerator(string name = null)
        {
            Name = name ?? "Now";
        }

        public IEnumerable<Date> Generate(DateTime fromDate)
        {
            yield return new Date(fromDate);
        }
    }
}
