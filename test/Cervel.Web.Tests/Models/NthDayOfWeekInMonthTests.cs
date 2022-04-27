using Cervel.Web.Models;
using System;
using Xunit;

namespace Cervel.Web.Models.Dates.Tests
{
    public class NthDayOfWeekInMonthTests
    {
        [Fact]
        public void Test_InvalidDayValue()
        {
            Assert.Throws<Exception>(() => new NthDayOfWeekInMonth(0, DayOfWeek.Monday, Month.January));
            Assert.Throws<Exception>(() => new NthDayOfWeekInMonth(6, DayOfWeek.Monday, Month.February));
            Assert.Throws<Exception>(() => new NthDayOfWeekInMonth(-6, DayOfWeek.Monday, Month.February));
        }

        [Fact]
        public void Test_Positive_First()
        {
            var descriptor = new NthDayOfWeekInMonth(1, DayOfWeek.Saturday, Month.January);
            Assert.Equal(new DateTime(2022, 1, 1), descriptor.GetDateTime(2022));
            Assert.Equal(new DateTime(2023, 1, 7), descriptor.GetDateTime(2023));
            Assert.Equal(new DateTime(2024, 1, 6), descriptor.GetDateTime(2024));
            Assert.Equal(new DateTime(2025, 1, 4), descriptor.GetDateTime(2025));
            Assert.Equal(new DateTime(2026, 1, 3), descriptor.GetDateTime(2026));
            Assert.Equal(new DateTime(2027, 1, 2), descriptor.GetDateTime(2027));
        }

        [Fact]
        public void Test_Positive_Second()
        {
            var descriptor = new NthDayOfWeekInMonth(2, DayOfWeek.Saturday, Month.January);
            Assert.Equal(new DateTime(2022, 1, 8), descriptor.GetDateTime(2022));
            Assert.Equal(new DateTime(2023, 1, 14), descriptor.GetDateTime(2023));
            Assert.Equal(new DateTime(2024, 1, 13), descriptor.GetDateTime(2024));
            Assert.Equal(new DateTime(2025, 1, 11), descriptor.GetDateTime(2025));
            Assert.Equal(new DateTime(2026, 1, 10), descriptor.GetDateTime(2026));
            Assert.Equal(new DateTime(2027, 1, 9), descriptor.GetDateTime(2027));
        }

        [Fact]
        public void Test_Positive_Fifth()
        {
            var descriptor = new NthDayOfWeekInMonth(5, DayOfWeek.Saturday, Month.January);
            Assert.Equal(new DateTime(2022, 1, 29), descriptor.GetDateTime(2022));
            Assert.Equal(new DateTime(2023, 1, 28), descriptor.GetDateTime(2023));
            Assert.Equal(new DateTime(2024, 1, 27), descriptor.GetDateTime(2024));
            Assert.Equal(new DateTime(2025, 1, 25), descriptor.GetDateTime(2025));
            Assert.Equal(new DateTime(2026, 1, 31), descriptor.GetDateTime(2026));
            Assert.Equal(new DateTime(2027, 1, 30), descriptor.GetDateTime(2027));
        }
    }
}
