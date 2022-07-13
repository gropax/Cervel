using Cervel.TimeParser.Extensions;
using Cervel.TimeParser.Tests;
using System;
using System.Linq;
using Xunit;

namespace Cervel.TimeParser.Tests
{
    public class TimeMeasureTests : TestBase
    {
        //[Fact]
        //public void Test_DayMeasure()
        //{
        //    var measure = new DayMeasure();
        //    Assert.Equal(Day(2022, 1, 2), measure.Shift(Day(2022, 1, 1)));
        //    Assert.Equal(Day(2022, 2, 1), measure.Shift(Day(2022, 1, 31)));
        //    Assert.Equal(Day(2023, 1, 1), measure.Shift(Day(2022, 12, 31)));
        //}

        [Fact]
        public void Test_MonthMeasure_Factor1()
        {
            var measure = new MonthMeasure();

            Assert.Equal(Day(2022, 2, 1), measure.Shift(Day(2022, 1, 1), 1));
            Assert.Equal(Day(2022, 3, 1), measure.Shift(Day(2022, 2, 1), 1));
            Assert.Equal(Day(2022, 4, 1), measure.Shift(Day(2022, 3, 1), 1));
            Assert.Equal(Day(2023, 1, 1), measure.Shift(Day(2022, 12, 1), 1));
        }
    }
}
