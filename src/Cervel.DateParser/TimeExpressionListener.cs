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

        public ITimeGenerator<TimeInterval> TimeIntervalGenerator { get; set; }
        public ITimeGenerator<DateTime> DateTimeGenerator { get; set; }
        
        private ITimeGenerator<TimeInterval> _timeIntervalGenerator;
        private ITimeGenerator<DateTime> _dateTimeGenerator;

        public override void ExitDateTimes(TimeExpressionParser.DateTimesContext context)
        {
            DateTimeGenerator = _dateTimeGenerator;
        }

        public override void ExitTimeIntervals(TimeExpressionParser.TimeIntervalsContext context)
        {
            TimeIntervalGenerator = _timeIntervalGenerator;
        }

        public override void ExitAlways(TimeExpressionParser.AlwaysContext context)
        {
            _timeIntervalGenerator = new TimeIntervals.AlwaysGenerator();
        }

        public override void ExitNever(TimeExpressionParser.NeverContext context)
        {
            if (_parseDateTime)
                _dateTimeGenerator = new DateTimes.NeverGenerator();
            else
                _timeIntervalGenerator = new TimeIntervals.NeverGenerator();
        }

        public override void ExitNow(TimeExpressionParser.NowContext context) => _dateTimeGenerator = Time.Now();

        public override void ExitNDaysAgo(TimeExpressionParser.NDaysAgoContext context) => HandleDay(Time.Today().ShiftDay(- ConsumeSingleNumber()));
        public override void ExitDayBeforeYesterday(TimeExpressionParser.DayBeforeYesterdayContext context) => HandleDay(Time.Today().ShiftDay(-2));
        public override void ExitYesterday(TimeExpressionParser.YesterdayContext context) => HandleDay(Time.Yesterday());
        public override void ExitToday(TimeExpressionParser.TodayContext context) => HandleDay(Time.Today());
        public override void ExitTomorrow(TimeExpressionParser.TomorrowContext context) => HandleDay(Time.Tomorrow());
        public override void ExitDayAfterTomorrow(TimeExpressionParser.DayAfterTomorrowContext context) => HandleDay(Time.Today().ShiftDay(2));
        public override void ExitNDaysFromNow(TimeExpressionParser.NDaysFromNowContext context) => HandleDay(Time.Today().ShiftDay(ConsumeSingleNumber()));

        private void HandleDay(ITimeGenerator<DateTime> generator)
        {
            if (_parseDateTime)
                _dateTimeGenerator = generator;
            else
                _timeIntervalGenerator = generator.AllDay();
        }


        private HashSet<DayOfWeek> _daysOfWeek = new HashSet<DayOfWeek>();
        public override void ExitNextDayOfWeek(TimeExpressionParser.NextDayOfWeekContext context)
        {
            HandleDay(Time.Next(_daysOfWeek.Single()));
            _daysOfWeek.Clear();
        }

        public override void ExitEveryDayOfWeek(TimeExpressionParser.EveryDayOfWeekContext context)
        {
            HandleDay(Time.Each(_daysOfWeek.Single()));
            _daysOfWeek.Clear();
        }

        public override void ExitMonday(TimeExpressionParser.MondayContext context) => _daysOfWeek.Add(DayOfWeek.Monday);
        public override void ExitTuesday(TimeExpressionParser.TuesdayContext context) => _daysOfWeek.Add(DayOfWeek.Tuesday);
        public override void ExitWednesday(TimeExpressionParser.WednesdayContext context) => _daysOfWeek.Add(DayOfWeek.Wednesday);
        public override void ExitThursday(TimeExpressionParser.ThursdayContext context) => _daysOfWeek.Add(DayOfWeek.Thursday);
        public override void ExitFriday(TimeExpressionParser.FridayContext context) => _daysOfWeek.Add(DayOfWeek.Friday);
        public override void ExitSaturday(TimeExpressionParser.SaturdayContext context) => _daysOfWeek.Add(DayOfWeek.Saturday);
        public override void ExitSunday(TimeExpressionParser.SundayContext context) => _daysOfWeek.Add(DayOfWeek.Sunday);

        

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
