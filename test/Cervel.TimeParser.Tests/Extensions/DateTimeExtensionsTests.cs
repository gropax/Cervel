using Cervel.TimeParser.Tests;
using System;
using System.Linq;
using Xunit;

namespace Cervel.TimeParser.Extensions.Tests
{
    public class DateTimeExtensionsTests : TestBase
    {
        [Fact]
        public void TestShift_Positive()
        {
            Assert.Equal(Day(2022, 1, 4), Day(2022, 1, 1).Shift(TimeSpan.FromDays(3)));
        }

        [Fact]
        public void TestShift_Negative()
        {
            Assert.Equal(Day(2022, 1, 2), Day(2022, 1, 4).Shift(TimeSpan.FromDays(-2)));
        }

        [Fact]
        public void TestShift_PositiveOverflow()
        {
            Assert.Equal(DateTime.MaxValue, DateTime.MaxValue.Shift(TimeSpan.FromDays(3)));
        }

        [Fact]
        public void TestShift_NegativeOverflow()
        {
            Assert.Equal(DateTime.MinValue, DateTime.MinValue.Shift(TimeSpan.FromDays(-3)));
        }

        [Fact]
        public void TestToInterval()
        {
            Assert.Equal(DayInterval(2022, 1, 31), Day(2022, 1, 31).ToInterval(TimeSpan.FromDays(1)));
        }
    }
}
