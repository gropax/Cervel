using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cervel.TimeParser.Dates
{
    public class Date : ITimeInterval<Date>
    {
        public DateTime DateTime { get; }
        public Date(DateTime dateTime)
        {
            DateTime = dateTime;
        }

        DateTime ITimeInterval.Start => DateTime;
        DateTime ITimeInterval.End => DateTime;

        public Date Cut(DateTime endTime)
        {
            return this;
        }
    }
}
