using Cervel.Web.Models;
using System;
using Xunit;

namespace Cervel.Web.Models.Dates.Monthly.Tests
{
    public class FirstValidDateBuilderTests
    {
        [Fact]
        public void Test_InvalidConstruction()
        {
            Assert.Throws<ArgumentNullException>(() => new FirstValidDateBuilder());
        }

        [Fact]
        public void Test_SomeValidDates()
        {
            var builder = new FirstValidDateBuilder(
                new NthDayBuilder(29),  // invalid
                new NthDayOfWeekBuilder(5, DayOfWeek.Friday),  // invalid
                new NthDayBuilder(23),  // valid
                new NthDayBuilder(1));  // valid

            Assert.True(builder.TryBuild(2022, Month.February, out var date2022));
            Assert.Equal(new DateTime(2022, 2, 23), date2022);
        }

        [Fact]
        public void Test_NoValidDates()
        {
            var builder = new FirstValidDateBuilder(
                new NthDayBuilder(29),
                new NthDayOfWeekBuilder(5, DayOfWeek.Friday));

            Assert.False(builder.TryBuild(2022, Month.February, out _));
        }
    }
}
