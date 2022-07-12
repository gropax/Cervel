using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cervel.TimeParser.Dates
{
    [DebuggerDisplay("{Name}")]
    public class ShiftGenerator<T> : IGenerator<T> where T : ITimeInterval<T>
    {
        public string Name { get; }
        private IGenerator<T> _generator;
        private TimeSpan _timeSpan;

        public ShiftGenerator(
            IGenerator<T> generator,
            TimeSpan timeSpan,
            string name = null)
        {
            _generator = generator;
            _timeSpan = timeSpan;
            Name = name ?? $"Shift<{timeSpan}, {generator.Name}>";
        }

        public IEnumerable<T> Generate(DateTime fromDate)
        {
            return _generator.Generate(fromDate).Select(d => d.Shift(_timeSpan));
        }
    }
}
