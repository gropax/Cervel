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
        private DateTime _jan1st2027 = new DateTime(2027, 1, 1);
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

            var intervals = intervalParseResult.Value.Generate(_jan1st2022, _feb1st2022).ToArray();
            Assert.Equal(
                Intervals(
                    DaysInterval(2022, 1, 1, dayNumber: 31)),
                intervals);
        }

        [Theory]
        [InlineData("lundi")]
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

            var intervals = intervalParseResult.Value.Generate(_jan1st2022, _feb1st2022).ToArray();
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
        [InlineData("mardi")]
        public void TestParse_Tuesday(string input)
        {
            var dateParseResult = _timeParser.ParseDateTimes(input, parserVersion: 2);
            Assert.True(dateParseResult.IsSuccess);

            var dates = dateParseResult.Value.Generate(_jan1st2022, _feb1st2022).ToArray();
            Assert.Equal(
                Dates(
                    Day(2022, 1, 4),
                    Day(2022, 1, 11),
                    Day(2022, 1, 18),
                    Day(2022, 1, 25)),
                dates);

            var intervalParseResult = _timeParser.ParseTimeIntervals(input, parserVersion: 2);
            Assert.True(intervalParseResult.IsSuccess);

            var intervals = intervalParseResult.Value.Generate(_jan1st2022, _feb1st2022).ToArray();
            Assert.Equal(
                Intervals(
                    DayInterval(2022, 1, 4),
                    DayInterval(2022, 1, 11),
                    DayInterval(2022, 1, 18),
                    DayInterval(2022, 1, 25)),
                intervals);
        }

        [Theory]
        [InlineData("mercredi")]
        public void TestParse_Wednesday(string input)
        {
            var dateParseResult = _timeParser.ParseDateTimes(input, parserVersion: 2);
            Assert.True(dateParseResult.IsSuccess);

            var dates = dateParseResult.Value.Generate(_jan1st2022, _feb1st2022).ToArray();
            Assert.Equal(
                Dates(
                    Day(2022, 1, 5),
                    Day(2022, 1, 12),
                    Day(2022, 1, 19),
                    Day(2022, 1, 26)),
                dates);

            var intervalParseResult = _timeParser.ParseTimeIntervals(input, parserVersion: 2);
            Assert.True(intervalParseResult.IsSuccess);

            var intervals = intervalParseResult.Value.Generate(_jan1st2022, _feb1st2022).ToArray();
            Assert.Equal(
                Intervals(
                    DayInterval(2022, 1, 5),
                    DayInterval(2022, 1, 12),
                    DayInterval(2022, 1, 19),
                    DayInterval(2022, 1, 26)),
                intervals);
        }

        [Theory]
        [InlineData("jeudi")]
        public void TestParse_Thursday(string input)
        {
            var dateParseResult = _timeParser.ParseDateTimes(input, parserVersion: 2);
            Assert.True(dateParseResult.IsSuccess);

            var dates = dateParseResult.Value.Generate(_jan1st2022, _feb1st2022).ToArray();
            Assert.Equal(
                Dates(
                    Day(2022, 1, 6),
                    Day(2022, 1, 13),
                    Day(2022, 1, 20),
                    Day(2022, 1, 27)),
                dates);

            var intervalParseResult = _timeParser.ParseTimeIntervals(input, parserVersion: 2);
            Assert.True(intervalParseResult.IsSuccess);

            var intervals = intervalParseResult.Value.Generate(_jan1st2022, _feb1st2022).ToArray();
            Assert.Equal(
                Intervals(
                    DayInterval(2022, 1, 6),
                    DayInterval(2022, 1, 13),
                    DayInterval(2022, 1, 20),
                    DayInterval(2022, 1, 27)),
                intervals);
        }

        [Theory]
        [InlineData("vendredi")]
        public void TestParse_Friday(string input)
        {
            var dateParseResult = _timeParser.ParseDateTimes(input, parserVersion: 2);
            Assert.True(dateParseResult.IsSuccess);

            var dates = dateParseResult.Value.Generate(_jan1st2022, _feb1st2022).ToArray();
            Assert.Equal(
                Dates(
                    Day(2022, 1, 7),
                    Day(2022, 1, 14),
                    Day(2022, 1, 21),
                    Day(2022, 1, 28)),
                dates);

            var intervalParseResult = _timeParser.ParseTimeIntervals(input, parserVersion: 2);
            Assert.True(intervalParseResult.IsSuccess);

            var intervals = intervalParseResult.Value.Generate(_jan1st2022, _feb1st2022).ToArray();
            Assert.Equal(
                Intervals(
                    DayInterval(2022, 1, 7),
                    DayInterval(2022, 1, 14),
                    DayInterval(2022, 1, 21),
                    DayInterval(2022, 1, 28)),
                intervals);
        }

        [Theory]
        [InlineData("samedi")]
        public void TestParse_Saturday(string input)
        {
            var dateParseResult = _timeParser.ParseDateTimes(input, parserVersion: 2);
            Assert.True(dateParseResult.IsSuccess);

            var dates = dateParseResult.Value.Generate(_jan1st2022, _feb1st2022).ToArray();
            Assert.Equal(
                Dates(
                    Day(2022, 1, 1),
                    Day(2022, 1, 8),
                    Day(2022, 1, 15),
                    Day(2022, 1, 22),
                    Day(2022, 1, 29)),
                dates);

            var intervalParseResult = _timeParser.ParseTimeIntervals(input, parserVersion: 2);
            Assert.True(intervalParseResult.IsSuccess);

            var intervals = intervalParseResult.Value.Generate(_jan1st2022, _feb1st2022).ToArray();
            Assert.Equal(
                Intervals(
                    DayInterval(2022, 1, 1),
                    DayInterval(2022, 1, 8),
                    DayInterval(2022, 1, 15),
                    DayInterval(2022, 1, 22),
                    DayInterval(2022, 1, 29)),
                intervals);
        }

        [Theory]
        [InlineData("dimanche")]
        public void TestParse_Sunday(string input)
        {
            var dateParseResult = _timeParser.ParseDateTimes(input, parserVersion: 2);
            Assert.True(dateParseResult.IsSuccess);

            var dates = dateParseResult.Value.Generate(_jan1st2022, _feb1st2022).ToArray();
            Assert.Equal(
                Dates(
                    Day(2022, 1, 2),
                    Day(2022, 1, 9),
                    Day(2022, 1, 16),
                    Day(2022, 1, 23),
                    Day(2022, 1, 30)),
                dates);

            var intervalParseResult = _timeParser.ParseTimeIntervals(input, parserVersion: 2);
            Assert.True(intervalParseResult.IsSuccess);

            var intervals = intervalParseResult.Value.Generate(_jan1st2022, _feb1st2022).ToArray();
            Assert.Equal(
                Intervals(
                    DayInterval(2022, 1, 2),
                    DayInterval(2022, 1, 9),
                    DayInterval(2022, 1, 16),
                    DayInterval(2022, 1, 23),
                    DayInterval(2022, 1, 30)),
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

            var intervals = intervalParseResult.Value.Generate(_jan1st2022, _feb1st2022).ToArray();
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
        [InlineData("chaque jour � partir de jeudi")]
        //[InlineData("(chaque jour (� partir de (jeudi)))")]
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

            var intervals = intervalParseResult.Value.Generate(_jan1st2022, _feb1st2022).ToArray();
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

            var intervals = intervalParseResult.Value.Generate(_jan1st2022, _feb1st2022).ToArray();
            Assert.Equal(
                Intervals(
                    DaysInterval(2022, 1, 1, dayNumber: 6)),
                intervals);
        }



        [Theory]
        [InlineData("chaque mois")]
        [InlineData("tous les mois")]
        //[InlineData("((chaque jour))")]
        public void TestParse_EveryMonth(string input)
        {
            var dateParseResult = _timeParser.ParseDateTimes(input, parserVersion: 2);
            Assert.True(dateParseResult.IsSuccess);

            var dates = dateParseResult.Value.Generate(_jan1st2022, _jan1st2023).ToArray();
            Assert.Equal(12, dates.Count());
            Assert.Equal(
                Dates(
                    Day(2022, 1, 1),
                    Day(2022, 2, 1),
                    Day(2022, 3, 1),
                    Day(2022, 4, 1),
                    Day(2022, 5, 1)),
                dates.Take(5));

            var intervalParseResult = _timeParser.ParseTimeIntervals(input, parserVersion: 2);
            Assert.True(intervalParseResult.IsSuccess);

            var intervals = intervalParseResult.Value.Generate(_jan1st2022, _jan1st2023).ToArray();
            Assert.Equal(
                Intervals(
                    Interval(Day(2022, 1, 1), Day(2023, 1, 1))),
                intervals);
        }


        [Theory]
        [InlineData("janvier", Month.January)]
        [InlineData("f�vrier", Month.February)]
        [InlineData("mars", Month.March)]
        [InlineData("avril", Month.April)]
        [InlineData("mai", Month.May)]
        [InlineData("juin", Month.June)]
        [InlineData("juillet", Month.July)]
        [InlineData("ao�t", Month.August)]
        [InlineData("septembre", Month.September)]
        [InlineData("octobre", Month.October)]
        [InlineData("novembre", Month.November)]
        [InlineData("d�cembre", Month.December)]
        public void TestParse_MonthByName(string input, Month month)
        {
            int monthIdx = (int)month;

            var dateParseResult = _timeParser.ParseDateTimes(input, parserVersion: 2);
            Assert.True(dateParseResult.IsSuccess);

            var dates = dateParseResult.Value.Generate(_jan1st2022, _jan1st2027).ToArray();
            Assert.Equal(
                Dates(
                    Day(2022, monthIdx, 1),
                    Day(2023, monthIdx, 1),
                    Day(2024, monthIdx, 1),
                    Day(2025, monthIdx, 1),
                    Day(2026, monthIdx, 1)),
                dates);

            var intervalParseResult = _timeParser.ParseTimeIntervals(input, parserVersion: 2);
            Assert.True(intervalParseResult.IsSuccess);

            var intervals = intervalParseResult.Value.Generate(_jan1st2022, _jan1st2027).ToArray();
            Assert.Equal(
                Intervals(
                    DaysInterval(2022, monthIdx, 1, dayNumber: DateTime.DaysInMonth(2022, monthIdx)),
                    DaysInterval(2023, monthIdx, 1, dayNumber: DateTime.DaysInMonth(2023, monthIdx)),
                    DaysInterval(2024, monthIdx, 1, dayNumber: DateTime.DaysInMonth(2024, monthIdx)),
                    DaysInterval(2025, monthIdx, 1, dayNumber: DateTime.DaysInMonth(2025, monthIdx)),
                    DaysInterval(2026, monthIdx, 1, dayNumber: DateTime.DaysInMonth(2026, monthIdx))),
                intervals);
        }


        [Theory]
        [InlineData("mars, avril et juillet")]
        public void TestParse_MultipleMonthes(string input)
        {
            var dateParseResult = _timeParser.ParseDateTimes(input, parserVersion: 2);
            Assert.True(dateParseResult.IsSuccess);

            var dates = dateParseResult.Value.Generate(_jan1st2022, _jan1st2023).ToArray();
            Assert.Equal(
                Dates(
                    Day(2022, 3, 1),
                    Day(2022, 4, 1),
                    Day(2022, 7, 1)),
                dates);

            var intervalParseResult = _timeParser.ParseTimeIntervals(input, parserVersion: 2);
            Assert.True(intervalParseResult.IsSuccess);

            var intervals = intervalParseResult.Value.Generate(_jan1st2022, _jan1st2023).ToArray();
            Assert.Equal(
                Intervals(
                    DaysInterval(2022, 3, 1, dayNumber: 61),
                    DaysInterval(2022, 7, 1, dayNumber: 31)),
                intervals);
        }
    }
}
