using System;
using Xunit;

namespace Cervel.TimeParser.Tests
{
    public class TimeParserV2Tests : TestBase
    {
        private TimeParser _timeParser = new TimeParser();
        //private DateTime _fromDate = new DateTime(2022, 1, 1);
        private DateTime _jan1st2022 = new DateTime(2022, 1, 1);
        private DateTime _feb1st2022 = new DateTime(2022, 2, 1);
        private DateTime _now = new DateTime(2022, 1, 1, 10, 30, 0);
        private DateTime _toDate = new DateTime(2032, 1, 1);

        public TimeParserV2Tests()
        {
            Time.SetNow(_now);
        }


        [Theory]
        [InlineData("lundi")]
        //[InlineData("lun")]
        //[InlineData("le lundi")]
        //[InlineData("les lundis")]
        //[InlineData("le lun")]
        //[InlineData("ls lun")]
        //[InlineData("tous les lundis")]
        //[InlineData("ts ls lundis")]
        //[InlineData("chaque lundi")]
        //[InlineData("ch lundi")]
        //[InlineData("lundi de chaque semaine")]
        //[InlineData("le lundi de ch sem")]
        //[InlineData("le lundi chaque semaine")]
        public void TestParse_Monday(string input)
        {
            var dates = _timeParser.ParseDateTimes(input, parserVersion: 2);

            Assert.True(dates.IsSuccess);
            Assert.Equal(
                Dates(
                    Day(2022, 1, 3),
                    Day(2022, 1, 10),
                    Day(2022, 1, 17),
                    Day(2022, 1, 24),
                    Day(2022, 1, 31)),
                dates.Value.Generate(_jan1st2022, _feb1st2022));

            var intervals = _timeParser.ParseTimeIntervals(input, parserVersion: 2);

            Assert.True(intervals.IsSuccess);
            Assert.Equal(
                Intervals(
                    DayInterval(2022, 1, 3),
                    DayInterval(2022, 1, 10),
                    DayInterval(2022, 1, 17),
                    DayInterval(2022, 1, 24),
                    DayInterval(2022, 1, 31)),
                intervals.Value.Generate(_jan1st2022, _feb1st2022));
        }

    }
}
