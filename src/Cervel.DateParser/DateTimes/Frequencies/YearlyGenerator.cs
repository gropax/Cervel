using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cervel.TimeParser.DateTimes
{
    public class YearlyGenerator : FrequencyGenerator
    {
        private int _factor;
        public YearlyGenerator(int factor)
        {
            _factor = factor;
        }

        protected override DateTime GetNext(DateTime date)
        {
            int year = date.Year + _factor;
            int month = date.Month;
            var daysInMonth = DateTime.DaysInMonth(year, month);
            return new DateTime(year, month, Math.Min(date.Day, daysInMonth));
        }
    }
}
