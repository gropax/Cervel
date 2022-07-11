using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using System.Threading.Tasks;
using Cervel.TimeParser.Extensions;

namespace Cervel.TimeParser.Dates
{
    [DebuggerDisplay("{Name}")]
    public class DayOfMonthGenerator : IGenerator<Date>
    {
        public string Name { get; }
        private readonly int _number;

        public DayOfMonthGenerator(
            int number,
            string name = null)
        {
            if (number < 1 || number > 31)
                throw new ArgumentOutOfRangeException(nameof(number));

            _number = number;
            Name = name ?? $"DayNumber({number})";
        }

        public IEnumerable<Date> Generate(DateTime fromDate)
        {
            var start = fromDate.Date;
            if (start < fromDate)
                start = start + TimeSpan.FromDays(1);

            if (TryGetDayOfMonth(start.Year, start.Month, _number, out var date) && date >= start)
                yield return new Date(date);

            int inc = 1;
            while (true)
            {
                var nextDate = date.AddMonths(inc);
                if (nextDate.Day == _number)
                {
                    date = nextDate;
                    yield return new Date(date);
                    inc = 1;
                }
                else
                    inc++;
            }

        }

        private bool TryGetDayOfMonth(int year, int month, int day, out DateTime date)
        {
            if (day <= DateTime.DaysInMonth(year, month))
            {
                date = new DateTime(year, month, day);
                return true;
            }
            else
            {
                date = default;
                return false;
            }
        }
    }
}
