using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cervel.TimeParser.DateTimes
{
    public class ShiftGenerator : DateTimeGenerator
    {
        private IGenerator<Date> _generator;
        private TimeSpan _timeSpan;

        public ShiftGenerator(
            IGenerator<Date> generator,
            TimeSpan timeSpan,
            string name = null)
            : base(name ?? $"Shift<{timeSpan}, {generator.Name}>")
        {
            _generator = generator;
            _timeSpan = timeSpan;
        }

        public override IEnumerable<Date> Generate(DateTime fromDate)
        {
            return _generator.Generate(fromDate).Select(d => d.Shift(_timeSpan));
        }
    }
}
