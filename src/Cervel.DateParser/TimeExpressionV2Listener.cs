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

        private Stack<VarScope> _varScopes = new();
        private VarScope _scope;

        public TimeExpressionV2Listener()
        {
            OpenScope();
        }

        private void CloseScope()
        {
            _varScopes.Pop();
            _scope = _varScopes.Peek();
        }

        private void OpenScope()
        {
            _scope = new();
            _varScopes.Push(_scope);
        }


        public override void ExitIntvDist(TimeExpressionV2Parser.IntvDistContext context)
        {
            IntervalDistribution = _scope.IntervalGenerators.ConsumeSingle();
        }

        public override void ExitDateDist(TimeExpressionV2Parser.DateDistContext context)
        {
            DateDistribution = _scope.DateGenerators.ConsumeSingle();
        }


        public override void EnterDayIntvDist(TimeExpressionV2Parser.DayIntvDistContext context)
        {
            OpenScope();
        }

        public override void ExitDayIntvDist(TimeExpressionV2Parser.DayIntvDistContext context)
        {
            var intervalGenerator = _scope.DateGenerators.ConsumeSingle().AllDay();
            CloseScope();
            _scope.IntervalGenerators.Set(intervalGenerator);
        }

        public override void EnterMonthIntvDist(TimeExpressionV2Parser.MonthIntvDistContext context)
        {
            OpenScope();
        }

        public override void ExitMonthIntvDist(TimeExpressionV2Parser.MonthIntvDistContext context)
        {
            var intervalGenerator = _scope.DateGenerators.ConsumeSingle().AllMonth();
            CloseScope();
            _scope.IntervalGenerators.Set(intervalGenerator);
        }

        public override void EnterDayDateDist(TimeExpressionV2Parser.DayDateDistContext context)
        {
            OpenScope();
        }

        public override void ExitDayDateDist(TimeExpressionV2Parser.DayDateDistContext context)
        {
            var dateGenerator = _scope.DateGenerators.ConsumeSingle();
            CloseScope();
            _scope.DateGenerators.Set(dateGenerator);
        }

        public override void ExitDayDateSince(TimeExpressionV2Parser.DayDateSinceContext context)
        {
            var gens = _scope.DateGenerators.Get();
            if (context.children.Count > 1)
                _scope.DateGenerators.Set(Time.Since(gens[1], gens[0]));
        }

        public override void ExitDayDateUntil(TimeExpressionV2Parser.DayDateUntilContext context)
        {
            var gens = _scope.DateGenerators.Get();
            if (context.children.Count > 1)
                _scope.DateGenerators.Set(Time.Until(gens[1], gens[0]));
        }

        public override void ExitDayDateExcept(TimeExpressionV2Parser.DayDateExceptContext context)
        {
            if (context.children.Count > 1)
            {
                var dates = _scope.DateGenerators.ConsumeSingle();
                var exception = _scope.IntervalGenerators.ConsumeSingle();
                _scope.DateGenerators.Set(Time.Outside(exception, dates));
            }
        }

        public override void ExitDayDateScoped(TimeExpressionV2Parser.DayDateScopedContext context)
        {
            if (context.children.Count > 1)
            {
                var dates = _scope.DateGenerators.ConsumeSingle();
                var scope = _scope.IntervalGenerators.ConsumeSingle();
                _scope.DateGenerators.Set(Time.Scope(scope, dates));
            }
        }

        public override void ExitEveryDay(TimeExpressionV2Parser.EveryDayContext context)
        {
            _scope.DateGenerators.Add(Time.EveryDay());
        }

        public override void ExitDayOfWeekUnion(TimeExpressionV2Parser.DayOfWeekUnionContext context)
        {
            var dowGens = _scope.DaysOfWeek.Consume().Distinct().Select(dow => Time.Each(dow)).ToArray();
            if (dowGens.Length > 1)
                _scope.DateGenerators.Add(Time.Union(dowGens));
            else
                _scope.DateGenerators.Add(dowGens.Single());
        }


        public override void ExitMonday(TimeExpressionV2Parser.MondayContext context) => _scope.DaysOfWeek.Add(DayOfWeek.Monday);
        public override void ExitTuesday(TimeExpressionV2Parser.TuesdayContext context) => _scope.DaysOfWeek.Add(DayOfWeek.Tuesday);
        public override void ExitWednesday(TimeExpressionV2Parser.WednesdayContext context) => _scope.DaysOfWeek.Add(DayOfWeek.Wednesday);
        public override void ExitThursday(TimeExpressionV2Parser.ThursdayContext context) => _scope.DaysOfWeek.Add(DayOfWeek.Thursday);
        public override void ExitFriday(TimeExpressionV2Parser.FridayContext context) => _scope.DaysOfWeek.Add(DayOfWeek.Friday);
        public override void ExitSaturday(TimeExpressionV2Parser.SaturdayContext context) => _scope.DaysOfWeek.Add(DayOfWeek.Saturday);
        public override void ExitSunday(TimeExpressionV2Parser.SundayContext context) => _scope.DaysOfWeek.Add(DayOfWeek.Sunday);



        public override void ExitEveryMonth(TimeExpressionV2Parser.EveryMonthContext context)
        {
            _scope.DateGenerators.Add(Time.StartOfEveryMonth());
        }

        public override void ExitMonthNameUnion(TimeExpressionV2Parser.MonthNameUnionContext context)
        {
            var monthGens = _scope.MonthNames.Consume().Distinct()
                .Select(month => Time.StartOfEvery(month)).ToArray();

            if (monthGens.Length > 1)
                _scope.DateGenerators.Add(Time.Union(monthGens));
            else
                _scope.DateGenerators.Add(monthGens.Single());
        }

        public override void ExitJanuary(TimeExpressionV2Parser.JanuaryContext context) => _scope.MonthNames.Add(Month.January);
        public override void ExitFebruary(TimeExpressionV2Parser.FebruaryContext context) => _scope.MonthNames.Add(Month.February);
        public override void ExitMarch(TimeExpressionV2Parser.MarchContext context) => _scope.MonthNames.Add(Month.March);
        public override void ExitApril(TimeExpressionV2Parser.AprilContext context) => _scope.MonthNames.Add(Month.April);
        public override void ExitMay(TimeExpressionV2Parser.MayContext context) => _scope.MonthNames.Add(Month.May);
        public override void ExitJune(TimeExpressionV2Parser.JuneContext context) => _scope.MonthNames.Add(Month.June);
        public override void ExitJuly(TimeExpressionV2Parser.JulyContext context) => _scope.MonthNames.Add(Month.July);
        public override void ExitAugust(TimeExpressionV2Parser.AugustContext context) => _scope.MonthNames.Add(Month.August);
        public override void ExitSeptember(TimeExpressionV2Parser.SeptemberContext context) => _scope.MonthNames.Add(Month.September);
        public override void ExitOctober(TimeExpressionV2Parser.OctoberContext context) => _scope.MonthNames.Add(Month.October);
        public override void ExitNovember(TimeExpressionV2Parser.NovemberContext context) => _scope.MonthNames.Add(Month.November);
        public override void ExitDecember(TimeExpressionV2Parser.DecemberContext context) => _scope.MonthNames.Add(Month.December);


        private static void EnsureSuccess(ParserRuleContext context)
        {
            if (context.exception != null)
                throw new ParseError($"Could not parse context [{context.GetType()}].", context.exception);
        }
    }


    internal class VarScope
    {
        public TmpVar<IGenerator<DateTime>> DateGenerators = new();
        public TmpVar<IGenerator<TimeInterval>> IntervalGenerators = new();
        public TmpVar<DayOfWeek> DaysOfWeek = new();
        public TmpVar<Month> MonthNames = new();
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
