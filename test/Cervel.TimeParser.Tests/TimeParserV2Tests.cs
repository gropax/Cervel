using System;
using System.Linq;
using Xunit;

namespace Cervel.TimeParser.Tests
{
    public class TimeParserV2Tests : TestBase
    {
        private TimeParser _timeParser = new TimeParser();
        //private DateTime _fromDate = new DateTime(2022, 1, 1);
        private DateTime _jan1st2022 = new DateTime(2022, 1, 1);
        private DateTime _jan1st2023 = new DateTime(2023, 1, 1);
        private DateTime _feb1st2022 = new DateTime(2022, 2, 1);
        private DateTime _now = new DateTime(2022, 1, 1, 10, 30, 0);

        public TimeParserV2Tests()
        {
            Time.SetNow(_now);
        }


        [Theory]
        [InlineData("chaque jour")]
        [InlineData("tous les jours")]
        //[InlineData("((chaque jour))")]
        public void TestParse_EveryDay(string input)
        {
            var dateParseResult = _timeParser.ParseDateTimes(input, parserVersion: 2);
            Assert.True(dateParseResult.IsSuccess);

            var dates = dateParseResult.Value.Generate(_jan1st2022, _feb1st2022).ToArray();
            Assert.Equal(31, dates.Count());
            Assert.Equal(
                Dates(
                    Day(2022, 1, 1),
                    Day(2022, 1, 2),
                    Day(2022, 1, 3),
                    Day(2022, 1, 4),
                    Day(2022, 1, 5)),
                dates.Take(5));

            var intervalParseResult = _timeParser.ParseTimeIntervals(input, parserVersion: 2);
            Assert.True(intervalParseResult.IsSuccess);

            var intervals = intervalParseResult.Value.Generate(_jan1st2022, _feb1st2022);
            Assert.Equal(
                Intervals(
                    DaysInterval(2022, 1, 1, dayNumber: 31)),
                intervals);
        }

        [Theory]
        [InlineData("lundi")]
        //[InlineData("(((lundi)))")]
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
            var dateParseResult = _timeParser.ParseDateTimes(input, parserVersion: 2);
            Assert.True(dateParseResult.IsSuccess);

            var dates = dateParseResult.Value.Generate(_jan1st2022, _feb1st2022).ToArray();
            Assert.Equal(
                Dates(
                    Day(2022, 1, 3),
                    Day(2022, 1, 10),
                    Day(2022, 1, 17),
                    Day(2022, 1, 24),
                    Day(2022, 1, 31)),
                dates);

            var intervalParseResult = _timeParser.ParseTimeIntervals(input, parserVersion: 2);
            Assert.True(intervalParseResult.IsSuccess);

            var intervals = intervalParseResult.Value.Generate(_jan1st2022, _feb1st2022);
            Assert.Equal(
                Intervals(
                    DayInterval(2022, 1, 3),
                    DayInterval(2022, 1, 10),
                    DayInterval(2022, 1, 17),
                    DayInterval(2022, 1, 24),
                    DayInterval(2022, 1, 31)),
                intervals);
        }


        [Theory]
        [InlineData("lundi, mardi et vendredi")]
        //[InlineData("((lundi, mardi et vendredi))")]
        public void TestParse_MultipleDaysOfWeek(string input)
        {
            var dateParseResult = _timeParser.ParseDateTimes(input, parserVersion: 2);
            Assert.True(dateParseResult.IsSuccess);

            var dates = dateParseResult.Value.Generate(_jan1st2022, _feb1st2022).ToArray();
            Assert.Equal(13, dates.Count());
            Assert.Equal(
                Dates(
                    Day(2022, 1, 3),
                    Day(2022, 1, 4),
                    Day(2022, 1, 7),
                    Day(2022, 1, 10),
                    Day(2022, 1, 11),
                    Day(2022, 1, 14),
                    Day(2022, 1, 17),
                    Day(2022, 1, 18),
                    Day(2022, 1, 21),
                    Day(2022, 1, 24),
                    Day(2022, 1, 25),
                    Day(2022, 1, 28),
                    Day(2022, 1, 31)),
                dates);

            var intervalParseResult = _timeParser.ParseTimeIntervals(input, parserVersion: 2);
            Assert.True(intervalParseResult.IsSuccess);

            var intervals = intervalParseResult.Value.Generate(_jan1st2022, _feb1st2022);
            Assert.Equal(
                Intervals(
                    DaysInterval(2022, 1, 3, dayNumber: 2),
                    DayInterval(2022, 1, 7),
                    DaysInterval(2022, 1, 10, dayNumber: 2),
                    DayInterval(2022, 1, 14),
                    DaysInterval(2022, 1, 17, dayNumber: 2),
                    DayInterval(2022, 1, 21),
                    DaysInterval(2022, 1, 24, dayNumber: 2),
                    DayInterval(2022, 1, 28),
                    DayInterval(2022, 1, 31)),
                intervals);
        }

        [Theory]
        [InlineData("chaque jour à partir de jeudi")]
        //[InlineData("(chaque jour (à partir de (jeudi)))")]
        public void TestParse_EveryDayFrom(string input)
        {
            var dateParseResult = _timeParser.ParseDateTimes(input, parserVersion: 2);
            Assert.True(dateParseResult.IsSuccess);

            var dates = dateParseResult.Value.Generate(_jan1st2022, _feb1st2022).ToArray();
            Assert.Equal(26, dates.Length);
            Assert.Equal(
                Dates(
                    Day(2022, 1, 6),
                    Day(2022, 1, 7),
                    Day(2022, 1, 8),
                    Day(2022, 1, 9),
                    Day(2022, 1, 10)),
                dates.Take(5));

            var intervalParseResult = _timeParser.ParseTimeIntervals(input, parserVersion: 2);
            Assert.True(intervalParseResult.IsSuccess);

            var intervals = intervalParseResult.Value.Generate(_jan1st2022, _feb1st2022);
            Assert.Equal(
                Intervals(
                    DaysInterval(2022, 1, 6, dayNumber: 26)),
                intervals);
        }

        [Theory]
        [InlineData("chaque jour jusqu a vendredi")]
        public void TestParse_EveryDayUntil(string input)
        {
            var dateParseResult = _timeParser.ParseDateTimes(input, parserVersion: 2);
            Assert.True(dateParseResult.IsSuccess);

            var dates = dateParseResult.Value.Generate(_jan1st2022, _feb1st2022).ToArray();
            Assert.Equal(6, dates.Length);
            Assert.Equal(
                Dates(
                    Day(2022, 1, 1),
                    Day(2022, 1, 2),
                    Day(2022, 1, 3),
                    Day(2022, 1, 4),
                    Day(2022, 1, 5),
                    Day(2022, 1, 6)),
                dates);

            var intervalParseResult = _timeParser.ParseTimeIntervals(input, parserVersion: 2);
            Assert.True(intervalParseResult.IsSuccess);

            var intervals = intervalParseResult.Value.Generate(_jan1st2022, _feb1st2022);
            Assert.Equal(
                Intervals(
                    DaysInterval(2022, 1, 1, dayNumber: 6)),
                intervals);
        }



        [Theory]
        [InlineData("mars")]
        public void TestParse_March(string input)
        {
            var dateParseResult = _timeParser.ParseDateTimes(input, parserVersion: 2);
            Assert.True(dateParseResult.IsSuccess);

            var dates = dateParseResult.Value.Generate(_jan1st2022, _jan1st2023).ToArray();
            Assert.Equal(
                Dates(
                    Day(2022, 3, 1)),
                dates);

            var intervalParseResult = _timeParser.ParseTimeIntervals(input, parserVersion: 2);
            Assert.True(intervalParseResult.IsSuccess);

            var intervals = intervalParseResult.Value.Generate(_jan1st2022, _jan1st2023);
            Assert.Equal(
                Intervals(
                    DaysInterval(2022, 3, 1, dayNumber: 31)),
                intervals);
        }
    }
}
