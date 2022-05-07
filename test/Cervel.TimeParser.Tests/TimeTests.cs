using Cervel.TimeParser.Tests;
using System;
using System.Linq;
using Xunit;

namespace Cervel.TimeParser.Tests
{
    public class TimeTests : TestBase
    {
        private DateTime _jan1st2022 = new DateTime(2022, 1, 1);
        private DateTime _jan1st2023 = new DateTime(2023, 1, 1);
        private DateTime _jan1st2027 = new DateTime(2027, 1, 1);
        private DateTime _feb1st2022 = new DateTime(2022, 2, 1);

        [Fact]
        public void TestShift_Positive()
        {
            var generator = Time.Each(Month.March);
            var dates = generator.Generate(_jan1st2022, _jan1st2027).ToArray();
            Assert.Equal(
                Dates(
                    Day(2022, 3, 1),
                    Day(2023, 3, 1),
                    Day(2024, 3, 1),
                    Day(2025, 3, 1),
                    Day(2026, 3, 1)),
                dates);

        }
    }
}
