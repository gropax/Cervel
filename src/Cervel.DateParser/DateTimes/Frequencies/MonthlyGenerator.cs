using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cervel.TimeParser.DateTimes
{
    public class MonthlyGenerator : FrequencyGenerator
    {
        private int _factor;
        public MonthlyGenerator(
            int factor = 1,
            string name = null)
            : base(name ?? $"Monthly<{factor}>")
        {
            _factor = factor;
        }

        protected override DateTime GetNext(DateTime date)
        {
            int virtMonth = date.Month + _factor;
            int year = date.Year + (virtMonth - 1) / 12;
            int month = (virtMonth - 1) % 12 + 1;
            var daysInMonth = DateTime.DaysInMonth(year, month);
            return new DateTime(year, month, Math.Min(date.Day, daysInMonth));
        }
    }
}
