using Cervel.TimeParser.DateTimes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cervel.TimeParser.Tests
{
    public class TestBase
    {
        protected IGenerator<DateTime> DateGenerator(params DateTime[] dateTimes) =>
            ListGenerator.Create(dateTimes);

        protected DateTime[] Dates(params DateTime[] dateTimes) => dateTimes;
        protected DateTime Day(int year, int month, int day) => new DateTime(year, month, day);
        protected DateTime Day(int year, int month, int day, int hour, int minute, int second) => new DateTime(year, month, day, hour, minute, second);

        protected TimeInterval[] Intervals(params TimeInterval[] timeSpans) => timeSpans;

        protected TimeInterval DayInterval(int year, int month, int day) => DaysInterval(year, month, day, 1);

        protected TimeInterval DaysInterval(int year, int month, int day, int dayNumber)
        {
            var start = new DateTime(year, month, day);
            return new TimeInterval(
                start: start,
                end: start + TimeSpan.FromDays(dayNumber));
        }

        protected TimeInterval DaysInterval(DateTime first, DateTime last)
        {
            return new TimeInterval(
                start: first,
                end: last + TimeSpan.FromDays(1));
        }
    }
}
