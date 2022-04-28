using Cervel.TimeParser.Tests;
using System;
using System.Linq;
using Xunit;

namespace Cervel.TimeParser.TimeIntervals.Tests
{
    public class DayFilterGeneratorTests : TestBase
    {
        private ITimeIntervalGenerator _generator;
        private DateTime _fromDate = new DateTime(2022, 1, 1);
        private DateTime _toDate = new DateTime(2022, 2, 1);

        [Fact]
        public void TestGenerate_WithoutParams()
        {
            _generator = new DayFilterGenerator(TimeSpan.FromDays(1));

            Assert.Equal(
                Intervals(DaysInterval(Day(2022, 1, 1), Day(2022, 1, 31))),
                _generator.Generate(_fromDate, _toDate));
        }

        //[Fact]
        //public void TestGenerate_WithDateTimeSelector()
        //{
        //    _generator = new DayFilterGenerator(dateTimeSelector: (dt) => dt.DayOfWeek == DayOfWeek.Monday);

        //    Assert.Equal(
        //        Dates(Day(2022, 1, 3), Day(2022, 1, 10), Day(2022, 1, 17), Day(2022, 1, 24), Day(2022, 1, 31)),
        //        _generator.Generate(_fromDate, _toDate));
        //}

        //[Fact]
        //public void TestGenerate_WithIndexSelector()
        //{
        //    _generator = new DayFilterGenerator(
        //        dateTimeSelector: (dt) => dt.DayOfWeek == DayOfWeek.Monday,
        //        indexSelector: (i) => i % 2 == 0);

        //    Assert.Equal(
        //        Dates(Day(2022, 1, 3), Day(2022, 1, 17), Day(2022, 1, 31)),
        //        _generator.Generate(_fromDate, _toDate));
        //}

        //[Fact]
        //public void TestGenerate_WithSkip()
        //{
        //    _generator = new DayFilterGenerator(
        //        dateTimeSelector: (dt) => dt.DayOfWeek == DayOfWeek.Monday,
        //        skip: 2);

        //    Assert.Equal(
        //        Dates(Day(2022, 1, 17), Day(2022, 1, 24), Day(2022, 1, 31)),
        //        _generator.Generate(_fromDate, _toDate));
        //}

        //[Fact]
        //public void TestGenerate_WithTake()
        //{
        //    _generator = new DayFilterGenerator(
        //        dateTimeSelector: (dt) => dt.DayOfWeek == DayOfWeek.Monday,
        //        take: 3);

        //    Assert.Equal(
        //        Dates(Day(2022, 1, 3), Day(2022, 1, 10), Day(2022, 1, 17)),
        //        _generator.Generate(_fromDate, _toDate));
        //}
    }
}
