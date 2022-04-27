using Cervel.Web.Models;
using System;
using Xunit;

namespace Cervel.Web.Tests
{
    public class NthInMonthDescriptorTests
    {
        [Fact]
        public void Test_InvalidDayValue()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => new NthInMonthDescriptor(0, Month.January));
            Assert.Throws<ArgumentOutOfRangeException>(() => new NthInMonthDescriptor(32, Month.January));
            Assert.Throws<ArgumentOutOfRangeException>(() => new NthInMonthDescriptor(31, Month.April));
            Assert.Throws<ArgumentOutOfRangeException>(() => new NthInMonthDescriptor(30, Month.February));
            Assert.Throws<ArgumentOutOfRangeException>(() => new NthInMonthDescriptor(-32, Month.January));
            Assert.Throws<ArgumentOutOfRangeException>(() => new NthInMonthDescriptor(-31, Month.April));
            Assert.Throws<ArgumentOutOfRangeException>(() => new NthInMonthDescriptor(-30, Month.February));
        }

        [Fact]
        public void Test_PositiveDay_RegularMonth()
        {
            var descriptor = new NthInMonthDescriptor(5, Month.January);
            Assert.Equal(new DateTime(2022, 1, 5), descriptor.GetDateTime(2022));
            Assert.Equal(new DateTime(2023, 1, 5), descriptor.GetDateTime(2023));
            Assert.Equal(new DateTime(2024, 1, 5), descriptor.GetDateTime(2024));
        }

        [Fact]
        public void Test_PositiveDay_February29th()
        {
            var descriptor = new NthInMonthDescriptor(29, Month.February);
            Assert.Equal(new DateTime(2023, 2, 28), descriptor.GetDateTime(2023));
            Assert.Equal(new DateTime(2024, 2, 29), descriptor.GetDateTime(2024));
            Assert.Equal(new DateTime(2025, 2, 28), descriptor.GetDateTime(2025));
        }

        [Fact]
        public void Test_NegativeDay_LongMonth()
        {
            var descriptor = new NthInMonthDescriptor(-5, Month.January);
            Assert.Equal(new DateTime(2022, 1, 27), descriptor.GetDateTime(2022));
            Assert.Equal(new DateTime(2023, 1, 27), descriptor.GetDateTime(2023));
            Assert.Equal(new DateTime(2024, 1, 27), descriptor.GetDateTime(2024));
        }

        [Fact]
        public void Test_NegativeDay_ShortMonth()
        {
            var descriptor = new NthInMonthDescriptor(-5, Month.April);
            Assert.Equal(new DateTime(2022, 4, 26), descriptor.GetDateTime(2022));
            Assert.Equal(new DateTime(2023, 4, 26), descriptor.GetDateTime(2023));
            Assert.Equal(new DateTime(2024, 4, 26), descriptor.GetDateTime(2024));
        }

        [Fact]
        public void Test_NegativeDay_February29th()
        {
            var descriptor = new NthInMonthDescriptor(-1, Month.February);
            Assert.Equal(new DateTime(2023, 2, 28), descriptor.GetDateTime(2023));
            Assert.Equal(new DateTime(2024, 2, 29), descriptor.GetDateTime(2024));
            Assert.Equal(new DateTime(2025, 2, 28), descriptor.GetDateTime(2025));
        }
    }
}
