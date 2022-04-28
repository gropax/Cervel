using System;
using Xunit;

namespace Cervel.TimeParser.Tests
{
    public class TimeParserTests : TestBase
    {
        private TimeParser _timeParser = new TimeParser();
        private DateTime _fromDate = new DateTime(2022, 1, 1);
        private DateTime _toDate = new DateTime(2032, 1, 1);


        [Theory]
        [InlineData("jamais")]
        [InlineData("jam")]
        public void Test_ParseDateTimes_Never(string input)
        {
            var result = _timeParser.ParseDateTimes(input);
            Assert.True(result.IsSuccess);
            Assert.Equal(new DateTime[0],
                result.Value.Generate(_fromDate, _toDate));
        }



        [Theory]
        [InlineData("toujours")]
        [InlineData("tjrs")]
        public void Test_ParseTimeIntervals_Always(string input)
        {
            var result = _timeParser.ParseTimeIntervals(input);
            Assert.True(result.IsSuccess);
            Assert.Equal(
                new[] { new TimeInterval(_fromDate, _toDate) },
                result.Value.Generate(_fromDate, _toDate));
        }

        [Theory]
        [InlineData("jamais")]
        [InlineData("jam")]
        public void Test_ParseTimeIntervals_Never(string input)
        {
            var result = _timeParser.ParseTimeIntervals(input);
            Assert.True(result.IsSuccess);
            Assert.Equal(new TimeInterval[0],
                result.Value.Generate(_fromDate, _toDate));
        }


        [Theory]
        [InlineData("lundi")]
        [InlineData("lu")]
        [InlineData("lun")]
        [InlineData("lundi prochain")]
        [InlineData("lun prochain")]
        [InlineData("lundi pro")]
        public void Test_ParseTimeIntervals_NextMonday(string input)
        {
            var result = _timeParser.ParseTimeIntervals(input);

            Assert.True(result.IsSuccess);
            Assert.Equal(
                Intervals(DayInterval(2022, 1, 3)),
                result.Value.Generate(_fromDate, _toDate));
        }
    }
}
