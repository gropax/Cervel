using System;
using Xunit;

namespace Cervel.TimeParser.Tests
{
    public class TimeParserTests
    {
        private TimeParser _timeParser = new TimeParser();
        private DateTime _fromDate = new DateTime(2000, 1, 1);
        private DateTime _toDate = new DateTime(2010, 1, 1);


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
        public void Test_ParseTimeSpans_Always(string input)
        {
            var result = _timeParser.ParseTimeSpans(input);
            Assert.True(result.IsSuccess);
            Assert.Equal(
                new[] { new TimeSpan(_fromDate, _toDate) },
                result.Value.Generate(_fromDate, _toDate));
        }

        [Theory]
        [InlineData("jamais")]
        [InlineData("jam")]
        public void Test_ParseTimeSpans_Never(string input)
        {
            var result = _timeParser.ParseTimeSpans(input);
            Assert.True(result.IsSuccess);
            Assert.Equal(new TimeSpan[0],
                result.Value.Generate(_fromDate, _toDate));
        }
    }
}
