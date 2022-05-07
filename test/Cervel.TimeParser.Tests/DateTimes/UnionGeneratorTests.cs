using Cervel.TimeParser.Tests;
using System;
using System.Linq;
using Xunit;

namespace Cervel.TimeParser.DateTimes.Tests
{
    public class UnionGeneratorTests : TestBase
    {
        private DateTime _fromDate = new DateTime(2022, 1, 1);
        private DateTime _toDate = new DateTime(2022, 2, 1);

        [Fact]
        public void TestGenerate_WithoutParams()
        {
            var generator = new UnionGenerator(DateGenerators(
                DateGenerator(
                    Day(2022, 1, 1),
                    Day(2022, 1, 5),
                    Day(2022, 1, 10),
                    Day(2022, 1, 15),
                    Day(2022, 1, 20)),
                DateGenerator(
                    Day(2022, 1, 3),
                    Day(2022, 1, 5),
                    Day(2022, 1, 7),
                    Day(2022, 1, 10),
                    Day(2022, 1, 19)),
                DateGenerator(
                    Day(2022, 1, 4),
                    Day(2022, 1, 6),
                    Day(2022, 1, 10),
                    Day(2022, 1, 15),
                    Day(2022, 1, 19))));

            var dates = generator.Generate(_fromDate, _toDate).ToArray();
            Assert.Equal(
                Dates(
                    Day(2022, 1, 1),
                    Day(2022, 1, 3),
                    Day(2022, 1, 4),
                    Day(2022, 1, 5),
                    Day(2022, 1, 6),
                    Day(2022, 1, 7),
                    Day(2022, 1, 10),
                    Day(2022, 1, 15),
                    Day(2022, 1, 19),
                    Day(2022, 1, 20)),
                dates);
        }
    }
}
