using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace Cervel.TimeParser.Dates
{
    [DebuggerDisplay("{Name}")]
    public class OnceGenerator : IGenerator<Date>
    {
        public string Name { get; }
        private Date _value;
        public OnceGenerator(
            Date value = null,
            string name = null)
        {
            _value = value;
            Name = name ?? $"Once<{value}>";
        }

        public IEnumerable<Date> Generate(DateTime fromDate)
        {
            yield return _value ?? new Date(fromDate);
        }
    }
}
