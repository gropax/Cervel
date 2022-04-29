using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cervel.TimeParser.DateTimes
{
    public abstract class FrequencyGenerator : IDateTimeGenerator
    {
        public IEnumerable<DateTime> Generate(DateTime fromDate)
        {
            var date = fromDate;

            while (date < DateTime.MaxValue)
            {
                yield return date;
                date = GetNext(date);
            }

            yield return DateTime.MaxValue;
        }

        protected abstract DateTime GetNext(DateTime date);
    }
}
