using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cervel.TimeParser.DateTimes
{
    public class OnceGenerator : DateTimeGenerator
    {
        private DateTime _value;
        public OnceGenerator(DateTime value)
        {
            _value = value;
        }

        public override IEnumerable<DateTime> Generate(DateTime fromDate)
        {
            yield return _value;
        }
    }
}
