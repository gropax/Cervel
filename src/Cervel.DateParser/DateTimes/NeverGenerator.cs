using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cervel.TimeParser.DateTimes
{
    public class NeverGenerator : DateTimeGenerator
    {
        public NeverGenerator(string name = null) : base(name ?? "Never") { }

        public override IEnumerable<Date> Generate(DateTime fromDate)
        {
            yield break;
        }
    }
}
