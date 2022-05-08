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
        public IGenerator<TimeInterval> IntervalDistribution { get; set; }
        public IGenerator<DateTime> DateDistribution { get; set; }
        
        private IGenerator<TimeInterval> _intervals;

        public override void ExitIntvDist(TimeExpressionV2Parser.IntvDistContext context)
        {
            IntervalDistribution = _intervals;
        }

        public override void ExitDayIntvDist(TimeExpressionV2Parser.DayIntvDistContext context)
        {
            _intervals = _dateGenerators.ConsumeSingle().AllDay();
        }

        public override void ExitMonthIntvDist(TimeExpressionV2Parser.MonthIntvDistContext context)
        {
            _intervals = _dateGenerators.ConsumeSingle().AllMonth();
        }

        public override void ExitDateDist(TimeExpressionV2Parser.DateDistContext context)
        {
            DateDistribution = _dateGenerators.ConsumeSingle();
        }

        public override void ExitDayDateSince(TimeExpressionV2Parser.DayDateSinceContext context)
        {
            var gens = _dateGenerators.Get();
            if (context.children.Count > 1)
                _dateGenerators.Set(Time.Since(gens[1], gens[0]));
        }

        public override void ExitDayDateUntil(TimeExpressionV2Parser.DayDateUntilContext context)
        {
            var gens = _dateGenerators.Get();
            if (context.children.Count > 1)
                _dateGenerators.Set(Time.Until(gens[1], gens[0]));
        }

        public override void ExitEveryDay(TimeExpressionV2Parser.EveryDayContext context)
        {
            _dateGenerators.Add(Time.EveryDay());
        }

        public override void ExitDayOfWeekUnion(TimeExpressionV2Parser.DayOfWeekUnionContext context)
        {
            var dowGens = _daysOfWeek.Consume().Distinct().Select(dow => Time.Each(dow)).ToArray();
            if (dowGens.Length > 1)
                _dateGenerators.Add(Time.Union(dowGens));
            else
                _dateGenerators.Add(dowGens.Single());
        }

        private readonly TmpVar<IGenerator<DateTime>> _dateGenerators = new();
        private readonly TmpVar<DayOfWeek> _daysOfWeek = new();
        private readonly TmpVar<Month> _monthNames = new();

        public override void ExitMonday(TimeExpressionV2Parser.MondayContext context) => _daysOfWeek.Add(DayOfWeek.Monday);
        public override void ExitTuesday(TimeExpressionV2Parser.TuesdayContext context) => _daysOfWeek.Add(DayOfWeek.Tuesday);
        public override void ExitWednesday(TimeExpressionV2Parser.WednesdayContext context) => _daysOfWeek.Add(DayOfWeek.Wednesday);
        public override void ExitThursday(TimeExpressionV2Parser.ThursdayContext context) => _daysOfWeek.Add(DayOfWeek.Thursday);
        public override void ExitFriday(TimeExpressionV2Parser.FridayContext context) => _daysOfWeek.Add(DayOfWeek.Friday);
        public override void ExitSaturday(TimeExpressionV2Parser.SaturdayContext context) => _daysOfWeek.Add(DayOfWeek.Saturday);
        public override void ExitSunday(TimeExpressionV2Parser.SundayContext context) => _daysOfWeek.Add(DayOfWeek.Sunday);



        public override void ExitEveryMonth(TimeExpressionV2Parser.EveryMonthContext context)
        {
            _dateGenerators.Add(Time.EveryMonth());
        }

        public override void ExitMonthNameUnion(TimeExpressionV2Parser.MonthNameUnionContext context)
        {
            var monthGens = _monthNames.Consume().Distinct()
                .Select(month => Time.Each(month)).ToArray();

            if (monthGens.Length > 1)
                _dateGenerators.Add(Time.Union(monthGens));
            else
                _dateGenerators.Add(monthGens.Single());
        }

        public override void ExitJanuary(TimeExpressionV2Parser.JanuaryContext context) => _monthNames.Add(Month.January);
        public override void ExitFebruary(TimeExpressionV2Parser.FebruaryContext context) => _monthNames.Add(Month.February);
        public override void ExitMarch(TimeExpressionV2Parser.MarchContext context) => _monthNames.Add(Month.March);
        public override void ExitApril(TimeExpressionV2Parser.AprilContext context) => _monthNames.Add(Month.April);
        public override void ExitMay(TimeExpressionV2Parser.MayContext context) => _monthNames.Add(Month.May);
        public override void ExitJune(TimeExpressionV2Parser.JuneContext context) => _monthNames.Add(Month.June);
        public override void ExitJuly(TimeExpressionV2Parser.JulyContext context) => _monthNames.Add(Month.July);
        public override void ExitAugust(TimeExpressionV2Parser.AugustContext context) => _monthNames.Add(Month.August);
        public override void ExitSeptember(TimeExpressionV2Parser.SeptemberContext context) => _monthNames.Add(Month.September);
        public override void ExitOctober(TimeExpressionV2Parser.OctoberContext context) => _monthNames.Add(Month.October);
        public override void ExitNovember(TimeExpressionV2Parser.NovemberContext context) => _monthNames.Add(Month.November);
        public override void ExitDecember(TimeExpressionV2Parser.DecemberContext context) => _monthNames.Add(Month.December);


        private static void EnsureSuccess(ParserRuleContext context)
        {
            if (context.exception != null)
                throw new ParseError($"Could not parse context [{context.GetType()}].", context.exception);
        }
    }


    internal class TmpVar<T>
    {
        private readonly List<T> _values = new();

        public void Add(T value)
        {
            _values.Add(value);
        }

        public void Set(T value)
        {
            _values.Clear();
            _values.Add(value);
        }

        public T[] Get()
        {
            return _values.ToArray();
        }

        public T[] Consume()
        {
            var values = _values.ToArray();
            _values.Clear();
            return values;
        }

        public T ConsumeSingle()
        {
            var value = _values.Single();
            _values.Clear();
            return value;
        }

    }
}
