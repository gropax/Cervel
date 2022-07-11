using Cervel.TimeParser.Tests;
using System;
using System.Linq;
using Xunit;

namespace Cervel.TimeParser.Dates.Tests
{
    public class DayFilterGeneratorTests : TestBase
    {
        private IGenerator<Date> _generator;
        private DateTime _fromDate = new DateTime(2022, 1, 1);
        private DateTime _toDate = new DateTime(2022, 2, 1);

        [Fact]
        public void TestGenerate_WithoutParams()
        {
            _generator = new DayFilterGenerator();

            var dates = _generator.Generate(_fromDate, _toDate).ToArray();
            Assert.Equal(31, dates.Length);

            Assert.Equal(
                Dates(Day(2022, 1, 1), Day(2022, 1, 2), Day(2022, 1, 3), Day(2022, 1, 4), Day(2022, 1, 5)),
                dates.Take(5));
        }

        [Fact]
        public void TestGenerate_WithDateTimeSelector()
        {
            _generator = new DayFilterGenerator(dateTimeSelector: (dt) => dt.DayOfWeek == DayOfWeek.Monday);

            Assert.Equal(
                Dates(Day(2022, 1, 3), Day(2022, 1, 10), Day(2022, 1, 17), Day(2022, 1, 24), Day(2022, 1, 31)),
                _generator.Generate(_fromDate, _toDate));
        }

        [Fact]
        public void TestGenerate_WithIndexSelector()
        {
            _generator = new DayFilterGenerator(
                dateTimeSelector: (dt) => dt.DayOfWeek == DayOfWeek.Monday,
                indexSelector: (i) => i % 2 == 0);

            Assert.Equal(
                Dates(Day(2022, 1, 3), Day(2022, 1, 17), Day(2022, 1, 31)),
                _generator.Generate(_fromDate, _toDate));
        }

        [Fact]
        public void TestGenerate_WithSkip()
        {
            _generator = new DayFilterGenerator(
                dateTimeSelector: (dt) => dt.DayOfWeek == DayOfWeek.Monday,
                skip: 2);

            Assert.Equal(
                Dates(Day(2022, 1, 17), Day(2022, 1, 24), Day(2022, 1, 31)),
                _generator.Generate(_fromDate, _toDate));
        }

        [Fact]
        public void TestGenerate_WithTake()
        {
            _generator = new DayFilterGenerator(
                dateTimeSelector: (dt) => dt.DayOfWeek == DayOfWeek.Monday,
                take: 3);

            Assert.Equal(
                Dates(Day(2022, 1, 3), Day(2022, 1, 10), Day(2022, 1, 17)),
                _generator.Generate(_fromDate, _toDate));
        }
    }
}
