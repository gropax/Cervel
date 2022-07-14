using Cervel.TimeParser.Dates;
using Cervel.TimeParser.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cervel.TimeParser
{
    public interface ITimeMeasure<T> where T : ITimeUnit<T>
    {
        string Name { get; }
        DateTime Shift(DateTime date, int factor);
        Date Shift(Date date, int factor);
        T Shift(T date, int factor);
        T GetUnit(DateTime dateTime);
        T GetUnit(Date date) => GetUnit(date.DateTime);
    }

    public static class TimeMeasures
    {
        public static DayMeasure Day = new DayMeasure();
        public static MonthMeasure Month = new MonthMeasure();
        public static YearMeasure Year = new YearMeasure();
    }

    public class DayMeasure : ITimeMeasure<Day>
    {
        public string Name => "Day";

        public Day GetUnit(DateTime dateTime)
        {
            return new Day(dateTime.Date);
        }

        public DateTime Shift(DateTime date, int factor) => date.AddDays(factor);
        public Date Shift(Date date, int factor) => new Date(date.DateTime.AddDays(factor));

        public Day Shift(Day day, int factor)
        {
            return new Day(day.Start.AddDays(factor));
        }
    }

    public class MonthMeasure : ITimeMeasure<Month>
    {
        public string Name => "Month";

        public Month GetUnit(DateTime dateTime)
        {
            return new Month(dateTime.Year, dateTime.Month);
        }

        public DateTime Shift(DateTime date, int factor) => date.AddMonths(factor);
        public Date Shift(Date date, int factor) => new Date(date.DateTime.AddMonths(factor));

        public Month Shift(Month month, int factor)
        {
            return new Month(month.Start.AddMonths(factor));
        }
    }

    public class YearMeasure : ITimeMeasure<Year>
    {
        public string Name => "Year";

        public Year GetUnit(DateTime dateTime)
        {
            return new Year(dateTime.Year);
        }

        public DateTime Shift(DateTime date, int factor) => date.AddYears(factor);
        public Date Shift(Date date, int factor) => new Date(date.DateTime.AddYears(factor));

        public Year Shift(Year month, int factor)
        {
            return new Year(month.Start.AddYears(factor));
        }
    }
}
