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
        [InlineData("aujourd'hui")]
        [InlineData("aujourd hui")]
        [InlineData("ajd")]
        public void Test_ParseTimeIntervals_Today(string input)
        {
            var result = _timeParser.ParseTimeIntervals(input);

            Assert.True(result.IsSuccess);
            Assert.Equal(
                Intervals(DayInterval(2022, 1, 1)),
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

        [Theory]
        [InlineData("mardi")]
        [InlineData("mar")]
        [InlineData("ma")]
        [InlineData("mardi prochain")]
        [InlineData("mar prochain")]
        [InlineData("mardi pro")]
        public void Test_ParseTimeIntervals_NextTuesday(string input)
        {
            var result = _timeParser.ParseTimeIntervals(input);

            Assert.True(result.IsSuccess);
            Assert.Equal(
                Intervals(DayInterval(2022, 1, 4)),
                result.Value.Generate(_fromDate, _toDate));
        }

        [Theory]
        [InlineData("mercredi")]
        [InlineData("mer")]
        [InlineData("me")]
        [InlineData("mercredi prochain")]
        [InlineData("mer prochain")]
        [InlineData("mercredi pro")]
        public void Test_ParseTimeIntervals_NextWednesday(string input)
        {
            var result = _timeParser.ParseTimeIntervals(input);

            Assert.True(result.IsSuccess);
            Assert.Equal(
                Intervals(DayInterval(2022, 1, 5)),
                result.Value.Generate(_fromDate, _toDate));
        }

        [Theory]
        [InlineData("jeudi")]
        [InlineData("jeu")]
        [InlineData("je")]
        [InlineData("jeudi prochain")]
        [InlineData("jeu prochain")]
        [InlineData("jeudi pro")]
        public void Test_ParseTimeIntervals_NextThursday(string input)
        {
            var result = _timeParser.ParseTimeIntervals(input);

            Assert.True(result.IsSuccess);
            Assert.Equal(
                Intervals(DayInterval(2022, 1, 6)),
                result.Value.Generate(_fromDate, _toDate));
        }

        [Theory]
        [InlineData("vendredi")]
        [InlineData("ven")]
        [InlineData("ve")]
        [InlineData("vendredi prochain")]
        [InlineData("ven prochain")]
        [InlineData("vendredi pro")]
        public void Test_ParseTimeIntervals_NextFriday(string input)
        {
            var result = _timeParser.ParseTimeIntervals(input);

            Assert.True(result.IsSuccess);
            Assert.Equal(
                Intervals(DayInterval(2022, 1, 7)),
                result.Value.Generate(_fromDate, _toDate));
        }

        [Theory]
        [InlineData("samedi")]
        [InlineData("sam")]
        [InlineData("sa")]
        [InlineData("samedi prochain")]
        [InlineData("sam prochain")]
        [InlineData("samedi pro")]
        public void Test_ParseTimeIntervals_NextSaturday(string input)
        {
            var result = _timeParser.ParseTimeIntervals(input);

            Assert.True(result.IsSuccess);
            Assert.Equal(
                Intervals(DayInterval(2022, 1, 8)),
                result.Value.Generate(_fromDate, _toDate));
        }

        [Theory]
        [InlineData("dimanche")]
        [InlineData("dim")]
        [InlineData("di")]
        [InlineData("dimanche prochain")]
        [InlineData("dim prochain")]
        [InlineData("dimanche pro")]
        public void Test_ParseTimeIntervals_NextSunday(string input)
        {
            var result = _timeParser.ParseTimeIntervals(input);

            Assert.True(result.IsSuccess);
            Assert.Equal(
                Intervals(DayInterval(2022, 1, 2)),
                result.Value.Generate(_fromDate, _toDate));
        }
    }
}
