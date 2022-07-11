using Cervel.TimeParser.DateTimes;
using Cervel.TimeParser.Tests;
using System;
using System.Linq;
using Xunit;

namespace Cervel.TimeParser.DateTimes.Tests
{
    public class FrequencyGeneratorTests : TestBase
    {
        private DateTime _jan1st2022 = new DateTime(2022, 1, 1);
        private DateTime _jan1st2023 = new DateTime(2023, 1, 1);

        [Fact]
        public void TestGenerate_Monthes()
        {
            IGenerator<Date> generator = new FrequencyGenerator(new MonthMeasure());
            var dates = generator.Generate(_jan1st2022, _jan1st2023).ToArray();

            Assert.Equal(
                Dates(
                    Day(2022, 1, 1),
                    Day(2022, 2, 1),
                    Day(2022, 3, 1),
                    Day(2022, 4, 1),
                    Day(2022, 5, 1),
                    Day(2022, 6, 1),
                    Day(2022, 7, 1),
                    Day(2022, 8, 1),
                    Day(2022, 9, 1),
                    Day(2022, 10, 1),
                    Day(2022, 11, 1),
                    Day(2022, 12, 1)),
                dates);
        }
    }
}
