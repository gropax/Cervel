using Cervel.TimeParser.DateTimes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace Cervel.TimeParser.TimeIntervals
{
    [DebuggerDisplay("{Name}")]
    public class FirstToInfinityGenerator : IGenerator<TimeInterval>
    {
        public string Name { get; }
        private IGenerator<Date> _generator;
        public FirstToInfinityGenerator(
            IGenerator<Date> generator,
            string name = null)
        {
            _generator = generator;
            Name = name ?? $"FirstToInfinity<{generator.Name}>";
        }

        public IEnumerable<TimeInterval> Generate(DateTime fromDate)
        {
            foreach (var date in _generator.Generate(fromDate))
            {
                yield return new TimeInterval(date.DateTime, DateTime.MaxValue);
                break;
            }
        }
    }
}
