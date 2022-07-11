using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cervel.TimeParser.Extensions;
using System.Diagnostics;

namespace Cervel.TimeParser.TimeIntervals
{
    [DebuggerDisplay("{Name}")]
    public class ShiftGenerator : IGenerator<TimeInterval>
    {
        public string Name { get; }
        private IGenerator<TimeInterval> _generator;
        private TimeSpan _timeSpan;

        public ShiftGenerator(
            IGenerator<TimeInterval> generator,
            TimeSpan timeSpan,
            string name = null)
        {
            _generator = generator;
            _timeSpan = timeSpan;
            Name = name ?? $"Shift<{timeSpan}, {generator.Name}>";
        }

        public IEnumerable<TimeInterval> Generate(DateTime fromDate)
        {
            return _generator.Generate(fromDate).Select(i => i.Shift(_timeSpan));
        }
    }
}
