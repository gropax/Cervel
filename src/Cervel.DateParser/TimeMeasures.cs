using Cervel.TimeParser.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cervel.TimeParser
{
    public interface ITimeMeasure
    {
        string Name { get; }
        DateTime AddTo(DateTime date);
    }

    public class DayMeasure : ITimeMeasure
    {
        private int _factor;
        private TimeSpan _timeSpan;
        public DayMeasure(int factor = 1)
        {
            _factor = factor;
            _timeSpan = TimeSpan.FromDays(factor);
        }

        public string Name => $"Day({_factor})";

        public DateTime AddTo(DateTime date)
        {
            return date.Shift(_timeSpan);
        }
    }

    public class MonthMeasure : ITimeMeasure
    {
        private int _factor;
        public MonthMeasure(int factor = 1)
        {
            _factor = factor;
        }

        public string Name => $"Month({_factor})";

        public DateTime AddTo(DateTime date)
        {
            int virtMonth = date.Month + _factor;
            int year = date.Year + (virtMonth - 1) / 12;
            int month = (virtMonth - 1) % 12 + 1;
            var daysInMonth = DateTime.DaysInMonth(year, month);
            return new DateTime(year, month, Math.Min(date.Day, daysInMonth));
        }
    }

    public class YearMeasure : ITimeMeasure
    {
        private int _factor;
        public YearMeasure(int factor = 1)
        {
            _factor = factor;
        }

        public string Name => $"Year({_factor})";

        public DateTime AddTo(DateTime date)
        {
            int year = date.Year + _factor;
            int month = date.Month;
            var daysInMonth = DateTime.DaysInMonth(year, month);
            return new DateTime(year, month, Math.Min(date.Day, daysInMonth));
        }
    }
}
