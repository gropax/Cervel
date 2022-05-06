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

        public IGenerator<TimeInterval> IntervalDistribution { get; set; }
        public IGenerator<DateTime> DateDistribution { get; set; }
        
        private IGenerator<TimeInterval> _intervals;

        public override void ExitIntvDist(TimeExpressionV2Parser.IntvDistContext context)
        {
            IntervalDistribution = _intervals;
        }

        public override void ExitDayIntvDist(TimeExpressionV2Parser.DayIntvDistContext context) =>
            _intervals = ConsumeSingleDateGenerator().AllDay();

        public override void ExitDateDist(TimeExpressionV2Parser.DateDistContext context) =>
            DateDistribution = ConsumeSingleDateGenerator();

        public override void ExitEveryDay(TimeExpressionV2Parser.EveryDayContext context) =>
            SetDateGenerator(Time.EveryDay());

        public override void ExitDayOfWeekUnion(TimeExpressionV2Parser.DayOfWeekUnionContext context) =>
            SetDateGenerator(
                Time.Union(ConsumeDaysOfWeek()
                    .Select(dow => Time.Each(dow)).ToArray()));


        #region DateGenerator

        private List<IGenerator<DateTime>> _dateGenerators = new List<IGenerator<DateTime>>();
        private void SetDateGenerator(IGenerator<DateTime> generator)
        {
            _dateGenerators.Clear();
            _dateGenerators.Add(generator);
        }

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

        private DayOfWeek[] ConsumeDaysOfWeek()
        {
            var dows = _daysOfWeek.ToArray();
            _daysOfWeek.Clear();
            return dows;
        }

        private DayOfWeek ConsumeSingleDayOfWeek()
        {
            var dow = _daysOfWeek.Single();
            _daysOfWeek.Clear();
            return dow;
        }

        #endregion


        private void EnsureSuccess(ParserRuleContext context)
        {
            if (context.exception != null)
                throw new ParseError($"Could not parse context [{context.GetType()}].", context.exception);
        }
    }
}
