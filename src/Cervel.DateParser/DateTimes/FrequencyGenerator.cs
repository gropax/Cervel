using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cervel.TimeParser.DateTimes
{
    [DebuggerDisplay("{Name}")]
    public class FrequencyGenerator : IGenerator<Date>
    {
        public string Name { get; }
        private ITimeMeasure _timeMeasure;

        public FrequencyGenerator(
            ITimeMeasure timeMeasure,
            string name = null)
            : base()
        {
            _timeMeasure = timeMeasure;
            Name = name ?? $"Freq<{timeMeasure.Name}>";
        }

        public IEnumerable<Date> Generate(DateTime fromDate)
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
