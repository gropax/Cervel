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

        public override IEnumerable<Date> Generate(DateTime fromDate)
        {
            var date = fromDate;

            while (date < DateTime.MaxValue)
            {
                yield return new Date(date);
                date = _timeMeasure.AddTo(new Date(date)).DateTime;
            }

            yield return new Date(DateTime.MaxValue);
        }
    }
}
