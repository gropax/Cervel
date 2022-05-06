using System;
using Xunit;

namespace Cervel.TimeParser.Tests
{
    public class TimeParserTests : TestBase
    {
        private TimeParser _timeParser = new TimeParser();
        //private DateTime _fromDate = new DateTime(2022, 1, 1);
        private DateTime _fromDate = new DateTime(2022, 1, 1);
        private DateTime _now = new DateTime(2022, 1, 1, 10, 30, 0);
        private DateTime _toDate = new DateTime(2032, 1, 1);
        private DateTime _febFst = new DateTime(2022, 2, 1);

        public TimeParserTests()
        {
            Time.SetNow(_now);
        }

        #region Dates

        #region Dates spéciales
        #region DateTimes "jamais"
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
        #endregion

        #region DateTimes "maintenant"
        [Theory]
        [InlineData("maintenant")]
        [InlineData("mnt")]
        public void Test_ParseDateTimes_Now(string input)
        {
            var result = _timeParser.ParseDateTimes(input);

            Assert.True(result.IsSuccess);
            Assert.Equal(
                Dates(Day(2022, 1, 1, 10, 30, 0)),
                result.Value.Generate(_fromDate, _toDate));
        }
        #endregion

        #endregion

        #region Dates relatives à aujourd'hui
        #region DateTimes "il y a N jours"
        [Theory]
        [InlineData("il y a 3 jour")]
        [InlineData("il y a 3 jours")]
        [InlineData("ya 3 jours")]
        [InlineData("ya 3 j")]
        public void Test_ParseDateTimes_NDaysAgo(string input)
        {
            var result = _timeParser.ParseDateTimes(input);

            Assert.True(result.IsSuccess);
            Assert.Equal(
                Dates(Day(2021, 12, 29)),
                result.Value.Generate(_fromDate, _toDate));
        }
        #endregion

        #region DateTimes "avant-hier"
        [Theory]
        [InlineData("avant-hier")]
        [InlineData("avant hier")]
        [InlineData("avt hier")]
        [InlineData("av hier")]
        public void Test_ParseDateTimes_DayBeforeYesterday(string input)
        {
            var result = _timeParser.ParseDateTimes(input);

            Assert.True(result.IsSuccess);
            Assert.Equal(
                Dates(Day(2021, 12, 30)),
                result.Value.Generate(_fromDate, _toDate));
        }
        #endregion

        #region DateTimes "hier"
        [Theory]
        [InlineData("hier")]
        public void Test_ParseDateTimes_Yesterday(string input)
        {
            var result = _timeParser.ParseDateTimes(input);

            Assert.True(result.IsSuccess);
            Assert.Equal(
                Dates(Day(2021, 12, 31)),
                result.Value.Generate(_fromDate, _toDate));
        }
        #endregion

        #region DateTimes "aujourd'hui"
        [Theory]
        [InlineData("aujourd'hui")]
        [InlineData("aujourd hui")]
        [InlineData("ajd")]
        public void Test_ParseDateTimes_Today(string input)
        {
            var result = _timeParser.ParseDateTimes(input);

            Assert.True(result.IsSuccess);
            Assert.Equal(
                Dates(Day(2022, 1, 1)),
                result.Value.Generate(_fromDate, _toDate));
        }
        #endregion

        #region DateTimes "demain"
        [Theory]
        [InlineData("demain")]
        [InlineData("dem")]
        public void Test_ParseDateTimes_Tomorrow(string input)
        {
            var result = _timeParser.ParseDateTimes(input);

            Assert.True(result.IsSuccess);
            Assert.Equal(
                Dates(Day(2022, 1, 2)),
                result.Value.Generate(_fromDate, _toDate));
        }
        #endregion

        #region DateTimes "après-demain"
        [Theory]
        [InlineData("après-demain")]
        [InlineData("après demain")]
        [InlineData("aprés demain")]
        [InlineData("ap dem")]
        public void Test_ParseDateTimes_DayAfterTomorrow(string input)
        {
            var result = _timeParser.ParseDateTimes(input);

            Assert.True(result.IsSuccess);
            Assert.Equal(
                Dates(Day(2022, 1, 3)),
                result.Value.Generate(_fromDate, _toDate));
        }
        #endregion

        #region DateTimes "dans N jours"
        [Theory]
        [InlineData("dans 3 jour")]
        [InlineData("dans 3 jours")]
        [InlineData("ds 3 j")]
        public void Test_ParseDateTimes_InNDays(string input)
        {
            var result = _timeParser.ParseDateTimes(input);

            Assert.True(result.IsSuccess);
            Assert.Equal(
                Dates(Day(2022, 1, 4)),
                result.Value.Generate(_fromDate, _toDate));
        }
        #endregion
        #endregion

        #region Prochain jour de la semaine
        #region DateTimes "lundi prochain"
        [Theory]
        [InlineData("lundi")]
        [InlineData("lu")]
        [InlineData("lun")]
        [InlineData("lundi prochain")]
        [InlineData("lun prochain")]
        [InlineData("lundi pro")]
        public void Test_ParseDateTimes_NextMonday(string input)
        {
            var result = _timeParser.ParseDateTimes(input);

            Assert.True(result.IsSuccess);
            Assert.Equal(
                Dates(Day(2022, 1, 3)),
                result.Value.Generate(_fromDate, _toDate));
        }

        [Theory]
        [InlineData("mardi")]
        [InlineData("mar")]
        [InlineData("ma")]
        [InlineData("mardi prochain")]
        [InlineData("mar prochain")]
        [InlineData("mardi pro")]
        public void Test_ParseDateTimes_NextTuesday(string input)
        {
            var result = _timeParser.ParseDateTimes(input);

            Assert.True(result.IsSuccess);
            Assert.Equal(
                Dates(Day(2022, 1, 4)),
                result.Value.Generate(_fromDate, _toDate));
        }

        [Theory]
        [InlineData("mercredi")]
        [InlineData("mer")]
        [InlineData("me")]
        [InlineData("mercredi prochain")]
        [InlineData("mer prochain")]
        [InlineData("mercredi pro")]
        public void Test_ParseDateTimes_NextWednesday(string input)
        {
            var result = _timeParser.ParseDateTimes(input);

            Assert.True(result.IsSuccess);
            Assert.Equal(
                Dates(Day(2022, 1, 5)),
                result.Value.Generate(_fromDate, _toDate));
        }

        [Theory]
        [InlineData("jeudi")]
        [InlineData("jeu")]
        [InlineData("je")]
        [InlineData("jeudi prochain")]
        [InlineData("jeu prochain")]
        [InlineData("jeudi pro")]
        public void Test_ParseDateTimes_NextThursday(string input)
        {
            var result = _timeParser.ParseDateTimes(input);

            Assert.True(result.IsSuccess);
            Assert.Equal(
                Dates(Day(2022, 1, 6)),
                result.Value.Generate(_fromDate, _toDate));
        }

        [Theory]
        [InlineData("vendredi")]
        [InlineData("ven")]
        [InlineData("ve")]
        [InlineData("vendredi prochain")]
        [InlineData("ven prochain")]
        [InlineData("vendredi pro")]
        public void Test_ParseDateTimes_NextFriday(string input)
        {
            var result = _timeParser.ParseDateTimes(input);

            Assert.True(result.IsSuccess);
            Assert.Equal(
                Dates(Day(2022, 1, 7)),
                result.Value.Generate(_fromDate, _toDate));
        }

        [Theory]
        [InlineData("samedi")]
        [InlineData("sam")]
        [InlineData("sa")]
        [InlineData("samedi prochain")]
        [InlineData("sam prochain")]
        [InlineData("samedi pro")]
        public void Test_ParseDateTimes_NextSaturday(string input)
        {
            var result = _timeParser.ParseDateTimes(input);

            Assert.True(result.IsSuccess);
            Assert.Equal(
                Dates(Day(2022, 1, 8)),
                result.Value.Generate(_fromDate, _toDate));
        }

        [Theory]
        [InlineData("dimanche")]
        [InlineData("dim")]
        [InlineData("di")]
        [InlineData("dimanche prochain")]
        [InlineData("dim prochain")]
        [InlineData("dimanche pro")]
        public void Test_ParseDateTimes_NextSunday(string input)
        {
            var result = _timeParser.ParseDateTimes(input);

            Assert.True(result.IsSuccess);
            Assert.Equal(
                Dates(Day(2022, 1, 2)),
                result.Value.Generate(_fromDate, _toDate));
        }
        #endregion
        #endregion

        #region Chaque jour de la semaine

        #region DateTimes "chaque lundi"
        [Theory]
        [InlineData("le lundi")]
        [InlineData("les lundis")]
        [InlineData("le lun")]
        [InlineData("ls lun")]
        [InlineData("tous les lundis")]
        [InlineData("ts ls lundis")]
        [InlineData("chaque lundi")]
        [InlineData("ch lundi")]
        [InlineData("lundi de chaque semaine")]
        [InlineData("le lundi de ch sem")]
        [InlineData("le lundi chaque semaine")]
        public void Test_ParseDateTimes_EveryMonday(string input)
        {
            var result = _timeParser.ParseDateTimes(input);

            Assert.True(result.IsSuccess);
            Assert.Equal(
                Dates(
                    Day(2022, 1, 3),
                    Day(2022, 1, 10),
                    Day(2022, 1, 17),
                    Day(2022, 1, 24),
                    Day(2022, 1, 31)),
                result.Value.Generate(_fromDate, _febFst));
        }
        #endregion

        #endregion

        #region Dates relatives

        #region DateTimes "il y a N jours"
        [Theory]
        [InlineData("3 jours avant chaque lundi")]
        [InlineData("3 jour avant chaque lundi")]
        [InlineData("3 jour av chaque lundi")]
        [InlineData("3 j av chaque lundi")]
        public void Test_ParseDateTimes_NDaysBefore(string input)
        {
            var result = _timeParser.ParseDateTimes(input);

            Assert.True(result.IsSuccess);
            Assert.Equal(
                Dates(
                    Day(2021, 12, 31),
                    Day(2022, 1, 7),
                    Day(2022, 1, 14),
                    Day(2022, 1, 21),
                    Day(2022, 1, 28)),
                result.Value.Generate(_fromDate, _febFst));
        }
        #endregion

        #region DateTimes "l'avant-veille de"
        [Theory]
        [InlineData("l'avant-veille de chaque lundi")]
        [InlineData("l av veil de ch lun")]
        public void Test_ParseDateTimes_TwoDaysBefore(string input)
        {
            var result = _timeParser.ParseDateTimes(input);

            Assert.True(result.IsSuccess);
            Assert.Equal(
                Dates(
                    Day(2022, 1, 1),
                    Day(2022, 1, 8),
                    Day(2022, 1, 15),
                    Day(2022, 1, 22),
                    Day(2022, 1, 29)),
                result.Value.Generate(_fromDate, _febFst));
        }
        #endregion

        #region DateTimes "la veille de"
        [Theory]
        [InlineData("la veille de chaque lundi")]
        [InlineData("la veil de ch lun")]
        //[InlineData("le jour d'avant chaque lundi")]
        //[InlineData("le j d av ch lun")]
        public void Test_ParseDateTimes_TheDayBefore(string input)
        {
            var result = _timeParser.ParseDateTimes(input);

            Assert.True(result.IsSuccess);
            Assert.Equal(
                Dates(
                    Day(2022, 1, 2),
                    Day(2022, 1, 9),
                    Day(2022, 1, 16),
                    Day(2022, 1, 23),
                    Day(2022, 1, 30)),
                result.Value.Generate(_fromDate, _febFst));
        }
        #endregion

        #region DateTimes "le lendemain de"
        [Theory]
        [InlineData("le lendemain de chaque lundi")]
        [InlineData("le lendem de ch lun")]
        [InlineData("le lend de ch lun")]
        //[InlineData("le jour d'après chaque lundi")]
        //[InlineData("le j d ap ch lun")]
        public void Test_ParseDateTimes_TheDayAfter(string input)
        {
            var result = _timeParser.ParseDateTimes(input);

            Assert.True(result.IsSuccess);
            Assert.Equal(
                Dates(
                    Day(2022, 1, 4),
                    Day(2022, 1, 11),
                    Day(2022, 1, 18),
                    Day(2022, 1, 25)),
                result.Value.Generate(_fromDate, _febFst));
        }
        #endregion

        #region DateTimes "le surlendemain de"
        [Theory]
        [InlineData("le surlendemain de chaque lundi")]
        [InlineData("le surlend de ch lundi")]
        [InlineData("le surlendem de ch lundi")]
        public void Test_ParseDateTimes_TwoDaysAfter(string input)
        {
            var result = _timeParser.ParseDateTimes(input);

            Assert.True(result.IsSuccess);
            Assert.Equal(
                Dates(
                    Day(2022, 1, 5),
                    Day(2022, 1, 12),
                    Day(2022, 1, 19),
                    Day(2022, 1, 26)),
                result.Value.Generate(_fromDate, _febFst));
        }
        #endregion

        #region DateTimes "N jours après"
        [Theory]
        [InlineData("3 jours après chaque lundi")]
        [InlineData("3 j ap ch lun")]
        public void Test_ParseDateTimes_NDaysAfter(string input)
        {
            var result = _timeParser.ParseDateTimes(input);

            Assert.True(result.IsSuccess);
            Assert.Equal(
                Dates(
                    Day(2022, 1, 6),
                    Day(2022, 1, 13),
                    Day(2022, 1, 20),
                    Day(2022, 1, 27)),
                result.Value.Generate(_fromDate, _febFst));
        }
        #endregion

        #region Séquence de plusieurs shifts
        [Theory]
        [InlineData("3 jours avant le surlendemain de la veille de 2 jours après demain")]
        public void Test_ParseDateTimes_ShiftSequence(string input)
        {
            var result = _timeParser.ParseDateTimes(input);

            Assert.True(result.IsSuccess);
            Assert.Equal(
                Dates(Day(2022, 1, 2)),
                result.Value.Generate(_fromDate, _febFst));
        }
        #endregion

        #endregion

        #endregion

        #region Intervalles

        #region TimeIntervals "toujours"
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
        #endregion

        #region TimeIntervals "jamais"
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
        #endregion

        #region TimeIntervals "aujourd'hui"
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
        #endregion

        #region TimeIntervals "lundi prochain"
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
        #endregion

        #region TimeIntervals "chaque lundi"
        [Theory]
        [InlineData("le lundi")]
        [InlineData("les lundis")]
        [InlineData("le lun")]
        [InlineData("ls lun")]
        [InlineData("tous les lundis")]
        [InlineData("ts ls lundis")]
        [InlineData("chaque lundi")]
        [InlineData("ch lundi")]
        [InlineData("lundi de chaque semaine")]
        [InlineData("le lundi de ch sem")]
        [InlineData("le lundi chaque semaine")]
        public void Test_ParseTimeIntervals_EveryMonday(string input)
        {
            var result = _timeParser.ParseTimeIntervals(input);

            Assert.True(result.IsSuccess);
            Assert.Equal(
                Intervals(
                    DayInterval(2022, 1, 3),
                    DayInterval(2022, 1, 10),
                    DayInterval(2022, 1, 17),
                    DayInterval(2022, 1, 24),
                    DayInterval(2022, 1, 31)),
                result.Value.Generate(_fromDate, new DateTime(2022, 2, 1)));
        }
        #endregion

        #endregion
    }
}
