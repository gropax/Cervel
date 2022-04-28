using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cervel.TimeParser.DateTimes
{
    public class ShiftGenerator : IDateTimeGenerator
    {
        private IDateTimeGenerator _generator;
        private TimeSpan _timeSpan;

        public ShiftGenerator(IDateTimeGenerator generator, TimeSpan timeSpan)
        {
            _generator = generator;
            _timeSpan = timeSpan;
        }

        public IEnumerable<DateTime> Generate(DateTime fromDate)
        {
            return _generator.Generate(fromDate + _timeSpan);
        }
    }
}
