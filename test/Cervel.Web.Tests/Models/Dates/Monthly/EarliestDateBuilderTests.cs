using Cervel.Web.Models;
using System;
using Xunit;

namespace Cervel.Web.Models.Dates.Monthly.Tests
{
    public class EarliestDateBuilderTests
    {
        [Fact]
        public void Test_InvalidConstruction()
        {
            Assert.Throws<ArgumentNullException>(() => new EarliestDateBuilder());
        }

        [Fact]
        public void Test_CommonCase()
        {
            var builder = new EarliestDateBuilder(
                new NthDayBuilder(13),
                new NthDayBuilder(23),
                new NthDayBuilder(3),
                new NthDayBuilder(31));

            Assert.True(builder.TryBuild(2022, Month.January, out var date2022));
            Assert.Equal(new DateTime(2022, 1, 3), date2022);
        }

        [Fact]
        public void Test_NoValidDates()
        {
            var builder = new EarliestDateBuilder(
                new NthDayBuilder(29),
                new NthDayOfWeekBuilder(5, DayOfWeek.Friday));

            Assert.False(builder.TryBuild(2022, Month.February, out _));
        }
    }
}
