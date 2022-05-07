using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cervel.TimeParser.Extensions;

namespace Cervel.TimeParser.TimeIntervals
{
    public class ShiftGenerator : TimeIntervalGenerator
    {
        private IGenerator<TimeInterval> _generator;
        private TimeSpan _timeSpan;

        public ShiftGenerator(
            IGenerator<TimeInterval> generator,
            TimeSpan timeSpan,
            string name = null)
            : base(name ?? $"Shift<{timeSpan}, {generator.Name}>")
        {
            _generator = generator;
            _timeSpan = timeSpan;
        }

        public override IEnumerable<TimeInterval> Generate(DateTime fromDate)
        {
            return _generator.Generate(fromDate).Select(i => i.Shift(_timeSpan));
        }
    }
}
