using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cervel.TimeParser.DateTimes
{
    public class FrequencyGenerator : DateTimeGenerator
    {
        private ITimeMeasure _timeMeasure;

        public FrequencyGenerator(
            ITimeMeasure timeMeasure,
            string name = null)
            : base(name ?? $"Freq<{timeMeasure.Name}>")
        {
            _timeMeasure = timeMeasure;
        }

        public override IEnumerable<DateTime> Generate(DateTime fromDate)
        {
            var date = fromDate;

            while (date < DateTime.MaxValue)
            {
                yield return date;
                date = _timeMeasure.AddTo(date);
            }

            yield return DateTime.MaxValue;
        }
    }
}
