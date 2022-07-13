using Antlr4.Runtime;
using Cervel.TimeParser;
using Cervel.TimeParser.Dates;
using Cervel.TimeParser.Extensions;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace Cervel.TimeParser
{
    public class TimeExpressionV2Listener : TimeExpressionV2BaseListener
    {
        public IGenerator<TimeInterval> TimeIntervals { get; set; }

        private Stack<VarScope> _varScopes = new();
        private VarScope _scope;

        public TimeExpressionV2Listener()
        {
            OpenScope("EnterRoot");
        }

        private void CloseScope()
        {
            _varScopes.Pop();
            _scope = _varScopes.Peek();
        }

        private void OpenScope(string name)
        {
            _scope = new(name.Substring(5));
            _varScopes.Push(_scope);
        }

        public override void ExitTime(TimeExpressionV2Parser.TimeContext context)
        {
            TimeIntervals = _scope.IntervalGenerators.ConsumeSingle();
        }


        public override void EnterIntervals(TimeExpressionV2Parser.IntervalsContext context)
        {
            OpenScope(nameof(EnterIntervals));
        }

        public override void ExitIntervals(TimeExpressionV2Parser.IntervalsContext context)
        {
            if (_scope.DayGenerators.HasValues())
            {
                var days = _scope.DayGenerators.ConsumeSingle();

                CloseScope();

                _scope.IntervalGenerators.Add(days.ToTimeInterval());
            }
            else
            {
                var intervals = _scope.IntervalGenerators.ConsumeSingle();

                CloseScope();

                _scope.IntervalGenerators.Add(intervals);
            }
        }


        public override void EnterDays(TimeExpressionV2Parser.DaysContext context)
        {
            OpenScope(nameof(EnterDays));
        }

        public override void ExitDays(TimeExpressionV2Parser.DaysContext context)
        {
            var intervalGenerator = _scope.DayGenerators.ConsumeSingle();

            CloseScope();

            _scope.DayGenerators.Add(intervalGenerator);
        }


        public override void EnterMonthes(TimeExpressionV2Parser.MonthesContext context)
        {
            OpenScope(nameof(EnterMonthes));
        }

        public override void ExitMonthes(TimeExpressionV2Parser.MonthesContext context)
        {
            var intervalGenerator = _scope.DateGenerators.ConsumeSingle().AllMonth();

            CloseScope();

            _scope.IntervalGenerators.Add(intervalGenerator);
        }


        public override void EnterDaysUntil(TimeExpressionV2Parser.DaysUntilContext context)
        {
            OpenScope(nameof(EnterDaysUntil));
        }

        public override void ExitDaysUntil(TimeExpressionV2Parser.DaysUntilContext context)
        {
            var gens = _scope.DayGenerators.Get();

            CloseScope();

            if (context.children.Count > 1)
                _scope.DayGenerators.Add(Time.Until(gens[1], gens[0]));
            else
                _scope.DayGenerators.Add(gens[0]);
        }


        public override void EnterDaysSince(TimeExpressionV2Parser.DaysSinceContext context)
        {
            OpenScope(nameof(EnterDaysSince));
        }

        public override void ExitDaysSince(TimeExpressionV2Parser.DaysSinceContext context)
        {
            var gens = _scope.DayGenerators.Consume();

            CloseScope();

            if (context.children.Count > 1)
            {
                if (gens.Length > 1)
                    _scope.DayGenerators.Add(Time.Since(gens[1], gens[0]));
                else
                    _scope.DayGenerators.Add(Time.Since(_scope.DateGenerators.ConsumeSingle(), gens[0]));
            }
            else
                _scope.DayGenerators.Add(gens[0]);
        }


        public override void EnterDaysExcept(TimeExpressionV2Parser.DaysExceptContext context)
        {
            OpenScope(nameof(EnterDaysExcept));
        }

        public override void ExitDaysExcept(TimeExpressionV2Parser.DaysExceptContext context)
        {
            var days = _scope.DayGenerators.ConsumeSingle();
            var exclude = _scope.IntervalGenerators.Consume();

            CloseScope();

            if (context.children.Count > 1)
                _scope.DayGenerators.Add(Time.Outside(exclude[0], days));
            else
                _scope.DayGenerators.Add(days);
        }


        public override void EnterDaysScopedUnion(TimeExpressionV2Parser.DaysScopedUnionContext context)
        {
            OpenScope(nameof(EnterDaysScopedUnion));
        }

        public override void ExitDaysScopedUnion(TimeExpressionV2Parser.DaysScopedUnionContext context)
        {
            var gens = _scope.DayGenerators.Consume();

            CloseScope();

            if (gens.Length > 1)
                _scope.DayGenerators.Add(Time.Union(gens));
            else
                _scope.DayGenerators.Add(gens.Single());
        }


        public override void EnterDaysNEveryM(TimeExpressionV2Parser.DaysNEveryMContext context)
        {
            OpenScope(nameof(EnterDaysNEveryM));
        }

        public override void ExitDaysNEveryM(TimeExpressionV2Parser.DaysNEveryMContext context)
        {
            var days = _scope.DayGenerators.ConsumeSingle();
            if (_scope.Numbers.HasValues())
            {
                var numbers = _scope.Numbers.Consume();
                days = days.NEveryM(numbers[0], numbers[1]);
            }

            CloseScope();

            _scope.DayGenerators.Add(days);
        }


        public override void EnterDaysScoped(TimeExpressionV2Parser.DaysScopedContext context)
        {
            OpenScope(nameof(EnterDaysScoped));
        }

        public override void ExitDaysScoped(TimeExpressionV2Parser.DaysScopedContext context)
        {
            var days = _scope.DayGenerators.ConsumeSingle();
            if (_scope.IntervalGenerators.HasValues())
            {
                var scope = _scope.IntervalGenerators.ConsumeSingle();
                days = Time.Scope(scope, days);
            }

            CloseScope();

            _scope.DayGenerators.Add(days);
        }


        public override void EnterNthDayUnion(TimeExpressionV2Parser.NthDayUnionContext context)
        {
            OpenScope(nameof(EnterNthDayUnion));
        }

        public override void ExitNthDayUnion(TimeExpressionV2Parser.NthDayUnionContext context)
        {
            var gens = _scope.DayGenerators.Consume();

            CloseScope();

            if (gens.Length > 1)
                _scope.DayGenerators.Add(Time.Union(gens));
            else
                _scope.DayGenerators.Add(gens.Single());
        }


        public override void EnterNthDayExpr(TimeExpressionV2Parser.NthDayExprContext context)
        {
            OpenScope(nameof(EnterNthDayExpr));
        }

        public override void ExitNthDayExpr(TimeExpressionV2Parser.NthDayExprContext context)
        {
            var ordinal = _scope.Numbers.ConsumeSingle();
            var gen = _scope.DayGenerators.ConsumeSingle();

            CloseScope();

            _scope.DayGenerators.Add(Time.Nth(ordinal, gen));
        }


        public override void ExitEveryDay(TimeExpressionV2Parser.EveryDayContext context)
        {
            _scope.DayGenerators.Add(Time.EveryDay());
        }


        public override void EnterDayOfWeekUnion(TimeExpressionV2Parser.DayOfWeekUnionContext context)
        {
            OpenScope(nameof(EnterDayOfWeekUnion));
        }

        public override void ExitDayOfWeekUnion(TimeExpressionV2Parser.DayOfWeekUnionContext context)
        {
            var dowGens = _scope.DaysOfWeek.Consume().Distinct()
                .Select(dow => Time.Each(dow)).ToArray();

            CloseScope();

            if (dowGens.Length > 1)
                _scope.DayGenerators.Add(Time.Union(dowGens));
            else
                _scope.DayGenerators.Add(dowGens.Single());
        }


        public override void ExitMonday(TimeExpressionV2Parser.MondayContext context) => _scope.DaysOfWeek.Add(DayOfWeek.Monday);
        public override void ExitTuesday(TimeExpressionV2Parser.TuesdayContext context) => _scope.DaysOfWeek.Add(DayOfWeek.Tuesday);
        public override void ExitWednesday(TimeExpressionV2Parser.WednesdayContext context) => _scope.DaysOfWeek.Add(DayOfWeek.Wednesday);
        public override void ExitThursday(TimeExpressionV2Parser.ThursdayContext context) => _scope.DaysOfWeek.Add(DayOfWeek.Thursday);
        public override void ExitFriday(TimeExpressionV2Parser.FridayContext context) => _scope.DaysOfWeek.Add(DayOfWeek.Friday);
        public override void ExitSaturday(TimeExpressionV2Parser.SaturdayContext context) => _scope.DaysOfWeek.Add(DayOfWeek.Saturday);
        public override void ExitSunday(TimeExpressionV2Parser.SundayContext context) => _scope.DaysOfWeek.Add(DayOfWeek.Sunday);


        public override void EnterDayOfMonthUnion(TimeExpressionV2Parser.DayOfMonthUnionContext context)
        {
            OpenScope(nameof(EnterDayOfMonthUnion));
        }

        public override void ExitDayOfMonthUnion(TimeExpressionV2Parser.DayOfMonthUnionContext context)
        {
            var domGens = _scope.Numbers.Consume().Distinct()
                .Select(dayOfMonth => Time.Each(dayOfMonth)).ToArray();

            CloseScope();

            if (domGens.Length > 1)
                _scope.DayGenerators.Add(Time.Union(domGens));
            else
                _scope.DayGenerators.Add(domGens.Single());
        }


        #region Ordinals

        public override void ExitOrdinal1(TimeExpressionV2Parser.Ordinal1Context context) => _scope.Numbers.Add(1);
        public override void ExitOrdinal2(TimeExpressionV2Parser.Ordinal2Context context) => _scope.Numbers.Add(2);
        public override void ExitOrdinal3(TimeExpressionV2Parser.Ordinal3Context context) => _scope.Numbers.Add(3);
        public override void ExitOrdinal4(TimeExpressionV2Parser.Ordinal4Context context) => _scope.Numbers.Add(4);
        public override void ExitOrdinal5(TimeExpressionV2Parser.Ordinal5Context context) => _scope.Numbers.Add(5);

        #endregion

        #region Days in month

        public override void ExitNumberInDigits(TimeExpressionV2Parser.NumberInDigitsContext context)
        {
            string field = context.children[0].GetText();
            int number = int.Parse(context.children[^1].GetText());
            _scope.Numbers.Add(number);
        }

        public override void ExitNumber1(TimeExpressionV2Parser.Number1Context context) => _scope.Numbers.Add(1);
        public override void ExitNumber2(TimeExpressionV2Parser.Number2Context context) => _scope.Numbers.Add(2);
        public override void ExitNumber3(TimeExpressionV2Parser.Number3Context context) => _scope.Numbers.Add(3);
        public override void ExitNumber4(TimeExpressionV2Parser.Number4Context context) => _scope.Numbers.Add(4);
        public override void ExitNumber5(TimeExpressionV2Parser.Number5Context context) => _scope.Numbers.Add(5);
        public override void ExitNumber6(TimeExpressionV2Parser.Number6Context context) => _scope.Numbers.Add(6);
        public override void ExitNumber7(TimeExpressionV2Parser.Number7Context context) => _scope.Numbers.Add(7);
        public override void ExitNumber8(TimeExpressionV2Parser.Number8Context context) => _scope.Numbers.Add(8);
        public override void ExitNumber9(TimeExpressionV2Parser.Number9Context context) => _scope.Numbers.Add(9);
        public override void ExitNumber10(TimeExpressionV2Parser.Number10Context context) => _scope.Numbers.Add(10);
        public override void ExitNumber11(TimeExpressionV2Parser.Number11Context context) => _scope.Numbers.Add(11);
        public override void ExitNumber12(TimeExpressionV2Parser.Number12Context context) => _scope.Numbers.Add(12);
        public override void ExitNumber13(TimeExpressionV2Parser.Number13Context context) => _scope.Numbers.Add(13);
        public override void ExitNumber14(TimeExpressionV2Parser.Number14Context context) => _scope.Numbers.Add(14);
        public override void ExitNumber15(TimeExpressionV2Parser.Number15Context context) => _scope.Numbers.Add(15);
        public override void ExitNumber16(TimeExpressionV2Parser.Number16Context context) => _scope.Numbers.Add(16);
        public override void ExitNumber17(TimeExpressionV2Parser.Number17Context context) => _scope.Numbers.Add(17);
        public override void ExitNumber18(TimeExpressionV2Parser.Number18Context context) => _scope.Numbers.Add(18);
        public override void ExitNumber19(TimeExpressionV2Parser.Number19Context context) => _scope.Numbers.Add(19);
        public override void ExitNumber20(TimeExpressionV2Parser.Number20Context context) => _scope.Numbers.Add(20);
        public override void ExitNumber21(TimeExpressionV2Parser.Number21Context context) => _scope.Numbers.Add(21);
        public override void ExitNumber22(TimeExpressionV2Parser.Number22Context context) => _scope.Numbers.Add(22);
        public override void ExitNumber23(TimeExpressionV2Parser.Number23Context context) => _scope.Numbers.Add(23);
        public override void ExitNumber24(TimeExpressionV2Parser.Number24Context context) => _scope.Numbers.Add(24);
        public override void ExitNumber25(TimeExpressionV2Parser.Number25Context context) => _scope.Numbers.Add(25);
        public override void ExitNumber26(TimeExpressionV2Parser.Number26Context context) => _scope.Numbers.Add(26);
        public override void ExitNumber27(TimeExpressionV2Parser.Number27Context context) => _scope.Numbers.Add(27);
        public override void ExitNumber28(TimeExpressionV2Parser.Number28Context context) => _scope.Numbers.Add(28);
        public override void ExitNumber29(TimeExpressionV2Parser.Number29Context context) => _scope.Numbers.Add(29);
        public override void ExitNumber30(TimeExpressionV2Parser.Number30Context context) => _scope.Numbers.Add(30);
        public override void ExitNumber31(TimeExpressionV2Parser.Number31Context context) => _scope.Numbers.Add(31);

        #endregion

        public override void EnterDayOfWeekOfMonthUnion(TimeExpressionV2Parser.DayOfWeekOfMonthUnionContext context)
        {
            OpenScope(nameof(EnterDayOfWeekOfMonthUnion));
        }

        public override void ExitDayOfWeekOfMonthUnion(TimeExpressionV2Parser.DayOfWeekOfMonthUnionContext context)
        {
            var domGens = _scope.DayGenerators.Consume();

            CloseScope();

            if (domGens.Length > 1)
                _scope.DayGenerators.Add(Time.Union(domGens));
            else
                _scope.DayGenerators.Add(domGens.Single());
        }

        public override void EnterDayOfWeekOfMonthExpr(TimeExpressionV2Parser.DayOfWeekOfMonthExprContext context)
        {
            OpenScope(nameof(EnterDayOfWeekOfMonthExpr));
        }

        public override void ExitDayOfWeekOfMonthExpr(TimeExpressionV2Parser.DayOfWeekOfMonthExprContext context)
        {
            var dayOfWeek = _scope.DaysOfWeek.ConsumeSingle();
            var dayOfMonth = _scope.Numbers.ConsumeSingle();

            CloseScope();

            _scope.DayGenerators.Add(Time.Each(dayOfMonth).Where(dayOfWeek));
        }


        public override void ExitEveryMonth(TimeExpressionV2Parser.EveryMonthContext context)
        {
            _scope.DateGenerators.Add(Time.StartOfEveryMonth());
        }

        public override void EnterMonthNameUnion(TimeExpressionV2Parser.MonthNameUnionContext context)
        {
            OpenScope(nameof(EnterMonthNameUnion));
        }

        public override void ExitMonthNameUnion(TimeExpressionV2Parser.MonthNameUnionContext context)
        {
            var monthGens = _scope.MonthNames.Consume().Distinct()
                .Select(month => Time.StartOfEvery(month)).ToArray();

            CloseScope();

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


    [DebuggerDisplay("{DebuggerDisplay()}")]
    internal class VarScope
    {
        private string _name;
        public VarScope(string name)
        {
            _name = name;
        }

        public TmpVar<IGenerator<Date>> DateGenerators = new();
        public TmpVar<IGenerator<Day>> DayGenerators = new();
        public TmpVar<IGenerator<TimeInterval>> IntervalGenerators = new();
        public TmpVar<DayOfWeek> DaysOfWeek = new();
        public TmpVar<int> Numbers = new();
        public TmpVar<Month> MonthNames = new();

        public string DebuggerDisplay()
        {
            int intvs = IntervalGenerators.Get().Length;
            int days = DayGenerators.Get().Length;
            return $"{_name}<intvs: {intvs}, days: {days}>";
        }
    }

    internal class TmpVar<T>
    {
        private readonly List<T> _values = new();

        public void Add(T value)
        {
            _values.Add(value);
        }

        //public void Set(T value)
        //{
        //    _values.Clear();
        //    _values.Add(value);
        //}

        public bool HasValues()
        {
            return _values.Count > 0;
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
