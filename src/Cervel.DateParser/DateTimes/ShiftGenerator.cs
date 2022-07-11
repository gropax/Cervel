using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cervel.TimeParser.DateTimes
{
    [DebuggerDisplay("{Name}")]
    public class ShiftGenerator : IGenerator<Date>
    {
        public string Name { get; }
        private IGenerator<Date> _generator;
        private TimeSpan _timeSpan;

        public ShiftGenerator(
            IGenerator<Date> generator,
            TimeSpan timeSpan,
            string name = null)
        {
            _generator = generator;
            _timeSpan = timeSpan;
            Name = name ?? $"Shift<{timeSpan}, {generator.Name}>";
        }

        public IEnumerable<Date> Generate(DateTime fromDate)
        {
            return _generator.Generate(fromDate).Select(d => d.Shift(_timeSpan));
        }
    }
}
