using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cervel.TimeParser.DateTimes
{
    public class OnceGenerator : DateTimeGenerator
    {
        private Date _value;
        public OnceGenerator(
            Date value = null,
            string name = null)
            : base(name ?? $"Once<{value}>")
        {
            _value = value;
        }

        public override IEnumerable<Date> Generate(DateTime fromDate)
        {
            yield return _value ?? new Date(fromDate);
        }
    }
}
