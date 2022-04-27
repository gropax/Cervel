using Cervel.Web.Models;
using System;
using Xunit;

namespace Cervel.Web.Models.Dates.Tests
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
                new NthDayInMonthBuilder(13, Month.February),
                new NthDayInMonthBuilder(23, Month.March),
                new NthDayInMonthBuilder(31, Month.January),
                new NthDayInMonthBuilder(1, Month.April));

            Assert.True(builder.TryBuild(2022, out var date2022));
            Assert.Equal(new DateTime(2022, 1, 31), date2022);
        }

        [Fact]
        public void Test_NoValidDates()
        {
            var builder = new EarliestDateBuilder(
                new NthDayInMonthBuilder(29, Month.February),
                new NthDayOfWeekInMonthBuilder(5, DayOfWeek.Friday, Month.January));

            Assert.False(builder.TryBuild(2022, out var date2022));
        }
    }
}
