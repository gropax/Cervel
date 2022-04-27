using Cervel.Web.Models;
using System;
using Xunit;

namespace Cervel.Web.Models.Dates.Tests
{
    public class NthDayInMonthTests
    {
        [Fact]
        public void Test_InvalidDayValue()
        {
            Assert.Throws<Exception>(() => new NthDayInMonth(0, Month.January));
            Assert.Throws<Exception>(() => new NthDayInMonth(32, Month.January));
            Assert.Throws<Exception>(() => new NthDayInMonth(31, Month.April));
            Assert.Throws<Exception>(() => new NthDayInMonth(30, Month.February));
            Assert.Throws<Exception>(() => new NthDayInMonth(-32, Month.January));
            Assert.Throws<Exception>(() => new NthDayInMonth(-31, Month.April));
            Assert.Throws<Exception>(() => new NthDayInMonth(-30, Month.February));
        }

        [Fact]
        public void Test_PositiveDay_RegularMonth()
        {
            var descriptor = new NthDayInMonth(5, Month.January);

            Assert.True(descriptor.TryBuild(2022, out var date2022));
            Assert.Equal(new DateTime(2022, 1, 5), date2022);

            Assert.True(descriptor.TryBuild(2023, out var date2023));
            Assert.Equal(new DateTime(2023, 1, 5), date2023);

            Assert.True(descriptor.TryBuild(2024, out var date2024));
            Assert.Equal(new DateTime(2024, 1, 5), date2024);
        }

        [Fact]
        public void Test_PositiveDay_February29th()
        {
            var descriptor = new NthDayInMonth(29, Month.February);

            Assert.False(descriptor.TryBuild(2023, out var date2023));

            Assert.True(descriptor.TryBuild(2024, out var date2024));
            Assert.Equal(new DateTime(2024, 2, 29), date2024);

            Assert.False(descriptor.TryBuild(2025, out var date2025));
        }

        [Fact]
        public void Test_NegativeDay_LongMonth()
        {
            var descriptor = new NthDayInMonth(-5, Month.January);

            Assert.True(descriptor.TryBuild(2022, out var date2022));
            Assert.Equal(new DateTime(2022, 1, 27), date2022);

            Assert.True(descriptor.TryBuild(2023, out var date2023));
            Assert.Equal(new DateTime(2023, 1, 27), date2023);

            Assert.True(descriptor.TryBuild(2024, out var date2024));
            Assert.Equal(new DateTime(2024, 1, 27), date2024);
        }

        [Fact]
        public void Test_NegativeDay_ShortMonth()
        {
            var descriptor = new NthDayInMonth(-5, Month.April);

            Assert.True(descriptor.TryBuild(2022, out var date2022));
            Assert.Equal(new DateTime(2022, 4, 26), date2022);

            Assert.True(descriptor.TryBuild(2023, out var date2023));
            Assert.Equal(new DateTime(2023, 4, 26), date2023);

            Assert.True(descriptor.TryBuild(2024, out var date2024));
            Assert.Equal(new DateTime(2024, 4, 26), date2024);
        }

        [Fact]
        public void Test_NegativeDay_February29th()
        {
            var descriptor = new NthDayInMonth(-1, Month.February);

            Assert.True(descriptor.TryBuild(2023, out var date2023));
            Assert.Equal(new DateTime(2023, 2, 28), date2023);

            Assert.True(descriptor.TryBuild(2024, out var date2024));
            Assert.Equal(new DateTime(2024, 2, 29), date2024);

            Assert.True(descriptor.TryBuild(2025, out var date2025));
            Assert.Equal(new DateTime(2025, 2, 28), date2025);
        }

        [Fact]
        public void Test_NegativeDay_February1st()
        {
            var descriptor = new NthDayInMonth(-29, Month.February);

            Assert.False(descriptor.TryBuild(2023, out var date2023));

            Assert.True(descriptor.TryBuild(2024, out var date2024));
            Assert.Equal(new DateTime(2024, 2, 1), date2024);

            Assert.False(descriptor.TryBuild(2025, out var date2025));
        }
    }
}
