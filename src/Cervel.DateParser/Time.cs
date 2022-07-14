using Cervel.TimeParser.Dates;
using Cervel.TimeParser.Extensions;
using Cervel.TimeParser.Generic;
using Cervel.TimeParser.TimeIntervals;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cervel.TimeParser
{
    public static class Time
    {
        public static DateTime Min(DateTime dateTime, DateTime other)
        {
            return dateTime <= other ? dateTime : other;
        }

        public static DateTime Max(DateTime dateTime, DateTime other)
        {
            return dateTime >= other ? dateTime : other;
        }

        public static IGenerator<T> Never<T>()
            where T : ITimeInterval<T>
            => new NeverGenerator<T>();

        public static IGenerator<Date> Start()
            => new OnceGenerator();


        private static DateTime? _now;
        public static void SetNow(DateTime date) => _now = date;
        public static Date GetNow() => _now.HasValue ? new Date(_now.Value) : new Date(DateTime.Now);

        public static IGenerator<Date> Now() => new OnceGenerator(GetNow());

        public static IGenerator<Day> Yesterday() => Today().Increment(-1);
        public static IGenerator<Day> Today() => Now().CoveringDays();
        public static IGenerator<Day> Tomorrow() => Today().Increment(1);

        public static IGenerator<Day> EveryDay()
        {
            return new EveryUnitGenerator<Day>(TimeMeasures.Day);
        }

        public static IGenerator<Month> EveryMonth()
        {
            return new EveryUnitGenerator<Month>(TimeMeasures.Month);
        }

        public static IGenerator<Year> EveryYear()
        {
            return new EveryUnitGenerator<Year>(TimeMeasures.Year);
        }

        public static IGenerator<Day> Each(DayOfWeek dow) => new DayOfWeekGenerator(dow);
        public static IGenerator<Day> Each(int dayOfMonth) => new DayOfMonthGenerator(dayOfMonth);
        public static IGenerator<Month> Each(MonthOfYear moy) => new MonthByNameGenerator(moy);

        public static IGenerator<T> Nth<T>(int n, IGenerator<T> g)
            where T : ITimeInterval<T>
        {
            return g.Skip<T>(n - 1).Take<T>(1, name: $"Nth<{n}, {g.Name}>");
        }

        public static IGenerator<T> NEveryM<T>(
            int n, int m,
            IGenerator<T> g)
            where T : ITimeInterval<T>
        {
            return g.NEveryM(n, m);
        }

        public static IGenerator<T> Union<T>(
            params IGenerator<T>[] generators)
            where T : ITimeUnit<T>, IComparable<T>
        {
            return new UnionGenerator<T>(generators);
        }

        public static IGenerator<T> Since<T, TScope>(
            IGenerator<TScope> scope,
            IGenerator<T> generator)
            where T : ITimeInterval<T>
            where TScope : ITimeInterval<TScope>
            => new SinceGenerator<T, TScope>(scope, generator);

        public static IGenerator<T> Until<T, TScope>(
            IGenerator<TScope> scope,
            IGenerator<T> generator)
            where T : ITimeInterval<T>
            where TScope : ITimeInterval<TScope>
            => new UntilGenerator<T, TScope>(scope, generator);

        public static IGenerator<TimeInterval> Complement<T>(
            IGenerator<T> generator)
            where T : ITimeInterval<T>
        {
            return new ComplementGenerator<T>(generator);
        } 

        public static IGenerator<T> Inside<T, TScope>(
            IGenerator<TScope> scope,
            IGenerator<T> generator)
            where T : ITimeInterval<T>
            where TScope : ITimeInterval<TScope>
        {
            return new ScopeGenerator<T, TScope>(scope, generator);
        }
            
        public static IGenerator<T> Outside<T, TScope>(
            IGenerator<TScope> scope,
            IGenerator<T> generator)
            where T : ITimeInterval<T>
            where TScope : ITimeInterval<TScope>
        {
            return new ScopeGenerator<T, TimeInterval>(Complement(scope), generator);
        } 

        public static IGenerator<T> Scope<T, TScope>(
            IGenerator<TScope> scopes,
            IGenerator<T> dates)
            where T : ITimeInterval<T>
            where TScope : ITimeInterval<TScope>
        {
            return new ScopeGenerator<T, TScope>(scopes, dates);
        }


        public static IGenerator<TimeInterval<double>> Quantize<T>(
            ITimeMeasure<T> timeMeasure,
            IGenerator<TimeInterval> g)
            where T : ITimeUnit<T>
        {
            return new QuantizeGenerator<T>(timeMeasure, g);
        }


        #region Month related methods

        /// <summary>
        /// Le mois-date précédent (ou la date en cours)
        /// </summary>
        public static IGenerator<Date> StartOfCurrentMonth() => Start().StartOfMonth().First($"SOMonth");

        /// <summary>
        /// Premier jour-date du mois de chaque date
        /// </summary>
        public static IGenerator<Date> StartOfMonth(IGenerator<Date> g) => 
            g.Map(Maps.StartOfMonth(), $"StartOfMonth<{g.Name}>");

        /// <summary>
        /// Décale un séquence en multiples de mois
        ///     Attention ! Du fait de l'irrégularité des mois, un shift de mois peut générer des doublons
        ///     (e.g. le 30 et 31 janviers seront rapportés au 28 février). Il faut donc nettoyer le flux.
        /// </summary>
        public static IGenerator<Date> ShiftMonth(IGenerator<Date> g, int n) =>
            g.Map(Maps.ShiftMonth(n), $"ShiftMonth({n})");

        #endregion
    }
}
