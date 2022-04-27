using Cervel.Web.Models;
using System;
using Xunit;

namespace Cervel.Web.Models.Dates.Monthly.Tests
{
    public class NthDayBuilderTests
    {
        [Fact]
        public void Test_InvalidDayValue()
        {
            Assert.Throws<Exception>(() => new NthDayBuilder(0));
            Assert.Throws<Exception>(() => new NthDayBuilder(32));
            Assert.Throws<Exception>(() => new NthDayBuilder(-32));
        }

        [Fact]
        public void Test_PositiveDay_RegularMonth()
        {
            var builder = new NthDayBuilder(5);

            Assert.True(builder.TryBuild(2022, Month.January, out var date2022));
            Assert.Equal(new DateTime(2022, 1, 5), date2022);

            Assert.True(builder.TryBuild(2023, Month.January, out var date2023));
            Assert.Equal(new DateTime(2023, 1, 5), date2023);

            Assert.True(builder.TryBuild(2024, Month.January, out var date2024));
            Assert.Equal(new DateTime(2024, 1, 5), date2024);
        }

        [Fact]
        public void Test_PositiveDay_February29th()
        {
            var builder = new NthDayBuilder(29);
            Assert.False(builder.TryBuild(2023, Month.February, out _));

            Assert.True(builder.TryBuild(2024, Month.February, out var date2024));
            Assert.Equal(new DateTime(2024, 2, 29), date2024);

            Assert.False(builder.TryBuild(2025, Month.February, out _));
        }

        [Fact]
        public void Test_NegativeDay_LongMonth()
        {
            var builder = new NthDayBuilder(-5);

            Assert.True(builder.TryBuild(2022, Month.January, out var date2022));
            Assert.Equal(new DateTime(2022, 1, 27), date2022);

            Assert.True(builder.TryBuild(2023, Month.January, out var date2023));
            Assert.Equal(new DateTime(2023, 1, 27), date2023);

            Assert.True(builder.TryBuild(2024, Month.January, out var date2024));
            Assert.Equal(new DateTime(2024, 1, 27), date2024);
        }

        [Fact]
        public void Test_NegativeDay_ShortMonth()
        {
            var builder = new NthDayBuilder(-5);

            Assert.True(builder.TryBuild(2022, Month.April, out var date2022));
            Assert.Equal(new DateTime(2022, 4, 26), date2022);

            Assert.True(builder.TryBuild(2023, Month.April, out var date2023));
            Assert.Equal(new DateTime(2023, 4, 26), date2023);

            Assert.True(builder.TryBuild(2024, Month.April, out var date2024));
            Assert.Equal(new DateTime(2024, 4, 26), date2024);
        }

        [Fact]
        public void Test_NegativeDay_February29th()
        {
            var builder = new NthDayBuilder(-1);

            Assert.True(builder.TryBuild(2023, Month.February, out var date2023));
            Assert.Equal(new DateTime(2023, 2, 28), date2023);

            Assert.True(builder.TryBuild(2024, Month.February, out var date2024));
            Assert.Equal(new DateTime(2024, 2, 29), date2024);

            Assert.True(builder.TryBuild(2025, Month.February, out var date2025));
            Assert.Equal(new DateTime(2025, 2, 28), date2025);
        }

        [Fact]
        public void Test_NegativeDay_February1st()
        {
            var builder = new NthDayBuilder(-29);

            Assert.False(builder.TryBuild(2023, Month.February, out _));

            Assert.True(builder.TryBuild(2024, Month.February, out var date2024));
            Assert.Equal(new DateTime(2024, 2, 1), date2024);

            Assert.False(builder.TryBuild(2025, Month.February, out _));
        }
    }
}
