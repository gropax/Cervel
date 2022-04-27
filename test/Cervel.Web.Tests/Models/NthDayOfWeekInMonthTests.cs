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

            Assert.True(descriptor.TryBuild(2022, out var date2022));
            Assert.Equal(new DateTime(2022, 1, 1), date2022);

            Assert.True(descriptor.TryBuild(2023, out var date2023));
            Assert.Equal(new DateTime(2023, 1, 7), date2023);

            Assert.True(descriptor.TryBuild(2024, out var date2024));
            Assert.Equal(new DateTime(2024, 1, 6), date2024);

            Assert.True(descriptor.TryBuild(2025, out var date2025));
            Assert.Equal(new DateTime(2025, 1, 4), date2025);

            Assert.True(descriptor.TryBuild(2026, out var date2026));
            Assert.Equal(new DateTime(2026, 1, 3), date2026);

            Assert.True(descriptor.TryBuild(2027, out var date2027));
            Assert.Equal(new DateTime(2027, 1, 2), date2027);
        }

        [Fact]
        public void Test_Positive_Second()
        {
            var descriptor = new NthDayOfWeekInMonth(2, DayOfWeek.Saturday, Month.January);

            Assert.True(descriptor.TryBuild(2022, out var date2022));
            Assert.Equal(new DateTime(2022, 1, 8), date2022);

            Assert.True(descriptor.TryBuild(2023, out var date2023));
            Assert.Equal(new DateTime(2023, 1, 14), date2023);

            Assert.True(descriptor.TryBuild(2024, out var date2024));
            Assert.Equal(new DateTime(2024, 1, 13), date2024);

            Assert.True(descriptor.TryBuild(2025, out var date2025));
            Assert.Equal(new DateTime(2025, 1, 11), date2025);

            Assert.True(descriptor.TryBuild(2026, out var date2026));
            Assert.Equal(new DateTime(2026, 1, 10), date2026);

            Assert.True(descriptor.TryBuild(2027, out var date2027));
            Assert.Equal(new DateTime(2027, 1, 9), date2027);
        }

        [Fact]
        public void Test_Positive_Fifth()
        {
            var descriptor = new NthDayOfWeekInMonth(5, DayOfWeek.Saturday, Month.January);

            Assert.True(descriptor.TryBuild(2022, out var date2022));
            Assert.Equal(new DateTime(2022, 1, 29), date2022);

            Assert.False(descriptor.TryBuild(2023, out var date2023));
            Assert.False(descriptor.TryBuild(2024, out var date2024));
            Assert.False(descriptor.TryBuild(2025, out var date2025));

            Assert.True(descriptor.TryBuild(2026, out var date2026));
            Assert.Equal(new DateTime(2026, 1, 31), date2026);

            Assert.True(descriptor.TryBuild(2027, out var date2027));
            Assert.Equal(new DateTime(2027, 1, 30), date2027);
        }
    }
}
