using Cervel.Web.Models;
using System;
using Xunit;

namespace Cervel.Web.Models.Dates.Tests
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
                new NthDayInMonthBuilder(29, Month.February),  // invalid
                new NthDayOfWeekInMonthBuilder(5, DayOfWeek.Friday, Month.January),  // invalid
                new NthDayInMonthBuilder(2, Month.April),  // valid
                new NthDayInMonthBuilder(1, Month.January));  // valid

            Assert.True(builder.TryBuild(2022, out var date2022));
            Assert.Equal(new DateTime(2022, 4, 2), date2022);
        }

        [Fact]
        public void Test_NoValidDates()
        {
            var builder = new FirstValidDateBuilder(
                new NthDayInMonthBuilder(29, Month.February),
                new NthDayOfWeekInMonthBuilder(5, DayOfWeek.Friday, Month.January));  // valid

            Assert.False(builder.TryBuild(2022, out var date2022));
        }
    }
}
