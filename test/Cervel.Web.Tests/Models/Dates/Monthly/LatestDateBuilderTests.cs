using Cervel.Web.Models;
using System;
using Xunit;

namespace Cervel.Web.Models.Dates.Monthly.Tests
{
    public class LatestDateBuilderTests
    {
        [Fact]
        public void Test_InvalidConstruction()
        {
            Assert.Throws<ArgumentNullException>(() => new LatestDateBuilder());
        }

        [Fact]
        public void Test_CommonCase()
        {
            var builder = new LatestDateBuilder(
                new NthDayBuilder(13),
                new NthDayBuilder(23),
                new NthDayBuilder(31),
                new NthDayBuilder(1));

            Assert.True(builder.TryBuild(2022, Month.January, out var date2022));
            Assert.Equal(new DateTime(2022, 1, 31), date2022);
        }

        [Fact]
        public void Test_NoValidDates()
        {
            var builder = new LatestDateBuilder(
                new NthDayBuilder(29),
                new NthDayOfWeekBuilder(5, DayOfWeek.Friday));

            Assert.False(builder.TryBuild(2022, Month.February, out _));
        }
    }
}
