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
        private DateTime _mar1st2022 = new DateTime(2022, 3, 1);
        private DateTime _now = new DateTime(2022, 1, 1, 10, 30, 0);

        public TimeParserV2Tests()
        {
            Time.SetNow(_now);
        }


        [Theory]
        [InlineData("jour")]
        [InlineData("le jour")]
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

        #region DaysOfWeek (lundi, mardi…)

        [Theory]
        [InlineData("lundi")]
        [InlineData("le lundi")]
        [InlineData("chaque lundi")]
        [InlineData("tous les lundis")]
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

        #endregion

        #region DaysOfMonth (le premier, le deux…)

        [Theory]
        [InlineData("le 1", 1)]
        [InlineData("le 1er", 1)]
        [InlineData("le premier", 1)]
        [InlineData("le 2", 2)]
        [InlineData("le deux", 2)]
        [InlineData("le 3", 3)]
        [InlineData("le trois", 3)]
        [InlineData("le 4", 4)]
        [InlineData("le quatre", 4)]
        [InlineData("le 5", 5)]
        [InlineData("le cinq", 5)]
        [InlineData("le 6", 6)]
        [InlineData("le six", 6)]
        [InlineData("le 7", 7)]
        [InlineData("le sept", 7)]
        [InlineData("le 8", 8)]
        [InlineData("le huit", 8)]
        [InlineData("le 9", 9)]
        [InlineData("le neuf", 9)]
        [InlineData("le 10", 10)]
        [InlineData("le dix", 10)]
        [InlineData("le 11", 11)]
        [InlineData("le onze", 11)]
        [InlineData("le 12", 12)]
        [InlineData("le douze", 12)]
        [InlineData("le 13", 13)]
        [InlineData("le treize", 13)]
        [InlineData("le 14", 14)]
        [InlineData("le quatorze", 14)]
        [InlineData("le 15", 15)]
        [InlineData("le quinze", 15)]
        [InlineData("le 16", 16)]
        [InlineData("le seize", 16)]
        [InlineData("le 17", 17)]
        [InlineData("le dix-sept", 17)]
        [InlineData("le dix sept", 17)]
        [InlineData("le 18", 18)]
        [InlineData("le dix-huit", 18)]
        [InlineData("le dix huit", 18)]
        [InlineData("le 19", 19)]
        [InlineData("le dix-neuf", 19)]
        [InlineData("le dix neuf", 19)]
        [InlineData("le 20", 20)]
        [InlineData("le vingt", 20)]
        [InlineData("le 21", 21)]
        [InlineData("le vingt-et-un", 21)]
        [InlineData("le vingt et un", 21)]
        [InlineData("le 22", 22)]
        [InlineData("le vingt-deux", 22)]
        [InlineData("le vingt deux", 22)]
        [InlineData("le 23", 23)]
        [InlineData("le vingt-trois", 23)]
        [InlineData("le vingt trois", 23)]
        [InlineData("le 24", 24)]
        [InlineData("le vingt-quatre", 24)]
        [InlineData("le vingt quatre", 24)]
        [InlineData("le 25", 25)]
        [InlineData("le vingt-cinq", 25)]
        [InlineData("le vingt cinq", 25)]
        [InlineData("le 26", 26)]
        [InlineData("le vingt-six", 26)]
        [InlineData("le vingt six", 26)]
        [InlineData("le 27", 27)]
        [InlineData("le vingt-sept", 27)]
        [InlineData("le vingt sept", 27)]
        [InlineData("le 28", 28)]
        [InlineData("le vingt-huit", 28)]
        [InlineData("le vingt huit", 28)]
        public void TestParse_DayInMonth(string input, int day)
        {
            var dateParseResult = _timeParser.ParseDateTimes(input, parserVersion: 2);
            Assert.True(dateParseResult.IsSuccess);

            var dates = dateParseResult.Value.Generate(_jan1st2022, _jan1st2023).ToArray();
            Assert.Equal(12, dates.Length);
            Assert.Equal(
                Dates(
                    Day(2022, 1, day),
                    Day(2022, 2, day),
                    Day(2022, 3, day),
                    Day(2022, 4, day),
                    Day(2022, 5, day)),
                dates.Take(5));

            var intervalParseResult = _timeParser.ParseTimeIntervals(input, parserVersion: 2);
            Assert.True(intervalParseResult.IsSuccess);

            var intervals = intervalParseResult.Value.Generate(_jan1st2022, _jan1st2023).ToArray();
            Assert.Equal(12, dates.Length);
            Assert.Equal(
                Intervals(
                    DayInterval(2022, 1, day),
                    DayInterval(2022, 2, day),
                    DayInterval(2022, 3, day),
                    DayInterval(2022, 4, day),
                    DayInterval(2022, 5, day)),
                intervals.Take(5));
        }

        [Theory]
        [InlineData("le 29", 29)]
        [InlineData("le vingt-neuf", 29)]
        [InlineData("le vingt neuf", 29)]
        [InlineData("le 30", 30)]
        [InlineData("le trente", 30)]
        public void TestParse_Day29InMonth(string input, int day)
        {
            var dateParseResult = _timeParser.ParseDateTimes(input, parserVersion: 2);
            Assert.True(dateParseResult.IsSuccess);

            var dates = dateParseResult.Value.Generate(_jan1st2022, _jan1st2023).ToArray();
            Assert.Equal(11, dates.Length);
            Assert.Equal(
                Dates(
                    Day(2022, 1, day),
                    Day(2022, 3, day),
                    Day(2022, 4, day),
                    Day(2022, 5, day),
                    Day(2022, 6, day)),
                dates.Take(5));

            var intervalParseResult = _timeParser.ParseTimeIntervals(input, parserVersion: 2);
            Assert.True(intervalParseResult.IsSuccess);

            var intervals = intervalParseResult.Value.Generate(_jan1st2022, _jan1st2023).ToArray();
            Assert.Equal(11, dates.Length);
            Assert.Equal(
                Intervals(
                    DayInterval(2022, 1, day),
                    DayInterval(2022, 3, day),
                    DayInterval(2022, 4, day),
                    DayInterval(2022, 5, day),
                    DayInterval(2022, 6, day)),
                intervals.Take(5));
        }

        [Theory]
        [InlineData("le 31")]
        [InlineData("le trente-et-un")]
        [InlineData("le trente et un")]
        public void TestParse_Day31InMonth(string input)
        {
            var dateParseResult = _timeParser.ParseDateTimes(input, parserVersion: 2);
            Assert.True(dateParseResult.IsSuccess);

            var dates = dateParseResult.Value.Generate(_jan1st2022, _jan1st2023).ToArray();
            Assert.Equal(7, dates.Length);
            Assert.Equal(
                Dates(
                    Day(2022, 1, 31),
                    Day(2022, 3, 31),
                    Day(2022, 5, 31),
                    Day(2022, 7, 31),
                    Day(2022, 8, 31)),
                dates.Take(5));

            var intervalParseResult = _timeParser.ParseTimeIntervals(input, parserVersion: 2);
            Assert.True(intervalParseResult.IsSuccess);

            var intervals = intervalParseResult.Value.Generate(_jan1st2022, _jan1st2023).ToArray();
            Assert.Equal(7, dates.Length);
            Assert.Equal(
                Intervals(
                    DayInterval(2022, 1, 31),
                    DayInterval(2022, 3, 31),
                    DayInterval(2022, 5, 31),
                    DayInterval(2022, 7, 31),
                    DayInterval(2022, 8, 31)),
                intervals.Take(5));
        }

        #endregion

        #region DaysOfWeekOfMonth

        [Theory]
        [InlineData("le vendredi 13")]
        public void TestParse_FridayThe13th(string input)
        {
            var toDate = new DateTime(2025, 1, 1);

            var dateParseResult = _timeParser.ParseDateTimes(input, parserVersion: 2);
            Assert.True(dateParseResult.IsSuccess);

            var dates = dateParseResult.Value.Generate(_jan1st2022, toDate).ToArray();
            Assert.Equal(
                Dates(
                    Day(2022, 5, 13),
                    Day(2023, 1, 13),
                    Day(2023, 10, 13),
                    Day(2024, 9, 13),
                    Day(2024, 12, 13)),
                dates);

            var intervalParseResult = _timeParser.ParseTimeIntervals(input, parserVersion: 2);
            Assert.True(intervalParseResult.IsSuccess);

            var intervals = intervalParseResult.Value.Generate(_jan1st2022, toDate).ToArray();
            Assert.Equal(
                Intervals(
                    DayInterval(2022, 5, 13),
                    DayInterval(2023, 1, 13),
                    DayInterval(2023, 10, 13),
                    DayInterval(2024, 9, 13),
                    DayInterval(2024, 12, 13)),
                intervals);
        }

        #endregion


        [Theory]
        [InlineData("le 13 mai")]
        public void TestParse_The13thOfMay(string input)
        {
            var dateParseResult = _timeParser.ParseDateTimes(input, parserVersion: 2);
            Assert.True(dateParseResult.IsSuccess);

            var dates = dateParseResult.Value.Generate(_jan1st2022, _jan1st2027).ToArray();
            Assert.Equal(
                Dates(
                    Day(2022, 5, 13),
                    Day(2023, 5, 13),
                    Day(2024, 5, 13),
                    Day(2025, 5, 13),
                    Day(2026, 5, 13)),
                dates);

            var intervalParseResult = _timeParser.ParseTimeIntervals(input, parserVersion: 2);
            Assert.True(intervalParseResult.IsSuccess);

            var intervals = intervalParseResult.Value.Generate(_jan1st2022, _jan1st2027).ToArray();
            Assert.Equal(
                Intervals(
                    DayInterval(2022, 5, 13),
                    DayInterval(2023, 5, 13),
                    DayInterval(2024, 5, 13),
                    DayInterval(2025, 5, 13),
                    DayInterval(2026, 5, 13)),
                intervals);
        }

        [Theory]
        [InlineData("le vendredi 13 mai")]
        public void TestParse_FridayThe13thOfMay(string input)
        {
            var dateParseResult = _timeParser.ParseDateTimes(input, parserVersion: 2);
            Assert.True(dateParseResult.IsSuccess);

            var dates = dateParseResult.Value.Generate(_jan1st2022, _jan1st2023).ToArray();
            Assert.Equal(
                Dates(Day(2022, 5, 13)),
                dates);

            var intervalParseResult = _timeParser.ParseTimeIntervals(input, parserVersion: 2);
            Assert.True(intervalParseResult.IsSuccess);

            var intervals = intervalParseResult.Value.Generate(_jan1st2022, _jan1st2023).ToArray();
            Assert.Equal(
                Intervals(
                    DayInterval(2022, 5, 13)),
                intervals);
        }

        #region Opérateurs séquentiels 

        [Theory]
        [InlineData("le premier jeudi de chaque mois")]
        [InlineData("le premier jeudi du mois")]
        public void TestParse_FirstThursdayOfEachMonth(string input)
        {
            var dateParseResult = _timeParser.ParseDateTimes(input, parserVersion: 2);
            Assert.True(dateParseResult.IsSuccess);

            var dates = dateParseResult.Value.Generate(_jan1st2022, _jan1st2023).ToArray();
            Assert.Equal(12, dates.Length);
            Assert.Equal(
                Dates(
                    Day(2022, 1, 6),
                    Day(2022, 2, 3),
                    Day(2022, 3, 3),
                    Day(2022, 4, 7),
                    Day(2022, 5, 5)),
                dates.Take(5));

            var intervalParseResult = _timeParser.ParseTimeIntervals(input, parserVersion: 2);
            Assert.True(intervalParseResult.IsSuccess);

            var intervals = intervalParseResult.Value.Generate(_jan1st2022, _jan1st2023).ToArray();
            Assert.Equal(12, intervals.Length);
            Assert.Equal(
                Intervals(
                    DayInterval(2022, 1, 6),
                    DayInterval(2022, 2, 10),
                    DayInterval(2022, 3, 3),
                    DayInterval(2022, 4, 7),
                    DayInterval(2022, 5, 5)),
                intervals.Take(5));
        }

        #endregion

        #region Opérateur ET

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
        [InlineData("le 5, le 11 et le 13")]
        public void TestParse_MultipleDaysOfMonth(string input)
        {
            var dateParseResult = _timeParser.ParseDateTimes(input, parserVersion: 2);
            Assert.True(dateParseResult.IsSuccess);

            var dates = dateParseResult.Value.Generate(_jan1st2022, _mar1st2022).ToArray();
            Assert.Equal(
                Dates(
                    Day(2022, 1, 5),
                    Day(2022, 1, 11),
                    Day(2022, 1, 13),
                    Day(2022, 2, 5),
                    Day(2022, 2, 11),
                    Day(2022, 2, 13)),
                dates);

            var intervalParseResult = _timeParser.ParseTimeIntervals(input, parserVersion: 2);
            Assert.True(intervalParseResult.IsSuccess);

            var intervals = intervalParseResult.Value.Generate(_jan1st2022, _mar1st2022).ToArray();
            Assert.Equal(
                Intervals(
                    DayInterval(2022, 1, 5),
                    DayInterval(2022, 1, 11),
                    DayInterval(2022, 1, 13),
                    DayInterval(2022, 2, 5),
                    DayInterval(2022, 2, 11),
                    DayInterval(2022, 2, 13)),
                intervals);
        }

        [Theory]
        [InlineData("le vendredi 13, le jeudi 3 et le lundi 18")]
        public void TestParse_MultipleDaysOfWeekOfMonth(string input)
        {
            var dateParseResult = _timeParser.ParseDateTimes(input, parserVersion: 2);
            Assert.True(dateParseResult.IsSuccess);

            var dates = dateParseResult.Value.Generate(_jan1st2022, _jan1st2023).ToArray();
            Assert.Equal(
                Dates(
                    Day(2022, 2, 3),
                    Day(2022, 3, 3),
                    Day(2022, 4, 18),
                    Day(2022, 5, 13),
                    Day(2022, 7, 18),
                    Day(2022, 11, 3)),
                dates);

            var intervalParseResult = _timeParser.ParseTimeIntervals(input, parserVersion: 2);
            Assert.True(intervalParseResult.IsSuccess);

            var intervals = intervalParseResult.Value.Generate(_jan1st2022, _jan1st2023).ToArray();
            Assert.Equal(
                Intervals(
                    DayInterval(2022, 2, 3),
                    DayInterval(2022, 3, 3),
                    DayInterval(2022, 4, 18),
                    DayInterval(2022, 5, 13),
                    DayInterval(2022, 7, 18),
                    DayInterval(2022, 11, 3)),
                intervals);
        }

        [Theory]
        [InlineData("le 13, le 15 et le 17 mai")]
        public void TestParse_MultipleDayOfMonthInMonth(string input)
        {
            var dateParseResult = _timeParser.ParseDateTimes(input, parserVersion: 2);
            Assert.True(dateParseResult.IsSuccess);

            var dates = dateParseResult.Value.Generate(_jan1st2022, _jan1st2023).ToArray();
            Assert.Equal(
                Dates(
                    Day(2022, 5, 13),
                    Day(2022, 5, 15),
                    Day(2022, 5, 17)),
                dates);

            var intervalParseResult = _timeParser.ParseTimeIntervals(input, parserVersion: 2);
            Assert.True(intervalParseResult.IsSuccess);

            var intervals = intervalParseResult.Value.Generate(_jan1st2022, _jan1st2023).ToArray();
            Assert.Equal(
                Intervals(
                    DayInterval(2022, 5, 13),
                    DayInterval(2022, 5, 15),
                    DayInterval(2022, 5, 17)),
                intervals);
        }

        [Theory]
        [InlineData("le 13 et le 15 mai et le 17 juin")]
        public void TestParse_MultipleLevelOfDayOfMonthInMonth(string input)
        {
            var dateParseResult = _timeParser.ParseDateTimes(input, parserVersion: 2);
            Assert.True(dateParseResult.IsSuccess);

            var dates = dateParseResult.Value.Generate(_jan1st2022, _jan1st2023).ToArray();
            Assert.Equal(
                Dates(
                    Day(2022, 5, 13),
                    Day(2022, 5, 15),
                    Day(2022, 6, 17)),
                dates);

            var intervalParseResult = _timeParser.ParseTimeIntervals(input, parserVersion: 2);
            Assert.True(intervalParseResult.IsSuccess);

            var intervals = intervalParseResult.Value.Generate(_jan1st2022, _jan1st2023).ToArray();
            Assert.Equal(
                Intervals(
                    DayInterval(2022, 5, 13),
                    DayInterval(2022, 5, 15),
                    DayInterval(2022, 6, 17)),
                intervals);
        }

        [Theory]
        [InlineData("le vendredi 13 et le dimanche 15 mai")]
        public void TestParse_MultipleLevelOfDaysOfWeekOfMonthInMonth(string input)
        {
            var dateParseResult = _timeParser.ParseDateTimes(input, parserVersion: 2);
            Assert.True(dateParseResult.IsSuccess);

            var dates = dateParseResult.Value.Generate(_jan1st2022, _jan1st2023).ToArray();
            Assert.Equal(
                Dates(
                    Day(2022, 5, 13),
                    Day(2022, 5, 15)),
                dates);

            var intervalParseResult = _timeParser.ParseTimeIntervals(input, parserVersion: 2);
            Assert.True(intervalParseResult.IsSuccess);

            var intervals = intervalParseResult.Value.Generate(_jan1st2022, _jan1st2023).ToArray();
            Assert.Equal(
                Intervals(
                    DayInterval(2022, 5, 13),
                    DayInterval(2022, 5, 15)),
                intervals);
        }

        [Theory]
        [InlineData("le vendredi 13 et le dimanche 15 mai et le jeudi 3 novembre")]
        public void TestParse_MultipleDaysOfWeekOfMonthInMonth(string input)
        {
            var dateParseResult = _timeParser.ParseDateTimes(input, parserVersion: 2);
            Assert.True(dateParseResult.IsSuccess);

            var dates = dateParseResult.Value.Generate(_jan1st2022, _jan1st2023).ToArray();
            Assert.Equal(
                Dates(
                    Day(2022, 5, 13),
                    Day(2022, 5, 15),
                    Day(2022, 11, 3)),
                dates);

            var intervalParseResult = _timeParser.ParseTimeIntervals(input, parserVersion: 2);
            Assert.True(intervalParseResult.IsSuccess);

            var intervals = intervalParseResult.Value.Generate(_jan1st2022, _jan1st2023).ToArray();
            Assert.Equal(
                Intervals(
                    DayInterval(2022, 5, 13),
                    DayInterval(2022, 5, 15),
                    DayInterval(2022, 11, 3)),
                intervals);
        }

        //[Theory(Skip = "")]
        //[InlineData("le 13 avril et mai", Skip = "tournure somme toute étrange")]
        //public void TestParse_DayOfMonthInMultipleMonth(string input)
        //{
        //    var dateParseResult = _timeParser.ParseDateTimes(input, parserVersion: 2);
        //    Assert.True(dateParseResult.IsSuccess);

        //    var dates = dateParseResult.Value.Generate(_jan1st2022, _jan1st2023).ToArray();
        //    Assert.Equal(
        //        Dates(
        //            Day(2022, 4, 13),
        //            Day(2022, 5, 13)),
        //        dates);

        //    var intervalParseResult = _timeParser.ParseTimeIntervals(input, parserVersion: 2);
        //    Assert.True(intervalParseResult.IsSuccess);

        //    var intervals = intervalParseResult.Value.Generate(_jan1st2022, _jan1st2023).ToArray();
        //    Assert.Equal(
        //        Intervals(
        //            DayInterval(2022, 4, 13),
        //            DayInterval(2022, 5, 13)),
        //        intervals);
        //}

        #endregion

        #region Opérateur À PARTIR DE

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

            var intervals = intervalParseResult.Value.Generate(_jan1st2022, _feb1st2022).ToArray();
            Assert.Equal(
                Intervals(
                    DaysInterval(2022, 1, 6, dayNumber: 26)),
                intervals);
        }

        [Theory]
        [InlineData("tous les jours à partir de mars")]
        public void TestParse_EveryDayFromMonth(string input)
        {
            var dateParseResult = _timeParser.ParseDateTimes(input, parserVersion: 2);
            Assert.True(dateParseResult.IsSuccess);

            var dates = dateParseResult.Value.Generate(_jan1st2022, _jan1st2023).ToArray();
            Assert.Equal(306, dates.Length);
            Assert.Equal(
                Dates(
                    Day(2022, 3, 1),
                    Day(2022, 3, 2),
                    Day(2022, 3, 3),
                    Day(2022, 3, 4),
                    Day(2022, 3, 5)),
                dates.Take(5));

            var intervalParseResult = _timeParser.ParseTimeIntervals(input, parserVersion: 2);
            Assert.True(intervalParseResult.IsSuccess);

            var intervals = intervalParseResult.Value.Generate(_jan1st2022, _jan1st2023).ToArray();
            Assert.Equal(
                Intervals(
                    Interval(Day(2022, 3, 1), Day(2023, 1, 1))),
                intervals);
        }

        #endregion

        #region Opérateur JUSQU'À

        [Theory]
        [InlineData("chaque jour jusqu'à vendredi")]
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

        #endregion

        #region Opérateur SAUF

        [Theory]
        [InlineData("tous les jours sauf le mardi")]
        public void TestParse_EveryDayExceptDayOfWeek(string input)
        {
            var dateParseResult = _timeParser.ParseDateTimes(input, parserVersion: 2);
            Assert.True(dateParseResult.IsSuccess);

            var dates = dateParseResult.Value.Generate(_jan1st2022, _feb1st2022).ToArray();
            Assert.Equal(27, dates.Length);
            Assert.Equal(
                Dates(
                    Day(2022, 1, 1),
                    Day(2022, 1, 2),
                    Day(2022, 1, 3),
                    Day(2022, 1, 5),
                    Day(2022, 1, 6),
                    Day(2022, 1, 7)),
                dates.Take(6));

            var intervalParseResult = _timeParser.ParseTimeIntervals(input, parserVersion: 2);
            Assert.True(intervalParseResult.IsSuccess);

            var intervals = intervalParseResult.Value.Generate(_jan1st2022, _feb1st2022).ToArray();
            Assert.Equal(
                Intervals(
                    DaysInterval(2022, 1, 1, dayNumber: 3),
                    DaysInterval(2022, 1, 5, dayNumber: 6),
                    DaysInterval(2022, 1, 12, dayNumber: 6),
                    DaysInterval(2022, 1, 19, dayNumber: 6),
                    DaysInterval(2022, 1, 26, dayNumber: 6)),
                intervals);
        }

        #endregion


        [Theory]
        [InlineData("mois")]
        [InlineData("le mois")]
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
        [InlineData("février", Month.February)]
        [InlineData("mars", Month.March)]
        [InlineData("avril", Month.April)]
        [InlineData("mai", Month.May)]
        [InlineData("juin", Month.June)]
        [InlineData("juillet", Month.July)]
        [InlineData("août", Month.August)]
        [InlineData("septembre", Month.September)]
        [InlineData("octobre", Month.October)]
        [InlineData("novembre", Month.November)]
        [InlineData("décembre", Month.December)]
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


        [Theory]
        [InlineData("tous les mardis de mars")]
        public void TestParse_EveryDaysOfWeekInNamedMonth(string input)
        {
            var dateParseResult = _timeParser.ParseDateTimes(input, parserVersion: 2);
            Assert.True(dateParseResult.IsSuccess);

            var dates = dateParseResult.Value.Generate(_jan1st2022, _jan1st2023).ToArray();
            Assert.Equal(
                Dates(
                    Day(2022, 3, 1),
                    Day(2022, 3, 8),
                    Day(2022, 3, 15),
                    Day(2022, 3, 22),
                    Day(2022, 3, 29)),
                dates);

            var intervalParseResult = _timeParser.ParseTimeIntervals(input, parserVersion: 2);
            Assert.True(intervalParseResult.IsSuccess);

            var intervals = intervalParseResult.Value.Generate(_jan1st2022, _jan1st2023).ToArray();
            Assert.Equal(
                Intervals(
                    DayInterval(2022, 3, 1),
                    DayInterval(2022, 3, 8),
                    DayInterval(2022, 3, 15),
                    DayInterval(2022, 3, 22),
                    DayInterval(2022, 3, 29)),
                intervals);
        }

        [Theory]
        [InlineData("tous les mardis, jeudis et samedis de mars")]
        public void TestParse_EveryMultipleDaysOfWeekInNamedMonth(string input)
        {
            var dateParseResult = _timeParser.ParseDateTimes(input, parserVersion: 2);
            Assert.True(dateParseResult.IsSuccess);

            var dates = dateParseResult.Value.Generate(_jan1st2022, _jan1st2023).ToArray();
            Assert.Equal(
                Dates(
                    Day(2022, 3, 1),
                    Day(2022, 3, 3),
                    Day(2022, 3, 5),
                    Day(2022, 3, 8),
                    Day(2022, 3, 10),
                    Day(2022, 3, 12),
                    Day(2022, 3, 15),
                    Day(2022, 3, 17),
                    Day(2022, 3, 19),
                    Day(2022, 3, 22),
                    Day(2022, 3, 24),
                    Day(2022, 3, 26),
                    Day(2022, 3, 29),
                    Day(2022, 3, 31)),
                dates);

            var intervalParseResult = _timeParser.ParseTimeIntervals(input, parserVersion: 2);
            Assert.True(intervalParseResult.IsSuccess);

            var intervals = intervalParseResult.Value.Generate(_jan1st2022, _jan1st2023).ToArray();
            Assert.Equal(
                Intervals(
                    DayInterval(2022, 3, 1),
                    DayInterval(2022, 3, 3),
                    DayInterval(2022, 3, 5),
                    DayInterval(2022, 3, 8),
                    DayInterval(2022, 3, 10),
                    DayInterval(2022, 3, 12),
                    DayInterval(2022, 3, 15),
                    DayInterval(2022, 3, 17),
                    DayInterval(2022, 3, 19),
                    DayInterval(2022, 3, 22),
                    DayInterval(2022, 3, 24),
                    DayInterval(2022, 3, 26),
                    DayInterval(2022, 3, 29),
                    DayInterval(2022, 3, 31)),
                intervals);
        }

        [Theory]
        [InlineData("tous les mardis de mars, mai et juillet")]
        public void TestParse_EveryDaysOfWeekInMultipleNamedMonthes(string input)
        {
            var dateParseResult = _timeParser.ParseDateTimes(input, parserVersion: 2);
            Assert.True(dateParseResult.IsSuccess);

            var dates = dateParseResult.Value.Generate(_jan1st2022, _jan1st2023).ToArray();
            Assert.Equal(
                Dates(
                    Day(2022, 3, 1),
                    Day(2022, 3, 8),
                    Day(2022, 3, 15),
                    Day(2022, 3, 22),
                    Day(2022, 3, 29),
                    Day(2022, 5, 3),
                    Day(2022, 5, 10),
                    Day(2022, 5, 17),
                    Day(2022, 5, 24),
                    Day(2022, 5, 31),
                    Day(2022, 7, 5),
                    Day(2022, 7, 12),
                    Day(2022, 7, 19),
                    Day(2022, 7, 26)),
                dates);

            var intervalParseResult = _timeParser.ParseTimeIntervals(input, parserVersion: 2);
            Assert.True(intervalParseResult.IsSuccess);

            var intervals = intervalParseResult.Value.Generate(_jan1st2022, _jan1st2023).ToArray();
            Assert.Equal(
                Intervals(
                    DayInterval(2022, 3, 1),
                    DayInterval(2022, 3, 8),
                    DayInterval(2022, 3, 15),
                    DayInterval(2022, 3, 22),
                    DayInterval(2022, 3, 29),
                    DayInterval(2022, 5, 3),
                    DayInterval(2022, 5, 10),
                    DayInterval(2022, 5, 17),
                    DayInterval(2022, 5, 24),
                    DayInterval(2022, 5, 31),
                    DayInterval(2022, 7, 5),
                    DayInterval(2022, 7, 12),
                    DayInterval(2022, 7, 19),
                    DayInterval(2022, 7, 26)),
                intervals);
        }
    }
}
