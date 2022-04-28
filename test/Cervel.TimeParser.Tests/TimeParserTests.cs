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
        [InlineData("toujours")]
        [InlineData("tjrs")]
        public void TestParse_Always(string input)
        {
            var result = _timeParser.Parse(input);
            Assert.NotNull(result.TimeSpanGenerator);
            Assert.Equal(
                new[] { new TimeSpan(_fromDate, _toDate) },
                result.TimeSpanGenerator.Generate(_fromDate, _toDate));
        }
    }
}
