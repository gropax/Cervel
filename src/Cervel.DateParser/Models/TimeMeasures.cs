using Cervel.TimeParser.Dates;
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
        Date AddTo(Date date);
    }

    public class DayMeasure : ITimeMeasure
    {
        private int _factor;
        public DayMeasure(int factor = 1)
        {
            _factor = factor;
        }

        public string Name => $"Day({_factor})";

        public Date AddTo(Date date)
        {
            return new Date(date.DateTime.AddDays(_factor));
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

        public Date AddTo(Date date)
        {
            return new Date(date.DateTime.AddMonths(_factor));
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

        public Date AddTo(Date date)
        {
            return new Date(date.DateTime.AddYears(_factor));
        }
    }
}
