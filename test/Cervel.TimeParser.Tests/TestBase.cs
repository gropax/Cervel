using Cervel.TimeParser.Dates;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cervel.TimeParser.Tests
{
    public class TestBase
    {
        protected IGenerator<Date>[] DateGenerators(params IGenerator<Date>[] generators) => generators;
        protected IGenerator<Date> DateGenerator(params Date[] dateTimes) =>
            ListGenerator<Date>.Create(dateTimes);

        protected Date[] Dates(params Date[] dateTimes) => dateTimes;
        protected Date Day(int year, int month, int day) => new Date(new DateTime(year, month, day));
        protected Date Day(int year, int month, int day, int hour, int minute, int second) => new Date(new DateTime(year, month, day, hour, minute, second));

        protected TimeInterval[] Intervals(params TimeInterval[] timeSpans) => timeSpans;
        protected TimeInterval Interval(Date start, Date end) => new TimeInterval(start.DateTime, end.DateTime);

        protected TimeInterval DayInterval(int year, int month, int day) => DaysInterval(year, month, day, 1);
        protected TimeInterval<T> MonthInterval<T>(int year, int month, T value) => new TimeInterval<T>(MonthesInterval(year, month, 1), value);

        protected TimeInterval DaysInterval(int year, int month, int day, int dayNumber)
        {
            var start = new DateTime(year, month, day);
            return new TimeInterval(
                start: start,
                end: start + TimeSpan.FromDays(dayNumber));
        }

        protected TimeInterval MonthesInterval(int year, int month, int monthNumber)
        {
            var measure = new MonthMeasure();
            var start = new DateTime(year, month, 1);
            return new TimeInterval(
                start: start,
                end: measure.Shift(new Date(start), 1).DateTime);
        }

        protected TimeInterval DaysInterval(Date first, Date last)
        {
            return new TimeInterval(
                start: first.DateTime,
                end: last.DateTime + TimeSpan.FromDays(1));
        }

        protected IGenerator<TimeInterval> Generator(params TimeInterval[] intervals)
        {
            return new ListGenerator<TimeInterval>(intervals);
        }

        protected TimeInterval<T>[] Intervals<T>(params TimeInterval<T>[] timeSpans) => timeSpans;
        protected TimeInterval<T> Interval<T>(DateTime start, DateTime end, T values) => new TimeInterval<T>(start, end, values);

    }
}
