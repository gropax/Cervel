using Antlr4.Runtime;
using Cervel.TimeParser;
using Cervel.TimeParser.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Cervel.TimeParser
{
    public class TimeExpressionV2Listener : TimeExpressionV2BaseListener
    {
        private bool _parseDateTime;
        public TimeExpressionV2Listener(bool parseDateTime)
        {
            _parseDateTime = parseDateTime;
        }

        public IGenerator<TimeInterval> TimeIntervalGenerator { get; set; }
        public IGenerator<DateTime> DateTimeGenerator { get; set; }
        
        private IGenerator<TimeInterval> _intervals;

        public override void ExitDateTimes(TimeExpressionV2Parser.DateTimesContext context) =>
            DateTimeGenerator = ConsumeSingleDateGenerator();

        public override void ExitTimeIntervals(TimeExpressionV2Parser.TimeIntervalsContext context)
        {
            TimeIntervalGenerator = _intervals;
        }

        public override void ExitDateIntervals(TimeExpressionV2Parser.DateIntervalsContext context) =>
            _intervals = ConsumeSingleDateGenerator().AllDay();

        public override void ExitAlways(TimeExpressionV2Parser.AlwaysContext context)
        {
            _intervals = new TimeIntervals.AlwaysGenerator();
        }

        public override void ExitNever(TimeExpressionV2Parser.NeverContext context)
        {
            if (_parseDateTime)
                _dateGenerators.Add(new DateTimes.NeverGenerator());
            else
                _intervals = new TimeIntervals.NeverGenerator();
        }

        public override void ExitNow(TimeExpressionV2Parser.NowContext context) =>
            _dateGenerators.Add(Time.Now());

        public override void ExitNDaysAgo(TimeExpressionV2Parser.NDaysAgoContext context) =>
            _dateGenerators.Add(Time.Today().ShiftDay(-ConsumeSingleNumber()));
        public override void ExitDayBeforeYesterday(TimeExpressionV2Parser.DayBeforeYesterdayContext context) =>
            _dateGenerators.Add(Time.Today().ShiftDay(-2));
        public override void ExitYesterday(TimeExpressionV2Parser.YesterdayContext context) =>
            _dateGenerators.Add(Time.Yesterday());
        public override void ExitToday(TimeExpressionV2Parser.TodayContext context) =>
            _dateGenerators.Add(Time.Today());
        public override void ExitTomorrow(TimeExpressionV2Parser.TomorrowContext context) =>
            _dateGenerators.Add(Time.Tomorrow());
        public override void ExitDayAfterTomorrow(TimeExpressionV2Parser.DayAfterTomorrowContext context) =>
            _dateGenerators.Add(Time.Today().ShiftDay(2));
        public override void ExitNDaysFromNow(TimeExpressionV2Parser.NDaysFromNowContext context) =>
            _dateGenerators.Add(Time.Today().ShiftDay(ConsumeSingleNumber()));


        public override void ExitShiftedDate(TimeExpressionV2Parser.ShiftedDateContext context)
        {
            var dateGenerator = ConsumeSingleDateGenerator();

            foreach (var shift in ConsumeDateShifts().Reverse())
                dateGenerator = shift(dateGenerator);

            _dateGenerators.Add(dateGenerator);
        }


        public override void ExitNDaysBefore(TimeExpressionV2Parser.NDaysBeforeContext context) =>
            _dateShifts.Add(Time.DayShift(-ConsumeSingleNumber()));
        public override void ExitTwoDaysBefore(TimeExpressionV2Parser.TwoDaysBeforeContext context) =>
            _dateShifts.Add(Time.DayShift(-2));
        public override void ExitTheDayBefore(TimeExpressionV2Parser.TheDayBeforeContext context) =>
            _dateShifts.Add(Time.DayShift(-1));
        public override void ExitTheDayAfter(TimeExpressionV2Parser.TheDayAfterContext context) =>
            _dateShifts.Add(Time.DayShift(1));
        public override void ExitTwoDaysAfter(TimeExpressionV2Parser.TwoDaysAfterContext context) =>
            _dateShifts.Add(Time.DayShift(2));
        public override void ExitNDaysAfter(TimeExpressionV2Parser.NDaysAfterContext context) =>
            _dateShifts.Add(Time.DayShift(ConsumeSingleNumber()));


        public override void ExitNextDayOfWeek(TimeExpressionV2Parser.NextDayOfWeekContext context) =>
            _dateGenerators.Add(Time.Next(ConsumeSingleDayOfWeek()));

        public override void ExitEveryDayOfWeek(TimeExpressionV2Parser.EveryDayOfWeekContext context) =>
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
        public override void ExitMonday(TimeExpressionV2Parser.MondayContext context) => _daysOfWeek.Add(DayOfWeek.Monday);
        public override void ExitTuesday(TimeExpressionV2Parser.TuesdayContext context) => _daysOfWeek.Add(DayOfWeek.Tuesday);
        public override void ExitWednesday(TimeExpressionV2Parser.WednesdayContext context) => _daysOfWeek.Add(DayOfWeek.Wednesday);
        public override void ExitThursday(TimeExpressionV2Parser.ThursdayContext context) => _daysOfWeek.Add(DayOfWeek.Thursday);
        public override void ExitFriday(TimeExpressionV2Parser.FridayContext context) => _daysOfWeek.Add(DayOfWeek.Friday);
        public override void ExitSaturday(TimeExpressionV2Parser.SaturdayContext context) => _daysOfWeek.Add(DayOfWeek.Saturday);
        public override void ExitSunday(TimeExpressionV2Parser.SundayContext context) => _daysOfWeek.Add(DayOfWeek.Sunday);

        private DayOfWeek ConsumeSingleDayOfWeek()
        {
            var dow = _daysOfWeek.Single();
            _daysOfWeek.Clear();
            return dow;
        }

        #endregion


        private List<int> _numbers = new List<int>();
        public override void ExitNumber(TimeExpressionV2Parser.NumberContext context)
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
