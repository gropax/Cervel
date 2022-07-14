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
            else if (_scope.MonthGenerators.HasValues())
            {
                var months = _scope.MonthGenerators.ConsumeSingle();
                CloseScope();
                _scope.IntervalGenerators.Add(months.ToTimeInterval());
            }
            else if (_scope.YearGenerators.HasValues())
            {
                var years = _scope.YearGenerators.ConsumeSingle();
                CloseScope();
                _scope.IntervalGenerators.Add(years.ToTimeInterval());
            }
            else
            {
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


        public override void EnterMonths(TimeExpressionV2Parser.MonthsContext context)
        {
            OpenScope(nameof(EnterMonths));
        }

        public override void ExitMonths(TimeExpressionV2Parser.MonthsContext context)
        {
            var intervalGenerator = _scope.MonthGenerators.ConsumeSingle();

            CloseScope();

            _scope.MonthGenerators.Add(intervalGenerator);
        }

        public override void EnterYears(TimeExpressionV2Parser.YearsContext context)
        {
            OpenScope(nameof(EnterYears));
        }

        public override void ExitYears(TimeExpressionV2Parser.YearsContext context)
        {
            var intervalGenerator = _scope.YearGenerators.ConsumeSingle();

            CloseScope();

            _scope.YearGenerators.Add(intervalGenerator);
        }



        public override void EnterDaysUntil(TimeExpressionV2Parser.DaysUntilContext context)
        {
            OpenScope(nameof(EnterDaysUntil));
        }

        public override void ExitDaysUntil(TimeExpressionV2Parser.DaysUntilContext context)
        {
            var days = _scope.DayGenerators.ConsumeSingle();
            var scope = _scope.IntervalGenerators.Consume();

            CloseScope();

            if (scope.Length > 0)
                _scope.DayGenerators.Add(Time.Until(scope[0], days));
            else
                _scope.DayGenerators.Add(days);
        }


        public override void EnterDaysSince(TimeExpressionV2Parser.DaysSinceContext context)
        {
            OpenScope(nameof(EnterDaysSince));
        }

        public override void ExitDaysSince(TimeExpressionV2Parser.DaysSinceContext context)
        {
            var days = _scope.DayGenerators.ConsumeSingle();
            var scope = _scope.IntervalGenerators.Consume();

            CloseScope();

            if (scope.Length > 0)
                _scope.DayGenerators.Add(Time.Since(scope[0], days));
            else
                _scope.DayGenerators.Add(days);
        }


        public override void EnterDaysExcept(TimeExpressionV2Parser.DaysExceptContext context)
        {
            OpenScope(nameof(EnterDaysExcept));
        }

        public override void ExitDaysExcept(TimeExpressionV2Parser.DaysExceptContext context)
        {
            var days = _scope.DayGenerators.ConsumeSingle();
            var scope = _scope.IntervalGenerators.Consume();

            CloseScope();

            if (scope.Length > 0)
                _scope.DayGenerators.Add(Time.Outside(scope[0], days));
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
            if (_scope.MonthGenerators.HasValues())
            {
                var scope = _scope.MonthGenerators.ConsumeSingle();
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


        #region Days in month

        #region Numbers
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

        #region Ordinals
        public override void ExitOrdinalInDigits(TimeExpressionV2Parser.OrdinalInDigitsContext context)
        {
            string field = context.children[0].GetText();
            string digits = new string(field.TakeWhile(c => char.IsDigit(c)).ToArray());
            int number = int.Parse(digits);
            _scope.Numbers.Add(number);
        }

        public override void ExitOrdinal1(TimeExpressionV2Parser.Ordinal1Context context) => _scope.Numbers.Add(1);
        public override void ExitOrdinal2(TimeExpressionV2Parser.Ordinal2Context context) => _scope.Numbers.Add(2);
        public override void ExitOrdinal3(TimeExpressionV2Parser.Ordinal3Context context) => _scope.Numbers.Add(3);
        public override void ExitOrdinal4(TimeExpressionV2Parser.Ordinal4Context context) => _scope.Numbers.Add(4);
        public override void ExitOrdinal5(TimeExpressionV2Parser.Ordinal5Context context) => _scope.Numbers.Add(5);
        public override void ExitOrdinal6(TimeExpressionV2Parser.Ordinal6Context context) => _scope.Numbers.Add(6);
        public override void ExitOrdinal7(TimeExpressionV2Parser.Ordinal7Context context) => _scope.Numbers.Add(7);
        public override void ExitOrdinal8(TimeExpressionV2Parser.Ordinal8Context context) => _scope.Numbers.Add(8);
        public override void ExitOrdinal9(TimeExpressionV2Parser.Ordinal9Context context) => _scope.Numbers.Add(9);
        public override void ExitOrdinal10(TimeExpressionV2Parser.Ordinal10Context context) => _scope.Numbers.Add(10);
        public override void ExitOrdinal11(TimeExpressionV2Parser.Ordinal11Context context) => _scope.Numbers.Add(11);
        public override void ExitOrdinal12(TimeExpressionV2Parser.Ordinal12Context context) => _scope.Numbers.Add(12);
        public override void ExitOrdinal13(TimeExpressionV2Parser.Ordinal13Context context) => _scope.Numbers.Add(13);
        public override void ExitOrdinal14(TimeExpressionV2Parser.Ordinal14Context context) => _scope.Numbers.Add(14);
        public override void ExitOrdinal15(TimeExpressionV2Parser.Ordinal15Context context) => _scope.Numbers.Add(15);
        public override void ExitOrdinal16(TimeExpressionV2Parser.Ordinal16Context context) => _scope.Numbers.Add(16);
        public override void ExitOrdinal17(TimeExpressionV2Parser.Ordinal17Context context) => _scope.Numbers.Add(17);
        public override void ExitOrdinal18(TimeExpressionV2Parser.Ordinal18Context context) => _scope.Numbers.Add(18);
        public override void ExitOrdinal19(TimeExpressionV2Parser.Ordinal19Context context) => _scope.Numbers.Add(19);
        public override void ExitOrdinal20(TimeExpressionV2Parser.Ordinal20Context context) => _scope.Numbers.Add(20);
        public override void ExitOrdinal21(TimeExpressionV2Parser.Ordinal21Context context) => _scope.Numbers.Add(21);
        public override void ExitOrdinal22(TimeExpressionV2Parser.Ordinal22Context context) => _scope.Numbers.Add(22);
        public override void ExitOrdinal23(TimeExpressionV2Parser.Ordinal23Context context) => _scope.Numbers.Add(23);
        public override void ExitOrdinal24(TimeExpressionV2Parser.Ordinal24Context context) => _scope.Numbers.Add(24);
        public override void ExitOrdinal25(TimeExpressionV2Parser.Ordinal25Context context) => _scope.Numbers.Add(25);
        public override void ExitOrdinal26(TimeExpressionV2Parser.Ordinal26Context context) => _scope.Numbers.Add(26);
        public override void ExitOrdinal27(TimeExpressionV2Parser.Ordinal27Context context) => _scope.Numbers.Add(27);
        public override void ExitOrdinal28(TimeExpressionV2Parser.Ordinal28Context context) => _scope.Numbers.Add(28);
        public override void ExitOrdinal29(TimeExpressionV2Parser.Ordinal29Context context) => _scope.Numbers.Add(29);
        public override void ExitOrdinal30(TimeExpressionV2Parser.Ordinal30Context context) => _scope.Numbers.Add(30);
        public override void ExitOrdinal31(TimeExpressionV2Parser.Ordinal31Context context) => _scope.Numbers.Add(31);
        #endregion

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
            _scope.MonthGenerators.Add(Time.EveryMonth());
        }

        public override void EnterMonthNameUnion(TimeExpressionV2Parser.MonthNameUnionContext context)
        {
            OpenScope(nameof(EnterMonthNameUnion));
        }

        public override void ExitMonthNameUnion(TimeExpressionV2Parser.MonthNameUnionContext context)
        {
            var monthGens = _scope.MonthNames.Consume().Distinct()
                .Select(month => Time.Each(month)).ToArray();

            CloseScope();

            if (monthGens.Length > 1)
                _scope.MonthGenerators.Add(Time.Union(monthGens));
            else
                _scope.MonthGenerators.Add(monthGens.Single());
        }

        public override void ExitJanuary(TimeExpressionV2Parser.JanuaryContext context) => _scope.MonthNames.Add(MonthOfYear.January);
        public override void ExitFebruary(TimeExpressionV2Parser.FebruaryContext context) => _scope.MonthNames.Add(MonthOfYear.February);
        public override void ExitMarch(TimeExpressionV2Parser.MarchContext context) => _scope.MonthNames.Add(MonthOfYear.March);
        public override void ExitApril(TimeExpressionV2Parser.AprilContext context) => _scope.MonthNames.Add(MonthOfYear.April);
        public override void ExitMay(TimeExpressionV2Parser.MayContext context) => _scope.MonthNames.Add(MonthOfYear.May);
        public override void ExitJune(TimeExpressionV2Parser.JuneContext context) => _scope.MonthNames.Add(MonthOfYear.June);
        public override void ExitJuly(TimeExpressionV2Parser.JulyContext context) => _scope.MonthNames.Add(MonthOfYear.July);
        public override void ExitAugust(TimeExpressionV2Parser.AugustContext context) => _scope.MonthNames.Add(MonthOfYear.August);
        public override void ExitSeptember(TimeExpressionV2Parser.SeptemberContext context) => _scope.MonthNames.Add(MonthOfYear.September);
        public override void ExitOctober(TimeExpressionV2Parser.OctoberContext context) => _scope.MonthNames.Add(MonthOfYear.October);
        public override void ExitNovember(TimeExpressionV2Parser.NovemberContext context) => _scope.MonthNames.Add(MonthOfYear.November);
        public override void ExitDecember(TimeExpressionV2Parser.DecemberContext context) => _scope.MonthNames.Add(MonthOfYear.December);


        public override void EnterNthYearUnion(TimeExpressionV2Parser.NthYearUnionContext context)
        {
            OpenScope(nameof(EnterNthYearUnion));
        }

        public override void ExitNthYearUnion(TimeExpressionV2Parser.NthYearUnionContext context)
        {
            var gens = _scope.YearGenerators.Consume();

            CloseScope();

            if (gens.Length > 1)
                _scope.YearGenerators.Add(Time.Union(gens));
            else
                _scope.YearGenerators.Add(gens.Single());
        }


        public override void EnterNthYearExpr(TimeExpressionV2Parser.NthYearExprContext context)
        {
            OpenScope(nameof(EnterNthYearExpr));
        }

        public override void ExitNthYearExpr(TimeExpressionV2Parser.NthYearExprContext context)
        {
            var ordinal = _scope.Numbers.ConsumeSingle();
            var gen = _scope.YearGenerators.ConsumeSingle();

            CloseScope();

            _scope.YearGenerators.Add(Time.Nth(ordinal, gen));
        }

        public override void EnterYearNameUnion(TimeExpressionV2Parser.YearNameUnionContext context)
        {
            OpenScope(nameof(EnterYearNameUnion));
        }

        public override void ExitYearNameUnion(TimeExpressionV2Parser.YearNameUnionContext context)
        {
            var yearGens = _scope.Numbers.Consume().Distinct()
                .Select(year => Time.Year(year)).ToArray();

            CloseScope();

            if (yearGens.Length > 1)
                _scope.YearGenerators.Add(Time.Union(yearGens));
            else
                _scope.YearGenerators.Add(yearGens.Single());
        }

        public override void ExitEveryYear(TimeExpressionV2Parser.EveryYearContext context)
        {
            _scope.YearGenerators.Add(Time.EveryYear());
        }

        public override void ExitYearName(TimeExpressionV2Parser.YearNameContext context)
        {
            string field = context.children[0].GetText();
            int number = int.Parse(context.children[^1].GetText());
            _scope.Numbers.Add(number);
        }


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

        public TmpVar<IGenerator<Day>> DayGenerators = new();
        public TmpVar<IGenerator<Month>> MonthGenerators = new();
        public TmpVar<IGenerator<Year>> YearGenerators = new();
        public TmpVar<IGenerator<TimeInterval>> IntervalGenerators = new();

        public TmpVar<DayOfWeek> DaysOfWeek = new();
        public TmpVar<int> Numbers = new();
        public TmpVar<MonthOfYear> MonthNames = new();

        public string DebuggerDisplay()
        {
            int intvs = IntervalGenerators.Get().Length;
            int days = DayGenerators.Get().Length;
            int months = MonthGenerators.Get().Length;
            return $"{_name}<intvs: {intvs}, days: {days}, months: {months}>";
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
