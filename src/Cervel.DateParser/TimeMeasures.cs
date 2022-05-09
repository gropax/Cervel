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
        public DayMeasure(int factor = 1)
        {
            _factor = factor;
        }

        public string Name => $"Day({_factor})";

        public DateTime AddTo(DateTime date)
        {
            return date.AddDays(_factor);
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
            return date.AddMonths(_factor);
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
            return date.AddYears(_factor);
        }
    }
}
