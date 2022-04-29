using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cervel.TimeParser.TimeIntervals
{
    public class FirstToInfinityGenerator : TimeIntervalGenerator
    {
        private ITimeGenerator<DateTime> _generator;
        public FirstToInfinityGenerator(ITimeGenerator<DateTime> generator)
        {
            _generator = generator;
        }

        public override IEnumerable<TimeInterval> Generate(DateTime fromDate)
        {
            foreach (var date in _generator.Generate(fromDate))
            {
                yield return new TimeInterval(date, DateTime.MaxValue);
                break;
            }
        }
    }
}
