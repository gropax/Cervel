using Cervel.Web.Models;
using System;
using Xunit;

namespace Cervel.Web.Models.Dates.Yearly.Tests
{
    public class NthDayOfWeekInMonthBuilderTests
    {
        [Fact]
        public void Test_InvalidDayValue()
        {
            Assert.Throws<Exception>(() => new NthDayOfWeekInMonthBuilder(0, DayOfWeek.Monday, Month.January));
            Assert.Throws<Exception>(() => new NthDayOfWeekInMonthBuilder(6, DayOfWeek.Monday, Month.February));
            Assert.Throws<Exception>(() => new NthDayOfWeekInMonthBuilder(-6, DayOfWeek.Monday, Month.February));
        }

        [Fact]
        public void Test_Positive_First()
        {
            var builder = new NthDayOfWeekInMonthBuilder(1, DayOfWeek.Saturday, Month.January);

            Assert.True(builder.TryBuild(2022, out var date2022));
            Assert.Equal(new DateTime(2022, 1, 1), date2022);

            Assert.True(builder.TryBuild(2023, out var date2023));
            Assert.Equal(new DateTime(2023, 1, 7), date2023);

            Assert.True(builder.TryBuild(2024, out var date2024));
            Assert.Equal(new DateTime(2024, 1, 6), date2024);

            Assert.True(builder.TryBuild(2025, out var date2025));
            Assert.Equal(new DateTime(2025, 1, 4), date2025);

            Assert.True(builder.TryBuild(2026, out var date2026));
            Assert.Equal(new DateTime(2026, 1, 3), date2026);

            Assert.True(builder.TryBuild(2027, out var date2027));
            Assert.Equal(new DateTime(2027, 1, 2), date2027);
        }

        [Fact]
        public void Test_Positive_Second()
        {
            var builder = new NthDayOfWeekInMonthBuilder(2, DayOfWeek.Saturday, Month.January);

            Assert.True(builder.TryBuild(2022, out var date2022));
            Assert.Equal(new DateTime(2022, 1, 8), date2022);

            Assert.True(builder.TryBuild(2023, out var date2023));
            Assert.Equal(new DateTime(2023, 1, 14), date2023);

            Assert.True(builder.TryBuild(2024, out var date2024));
            Assert.Equal(new DateTime(2024, 1, 13), date2024);

            Assert.True(builder.TryBuild(2025, out var date2025));
            Assert.Equal(new DateTime(2025, 1, 11), date2025);

            Assert.True(builder.TryBuild(2026, out var date2026));
            Assert.Equal(new DateTime(2026, 1, 10), date2026);

            Assert.True(builder.TryBuild(2027, out var date2027));
            Assert.Equal(new DateTime(2027, 1, 9), date2027);
        }

        [Fact]
        public void Test_Positive_Fifth()
        {
            var builder = new NthDayOfWeekInMonthBuilder(5, DayOfWeek.Saturday, Month.January);

            Assert.True(builder.TryBuild(2022, out var date2022));
            Assert.Equal(new DateTime(2022, 1, 29), date2022);

            Assert.False(builder.TryBuild(2023, out var date2023));
            Assert.False(builder.TryBuild(2024, out var date2024));
            Assert.False(builder.TryBuild(2025, out var date2025));

            Assert.True(builder.TryBuild(2026, out var date2026));
            Assert.Equal(new DateTime(2026, 1, 31), date2026);

            Assert.True(builder.TryBuild(2027, out var date2027));
            Assert.Equal(new DateTime(2027, 1, 30), date2027);
        }
    }
}
