using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cervel.TimeParser.DateTimes
{
    public class DailyGenerator : FrequencyGenerator
    {
        private TimeSpan _timeSpan;
        public DailyGenerator(TimeSpan timeSpan)
        {
            _timeSpan = timeSpan;
        }

        protected override DateTime GetNext(DateTime date) => date.Shift(_timeSpan);
    }
}
