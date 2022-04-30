using Antlr4.Runtime;
using Cervel.TimeParser;
using Cervel.TimeParser.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Cervel.TimeParser
{
    public class TimeExpressionListener : TimeExpressionBaseListener
    {
        private bool _parseDateTime;
        public TimeExpressionListener(bool parseDateTime)
        {
            _parseDateTime = parseDateTime;
        }

        public IGenerator<TimeInterval> TimeIntervalGenerator { get; set; }
        public IGenerator<DateTime> DateTimeGenerator { get; set; }
        
        private IGenerator<TimeInterval> _intervals;

        public override void ExitDateTimes(TimeExpressionParser.DateTimesContext context) =>
            DateTimeGenerator = ConsumeSingleDateGenerator();

        public override void ExitTimeIntervals(TimeExpressionParser.TimeIntervalsContext context)
        {
            TimeIntervalGenerator = _intervals;
        }

        public override void ExitDateIntervals(TimeExpressionParser.DateIntervalsContext context) =>
            _intervals = ConsumeSingleDateGenerator().AllDay();

        public override void ExitAlways(TimeExpressionParser.AlwaysContext context)
        {
            _intervals = new TimeIntervals.AlwaysGenerator();
        }

        public override void ExitNever(TimeExpressionParser.NeverContext context)
        {
            if (_parseDateTime)
                _dateGenerators.Add(new DateTimes.NeverGenerator());
            else
                _intervals = new TimeIntervals.NeverGenerator();
        }

        public override void ExitNow(TimeExpressionParser.NowContext context) =>
            _dateGenerators.Add(Time.Now());

        public override void ExitNDaysAgo(TimeExpressionParser.NDaysAgoContext context) =>
            _dateGenerators.Add(Time.Today().ShiftDay(-ConsumeSingleNumber()));
        public override void ExitDayBeforeYesterday(TimeExpressionParser.DayBeforeYesterdayContext context) =>
            _dateGenerators.Add(Time.Today().ShiftDay(-2));
        public override void ExitYesterday(TimeExpressionParser.YesterdayContext context) =>
            _dateGenerators.Add(Time.Yesterday());
        public override void ExitToday(TimeExpressionParser.TodayContext context) =>
            _dateGenerators.Add(Time.Today());
        public override void ExitTomorrow(TimeExpressionParser.TomorrowContext context) =>
            _dateGenerators.Add(Time.Tomorrow());
        public override void ExitDayAfterTomorrow(TimeExpressionParser.DayAfterTomorrowContext context) =>
            _dateGenerators.Add(Time.Today().ShiftDay(2));
        public override void ExitNDaysFromNow(TimeExpressionParser.NDaysFromNowContext context) =>
            _dateGenerators.Add(Time.Today().ShiftDay(ConsumeSingleNumber()));


        public override void ExitShiftedDate(TimeExpressionParser.ShiftedDateContext context)
        {
            var dateGenerator = ConsumeSingleDateGenerator();

            foreach (var shift in ConsumeDateShifts().Reverse())
                dateGenerator = shift(dateGenerator);

            _dateGenerators.Add(dateGenerator);
        }


        public override void ExitNDaysBefore(TimeExpressionParser.NDaysBeforeContext context) =>
            _dateShifts.Add(Time.DayShift(-ConsumeSingleNumber()));
        public override void ExitTwoDaysBefore(TimeExpressionParser.TwoDaysBeforeContext context) =>
            _dateShifts.Add(Time.DayShift(-2));
        public override void ExitTheDayBefore(TimeExpressionParser.TheDayBeforeContext context) =>
            _dateShifts.Add(Time.DayShift(-1));
        public override void ExitTheDayAfter(TimeExpressionParser.TheDayAfterContext context) =>
            _dateShifts.Add(Time.DayShift(1));
        public override void ExitTwoDaysAfter(TimeExpressionParser.TwoDaysAfterContext context) =>
            _dateShifts.Add(Time.DayShift(2));
        public override void ExitNDaysAfter(TimeExpressionParser.NDaysAfterContext context) =>
            _dateShifts.Add(Time.DayShift(ConsumeSingleNumber()));


        public override void ExitNextDayOfWeek(TimeExpressionParser.NextDayOfWeekContext context) =>
            _dateGenerators.Add(Time.Next(ConsumeSingleDayOfWeek()));

        public override void ExitEveryDayOfWeek(TimeExpressionParser.EveryDayOfWeekContext context) =>
            _dateGenerators.Add(Time.Each(ConsumeSingleDayOfWeek()));


        #region DateGenerator

        private List<IGenerator<DateTime>> _dateGenerators = new List<IGenerator<DateTime>>();
        private IGenerator<DateTime> ConsumeSingleDateGenerator()
        {
            var gen = _dateGenerators.Single();
            _dateGenerators.Clear();
            return gen;
        }

        #endregion

        #region Date shift

        private List<Func<IGenerator<DateTime>, IGenerator<DateTime>>> _dateShifts =
            new List<Func<IGenerator<DateTime>, IGenerator<DateTime>>>();
            
        private IEnumerable<Func<IGenerator<DateTime>, IGenerator<DateTime>>> ConsumeDateShifts()
        {
            var shifts = _dateShifts.ToArray();
            _dateShifts.Clear();
            return shifts;
        }

        #endregion

        #region DayOfWeek
        private HashSet<DayOfWeek> _daysOfWeek = new HashSet<DayOfWeek>();
        public override void ExitMonday(TimeExpressionParser.MondayContext context) => _daysOfWeek.Add(DayOfWeek.Monday);
        public override void ExitTuesday(TimeExpressionParser.TuesdayContext context) => _daysOfWeek.Add(DayOfWeek.Tuesday);
        public override void ExitWednesday(TimeExpressionParser.WednesdayContext context) => _daysOfWeek.Add(DayOfWeek.Wednesday);
        public override void ExitThursday(TimeExpressionParser.ThursdayContext context) => _daysOfWeek.Add(DayOfWeek.Thursday);
        public override void ExitFriday(TimeExpressionParser.FridayContext context) => _daysOfWeek.Add(DayOfWeek.Friday);
        public override void ExitSaturday(TimeExpressionParser.SaturdayContext context) => _daysOfWeek.Add(DayOfWeek.Saturday);
        public override void ExitSunday(TimeExpressionParser.SundayContext context) => _daysOfWeek.Add(DayOfWeek.Sunday);

        private DayOfWeek ConsumeSingleDayOfWeek()
        {
            var dow = _daysOfWeek.Single();
            _daysOfWeek.Clear();
            return dow;
        }

        #endregion


        private List<int> _numbers = new List<int>();
        public override void ExitNumber(TimeExpressionParser.NumberContext context)
        {
            int number = int.Parse(context.children[0].GetText());
            _numbers.Add(number);
        }

        private int ConsumeSingleNumber()
        {
            int number = _numbers.Single();
            _numbers.Clear();
            return number;
        }


        private void EnsureSuccess(ParserRuleContext context)
        {
            if (context.exception != null)
                throw new ParseError($"Could not parse context [{context.GetType()}].", context.exception);
        }
    }
}
