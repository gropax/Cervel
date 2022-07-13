using Cervel.TimeParser.Tests;
using System;
using System.Linq;
using Xunit;

namespace Cervel.TimeParser.TimeIntervals.Tests
{
    public class QuantizeGeneratorTests : TestBase
    {
        private DateTime _fromDate = new DateTime(2022, 1, 1);

        [Fact]
        public void TestGenerate()
        {
            var generator = Generator(
                DaysInterval(2022, 3, 5, dayNumber: 5),
                DaysInterval(2022, 3, 15, dayNumber: 5),
                DaysInterval(2022, 3, 25, dayNumber: 10),
                DaysInterval(2022, 4, 25, dayNumber: 40),
                DaysInterval(2022, 6, 15, dayNumber: 5));

            var quantizeGen = new QuantizeGenerator<Month>(new MonthMeasure(), generator);
            var quantized = quantizeGen.Generate(_fromDate).Take(8).ToArray();

            Assert.Equal(
                Intervals(
                    MonthInterval(2022, 1, value: 0.0),
                    MonthInterval(2022, 2, value: 0.0),
                    MonthInterval(2022, 3, value: 17.0 / 31),
                    MonthInterval(2022, 4, value: 9.0 / 30),
                    MonthInterval(2022, 5, value: 1.0),
                    MonthInterval(2022, 6, value: 8.0 / 30),
                    MonthInterval(2022, 7, value: 0.0),
                    MonthInterval(2022, 8, value: 0.0)),
                quantized);
        }
    }
}
