using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace Cervel.TimeParser.TimeIntervals
{
    [DebuggerDisplay("{Name}")]
    public class MonthByNameGenerator : IGenerator<Month>
    {
        private MonthOfYear _targetMonth;
        public string Name { get; }

        public MonthByNameGenerator(
            MonthOfYear month,
            string name = null)
        {
            _targetMonth = month;
            Name = name ?? $"{month}";
        }

        public IEnumerable<Month> Generate(DateTime fromDate)
        {
            var currentMonth = (MonthOfYear)fromDate.Month;
            int nextTargetMonth = _targetMonth >= currentMonth ? (int)_targetMonth : (int)_targetMonth + 12;
            int inc = nextTargetMonth - (int)currentMonth;

            var month = new Month(fromDate).Next(inc);
            yield return month;

            while (true)
            {
                month = month.Next(12);
                yield return month;
            }
        }
    }
}
